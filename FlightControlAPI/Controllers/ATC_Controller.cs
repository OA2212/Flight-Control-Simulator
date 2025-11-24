
using CommonModels;
using FlightControlAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATC_Controller : ControllerBase
    {

        private IManagmentATC _managmentATC;
        public ATC_Controller(IManagmentATC managmentATC)
        {
            _managmentATC = managmentATC;
        }

        [Route("NewFlightAndPlane")]
        [HttpPost]
        public ActionResult CreateNewPlaneAndFlight(NewFlightAndPlaneRequest ng)
        {
            if (ng == null)
            {
                return Problem("Empty request !", "Request", 400);
            }
            var Newng = _managmentATC.CreateNewPlaneAndFlight(ng);

            

            return Ok(Newng);

        }

        [Route("Landing")]
        [HttpPost]
        public ActionResult ManageLandingRequests(Request? req)
        {
            if (req == null)
            {
            }

            var newReq = _managmentATC.LandingManagment(req);

            return Ok(newReq!);

        }
        

        [Route("Departure")]
        [HttpPost]
        public IActionResult ManageDepartureRequests(Request? req)
        {
            if (req == null)
            {
                return BadRequest();
            }
            var newReq =_managmentATC.DepatureManagment(req)!;

            return Ok(newReq!);
        }

        [HttpGet]
        public async Task<IEnumerable<Plane>> GetAllLandinbgPlanes()
        {
            var planes = await _managmentATC.GetAllPlanes();
            return planes;
        }

    }
}
