using System;
using System.Collections.Generic;
using System.Linq;
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
            _userManager.RemoveConnection(Context.ConnectionId);
        }
        public async Task RelayMessage(string groupName, string message)
        {
            await Clients.Clients(_groupManager.GetUsers(groupName)).ReceiveMessage(message);
        }

        public async Task GroupJoin(string groupName)
        {
            _groupManager.AddToGroup( _userManager.GetUser(Context.ConnectionId).Username, groupName);
        }

        public async Task GroupLeave(string groupName)
        {
            _groupManager.RemoveFromGroup(_userManager.GetUser(Context.ConnectionId).Username, groupName);
        }

        public async Task<List<GroupInfo>> GetGroups()
        {
            return _groupManager.GetGroups();
        }
    }
}
