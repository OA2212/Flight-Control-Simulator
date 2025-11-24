using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommonModels;
using FlightControl_GUI.Models;

namespace FlightControl_GUI
{
    public class UI_Service
    {
        public UI_Service()
        {
        }

        public UiItem SetUiPlane(Plane plane)
        {
            UiItem item;

            if (plane.AirportLocation?.LocationNumber == 1 || plane.AirportLocation?.LocationNumber == 2 || plane.AirportLocation?.LocationNumber == 3 || plane.AirportLocation?.LocationNumber == 4 || plane.AirportLocation?.LocationNumber == 9)
            {
                item = new UiItem(60, 60, 0, -10, @"C:\Users\ofek1\Desktop\FlightControl-Project\FlightControl-GUI\photos\planes\plane1.png") { Plane = plane };
                return item;
            }
            else if(plane.AirportLocation?.LocationNumber == 5)
            {
                item = new UiItem(60, 60, -20, 0, @"C:\Users\ofek1\Desktop\FlightControl-Project\FlightControl-GUI\photos\planes\‏‏plane12.png") { Plane = plane };
                return item;
            }
            else if(plane.AirportLocation?.LocationNumber == 6 || plane.AirportLocation?.LocationNumber == 7)
            {
                item = new UiItem(60, 60, 0, 0, @"C:\Users\ofek1\Desktop\FlightControl-Project\FlightControl-GUI\photos\planes\‏‏plane12.png") { Plane = plane };
                return item;
            }
            else
            {
                item = new UiItem(60, 60, -20, 80, @"C:\Users\ofek1\Desktop\FlightControl-Project\FlightControl-GUI\photos\planes\‏‏‏‏plane13.png") { Plane = plane };
                return item;
            }
        }

    }
}
