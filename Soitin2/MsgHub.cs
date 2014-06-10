using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Soitin2
{
    public class MsgHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.broadcastMessage(message);
        }
        public void StatusUpdate(string trackname, int elapsedtime, int totaltime, int state)
        {
            Clients.All.statusMessage(trackname, elapsedtime, totaltime, state);
        }
        public void CurrentPlaylistUpdate()
        {
            Clients.All.currentPlaylistUpdateMessage();
        }
        public void SupplementalPlaylistUpdate() 
        {
            Clients.All.supplementalPlaylistUpdateMessage();
        }
            
    }
}