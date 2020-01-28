using System.Collections.Generic;
using Sharpach.SignalTest.Entities;

namespace Sharpach.SignalTest.Entities 
{
    public class Group
    {
        public GroupInfo GroupInfo;
        public List<string> Users;

        public Group(string groupName, int? maxSize = null)
        {
            GroupInfo = new GroupInfo{ Name = groupName, MaxSize = maxSize};
            Users = new List<string>();
        }

        public Group(GroupInfo groupInfo)
        {
            GroupInfo = groupInfo;
            Users = new List<string>();
        }
    }
}