using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ComicTales
{
    public class StoryNotifications : Hub
    {
        public void Join(string groupId)
        {
            Groups.Add(Context.ConnectionId, groupId);
        }
    }
}