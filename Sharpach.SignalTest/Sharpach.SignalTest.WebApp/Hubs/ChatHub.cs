using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Sharpach.SignalTest.Core;
using Sharpach.SignalTest.Entities;

namespace Sharpach.SignalTest.WebApp.Hubs
{
    public class ChatHub : Hub<IChatHubClient>
    {
        private readonly GroupManager _groupManager;
        private readonly UserManager _userManager;

        public ChatHub(GroupManager groupManager, UserManager userManager)
        {
            _groupManager = groupManager;
            _userManager = userManager;
        }
        
        public async Task Register(string username)
        {
            _userManager.AddConnection(Context.ConnectionId, username);
        }

        public async Task LogOff()
        {
            if (_userManager.IsAuthed(Context.ConnectionId))
            {
                _userManager.RemoveConnection(Context.ConnectionId);
            }
            else
            {
                throw new AuthenticationException("You must register first");
            }
        }
        public async Task RelayMessage(string groupName, string message)
        {
            if (_userManager.IsAuthed(Context.ConnectionId))
            {
                await Clients.Clients(_groupManager.GetUsers(groupName)).ReceiveMessage(message);
            }
            else
            {
                throw new AuthenticationException("You must register first");
            }
        }

        public async Task GroupJoin(string groupName)
        {
            if (_userManager.IsAuthed(Context.ConnectionId))
            {
                _groupManager.AddToGroup( _userManager.GetUser(Context.ConnectionId).Username, groupName);
            }
            else
            {
                throw new AuthenticationException("You must register first");
            }
        }

        public async Task GroupLeave(string groupName)
        {
            if (_userManager.IsAuthed(Context.ConnectionId))
            {
                _groupManager.RemoveFromGroup(_userManager.GetUser(Context.ConnectionId).Username, groupName);
            }
            else
            {
                throw new AuthenticationException("You must register first");
            }
        }

        public async Task GroupCreate(string groupName)
        {
            if (_userManager.IsAuthed(Context.ConnectionId))
            {
                _groupManager.CreateGroup(new GroupInfo{ Name = groupName });
            }
            else
            {
                throw new AuthenticationException("You must register first");
            }
        }

        public async Task<List<GroupInfo>> GetGroups()
        {
            return _groupManager.GetGroups();
        }
    }
}
