using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string groupName)
        {
            //Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);

            Clients.Group(groupName).addNewMessageToPage(name, message);
        }

        public Task JoinGroup(string roomName, string name)
        {
            Send(name, " has joined the room", roomName);
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }

        public void Pause(string groupName)
        {
            Clients.Group(groupName)._pause();
        }

        public void Play(string groupName, string time)
        {
            Clients.Group(groupName)._play(time);
        }
    }
}