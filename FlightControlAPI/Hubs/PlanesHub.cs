using CommonModels;
using FlightControlAPI.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlightControlAPI.Hubs
{
    public class PlanesHub : Hub
    {

        private readonly IManagmentATC _managmentATC;

        public PlanesHub(IManagmentATC managmentATC)
        {
            _managmentATC = managmentATC;
        }

        public async Task Send()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var planes = await _managmentATC.GetAllPlanes();
            var json = JsonSerializer.Serialize(planes, options);

            await Clients.All.SendAsync("GetPlanes", json);
        }
    }
}
