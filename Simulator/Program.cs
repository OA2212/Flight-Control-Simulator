using CommonModels;
using System.Net.Http.Json;
using Plane = CommonModels.Plane;
using Timer = System.Timers.Timer;

Random rnd = new Random();
HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5044/");
var result = await client.GetFromJsonAsync<List<Plane>>("api/ATC_/");
if (result?.Count == 0)
{
    for (int i = 0; i < 5; i++)
    {
        Flight flight1;
        Plane plane1;
        if (i >= 3)
        {
            plane1 = new Plane() { Name = Guid.NewGuid().ToString(), AirportLocationId = i+3 };
            flight1 = new Flight() { FlightNumber = $"LY{rnd.Next(100, 1000)}", isLanding = false };
        }
        else
        {
            plane1 = new Plane() { Name = Guid.NewGuid().ToString(), AirportLocationId = 1 };
            flight1 = new Flight() { FlightNumber = $"LY{rnd.Next(100, 1000)}", isLanding = true };
        }
        NewFlightAndPlaneRequest ng = new NewFlightAndPlaneRequest() { Flight = flight1, Plane = plane1 };

        var res = await client.PostAsJsonAsync<NewFlightAndPlaneRequest>("api/ATC_/NewFlightAndPlane", ng);

        Console.WriteLine("New Plane and Flight created");
    }
}

Timer timer = new Timer(7000);
timer.Elapsed += async (sender, e) => await UpdatedPlanesLocation();
timer.Start();
Timer timer1 = new Timer(14000);
timer1.Elapsed += async (sender, e) => await AddNewPlaneAndFlight();
timer1.Start();
Console.Write("Press any key to exit... ");
Console.ReadKey();


static async Task UpdatedPlanesLocation()
{
    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri("http://localhost:5044/");
    var result = await client.GetFromJsonAsync<List<Plane>>("api/ATC_/");
    for (int i = 0; i < result!.Count; i++)
    {
        if (result[i].AirportLocation?.LocationNumber >= 6 || (result[i].AirportLocation?.LocationNumber == 4 && !result[i]!.Flight!.isLanding))
        {
            Request req2 = new Request() { isLanding = false, Flight = result[i].Flight, FlightId = result[i].Flight!.Id };
            var res1 = await client.PostAsJsonAsync<Request>("api/ATC_/departure", req2);
            Console.WriteLine("Location updated - in departure");
        }
        else
        {
            Request req1 = new Request() { isLanding = true, Flight = result[i].Flight, FlightId = result[i].Flight!.Id };
            var res = await client.PostAsJsonAsync<Request>("api/ATC_/Landing", req1);
            Console.WriteLine("Location updated - in Landing");
        }
    }
}

static async Task AddNewPlaneAndFlight()
{
    Random rnd = new Random();
    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri("http://localhost:5044/");

    Plane plane1 = new Plane() { Name = Guid.NewGuid().ToString(), AirportLocationId = 1 };
    Flight flight1 = new Flight() { FlightNumber = $"LY{rnd.Next(100, 1000)}", isLanding = true };
    NewFlightAndPlaneRequest ng = new NewFlightAndPlaneRequest() { Flight = flight1, Plane = plane1 };
    var res2 = await client.PostAsJsonAsync<NewFlightAndPlaneRequest>("api/ATC_/NewFlightAndPlane", ng);
    Console.WriteLine("New Plane and Flight created");
}
