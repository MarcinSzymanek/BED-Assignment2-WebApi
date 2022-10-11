using Assignment2.Controllers;
using Assignment2.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Assignment2.Hubs;

[HubName("messageHub")]
public class MessageHub : Hub
{
    public MessageHub()
    {

    }
   
}