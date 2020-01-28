using Sharpach.SignalTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharpach.SignalTest.Core
{
    public class GroupManager
    {
        private readonly List<Group> _groups;
        public GroupManager()
        {
            _groups = new List<Group>();
        }

        public List<GroupInfo> GetGroups()
        {
            return null;
        }

        public void CreateGroup(GroupInfo groupInfo)
        {
            if (_groups.All(g => g.GroupInfo.Name != groupInfo.Name))
            {
                _groups.Add(new Group(groupInfo));
            }
        }

        public void RemoveGroup(string groupName)
        {
            if (_groups.Any(g => g.GroupInfo.Name == groupName))
            {
                _groups.RemoveAll(g => g.GroupInfo.Name == groupName);
            }
        }

        public void AddToGroup(string userId, string groupName)
        {
            var group = _groups.SingleOrDefault(g => g.GroupInfo.Name == groupName);
            if (group != null)
            {
                if (group.GroupInfo.MaxSize < group.GroupInfo.UserCount)
                {
                    group.Users.Add(userId);
                }
                else
                {
                    throw new Exception($"Group {groupName} already contains maximum amount of users - {group.GroupInfo.UserCount}");
                }
            }
            else
            {
                throw new Exception($"Group {groupName} does not exist");
            }
        }

        public void RemoveFromGroup(string userId, string groupName)
        {
            var group = _groups.SingleOrDefault(g => g.GroupInfo.Name == groupName);
            if (group != null)
            {
                group.Users.Remove(userId);
            }
            else
            {
                throw new Exception($"Group {groupName} does not exist");
            }
        }

        public List<string> GetUsers(string groupName)
        {
            var group = _groups.SingleOrDefault(g => g.GroupInfo.Name == groupName);
            if (group != null)
            {
                return group.Users;
            }
            else
            {
                throw new Exception($"Group {groupName} does not exist");
            }
        }
    }
}
