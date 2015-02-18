using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRChat.Models;
using SignalRChat.Controllers;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ConfessionHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients
            Clients.All.addNewMessageToPage(name, message);

            try
            {
                // Add this message to the database
                Task.Run(async () => await SendToDb(new Confession
                {
                    Submitter = name,
                    TheConfession = message
                }));
            }
            catch (Exception e)
            {
                Clients.All.addNewMessageToPage("System", string.Format(
                    "The message \"{0}: {1}\" encountered an error while saving...\n{2}", name, message, e.Message));
            }
        }

        public async Task SendToDb(Confession conf)
        {
            var confCont = new ConfessionsController();
            await confCont.Create(conf);
        }
    }
}