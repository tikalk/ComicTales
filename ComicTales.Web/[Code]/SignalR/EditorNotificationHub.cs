using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ComicTales
{
    [HubName("comicStoryNotificationsHub")]
    public class StoryNotificationsHub : Hub
    {
        // called by the client when editing a story
        public void Join(string groupId /*actualy this is the story Id*/)
        {
            Groups.Add(Context.ConnectionId, groupId);
        }

        public void SendMessageToGroup(string groupId, string message)
        {
            Clients.Group(groupId).addMessageToGroup(message);
        }

        #region Who Is Connected

        private List<string> _connections = new List<string>();

        //called by a timer
        public void WhoIsConnected()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<StoryNotificationsHub>();
            context.Clients.All.areYouConnected();
            _connections.Clear();
        }

        public void IAmConnected()
        {
            if (!_connections.Contains(Context.ConnectionId))
            {
                _connections.Add(Context.ConnectionId);
            }
        }

        #endregion

        #region Override

        public static HashSet<string> ClientsMonitor = new HashSet<string>();

        public override System.Threading.Tasks.Task OnConnected()
        {
            Log(Context.ConnectionId);

            //see if the user was already in a group
            RejoinGroup(Context.ConnectionId);

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            Log(Context.ConnectionId);
            return base.OnDisconnected();
        }

        public override System.Threading.Tasks.Task OnReconnected()
        {
            Log(Context.ConnectionId);
            return base.OnReconnected();
        }

        private void Log(string p)
        {
            
        }

        private void RejoinGroup(string s)
        {
           
        }

        #endregion
    }
}