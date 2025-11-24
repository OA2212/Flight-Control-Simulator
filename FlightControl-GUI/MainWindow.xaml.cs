
using CommonModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;

namespace FlightControl_GUI
{
    public partial class MainWindow : Window
    {
        HubConnection hubConnection;
        private DispatcherTimer timer;
        private UI_Service _Service;
        public MainWindow()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder()
                      .WithUrl("http://localhost:5044/Planes")
                      .Build();
            _Service = new UI_Service();
            timer = new DispatcherTimer();
        }
        private async void OnLoad(object sender, RoutedEventArgs e)
        {

            hubConnection.On("GetPlanes", (string data) =>
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                var planesList = JsonSerializer.Deserialize<List<Plane>>(data, options);

                Dispatcher.Invoke(() =>
                {
                    txtbTime.Text = $"{DateTime.Now}";
                    cnvAdd1.Children.Clear();
                    cnvAdd2.Children.Clear();
                    cnvAdd3.Children.Clear();
                    cnvAdd4.Children.Clear();
                    cnvAdd51.Children.Clear();
                    cnvAdd52.Children.Clear();
                    cnvAdd53.Children.Clear();
                    cnvAdd54.Children.Clear();
                    cnvAdd6.Children.Clear();
                    cnvAdd7.Children.Clear();
                    cnvAdd8.Children.Clear();
                    cnvAdd9.Children.Clear();
                    txtbFN2.Text = "";
                    txtbFN3.Text = "";
                    txtbFN4.Text = "";
                    txtbFN51.Text = "";
                    txtbFN52.Text = "";
                    txtbFN53.Text = "";
                    txtbFN54.Text = "";
                    txtbFN6.Text = "";
                    txtbFN7.Text = "";
                    txtbFN8.Text = "";
                    txtbFN9.Text = "";
                    lvPlanes.Items.Clear();
                    foreach (var item in planesList!)
                    {
                        var UiItem = _Service.SetUiPlane(item);
                        string status = "";
                        switch (item.AirportLocation?.LocationNumber)
                        {
                            case 1:
                                {
                                    status = " In Landing Proccess (1)";
                                    cnvAdd1.Children.Add(UiItem.Image);
                                }
                                break;
                            case 2:
                                {
                                    status = " In Landing Proccess (2)";
                                    cnvAdd2.Children.Add(UiItem.Image);
                                    txtbFN2.Text = UiItem.Plane?.Flight?.FlightNumber;
                                }
                                break;
                            case 3:
                                {
                                    status = " In Landing Proccess (3)";
                                    cnvAdd3.Children.Add(UiItem.Image);
                                    txtbFN3.Text = UiItem.Plane?.Flight?.FlightNumber;
                                }
                                break;
                            case 4:
                                if (item.Flight!.isLanding)
                                {
                                    status = " Landed";
                                    cnvAdd4.Children.Add(UiItem.Image);
                                    txtbFN4.Foreground = Brushes.DarkRed;
                                    txtbFN4.Text = UiItem.Plane?.Flight?.FlightNumber;
                                }
                                else
                                {
                                    status = " Departuring";
                                    cnvAdd4.Children.Add(UiItem.Image);
                                    txtbFN4.Foreground = Brushes.Yellow;
                                    txtbFN4.Text = UiItem.Plane?.Flight?.FlightNumber;
                                }
                                break;
                            case 5:
                                {
                                    status = " Taxi to the Gate";
                                    if (cnvAdd51.Children.Count == 0)
                                    {
                                        cnvAdd51.Children.Add(UiItem.Image);
                                        txtbFN51.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                    else if(cnvAdd52.Children.Count == 0)
                                    {
                                        cnvAdd52.Children.Add(UiItem.Image);
                                        txtbFN52.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                    else if (cnvAdd53.Children.Count == 0)
                                    {
                                        cnvAdd53.Children.Add(UiItem.Image);
                                        txtbFN53.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                    else
                                    {
                                        cnvAdd54.Children.Add(UiItem.Image);
                                        txtbFN54.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                }
                                break;
                            case 6:
                                {
                                    if (item.Flight!.isLanding)
                                    {
                                        status = " Gate 6\nDrops off passengers";
                                        cnvAdd6.Children.Add(UiItem.Image);
                                        txtbFN6.Foreground = Brushes.DarkRed;
                                        txtbFN6.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                    else
                                    {
                                        status = " Gate 6\npicks up passengers";
                                        cnvAdd6.Children.Add(UiItem.Image);
                                        txtbFN6.Foreground = Brushes.Yellow;
                                        txtbFN6.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                }
                                break;
                            case 7:
                                {
                                    if (item.Flight!.isLanding)
                                    {
                                        status = " Gate 7\nDrops off passengers";
                                        cnvAdd7.Children.Add(UiItem.Image);
                                        txtbFN7.Foreground = Brushes.DarkRed;
                                        txtbFN7.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                    else
                                    {
                                        status = " Gate 7\npicks up passengers";
                                        cnvAdd7.Children.Add(UiItem.Image);
                                        txtbFN7.Foreground = Brushes.Yellow;
                                        txtbFN7.Text = UiItem.Plane?.Flight?.FlightNumber;
                                    }
                                }
                                break;
                            case 8:
                                {
                                    status = " Taxi for Runaway";
                                    cnvAdd8.Children.Add(UiItem.Image);
                                    txtbFN8.Text = UiItem.Plane?.Flight?.FlightNumber;
                                }
                                break;
                            case 9:
                                {
                                    status = " Departured";
                                    cnvAdd9.Children.Add(UiItem.Image);
                                    txtbFN9.Text = UiItem.Plane?.Flight?.FlightNumber;
                                }
                                break;

                        }
                        lvPlanes.Items.Add($"Flight: {item.Flight?.FlightNumber}\nStatus:{status}");
                    }
                });

                Thread.Sleep(1000);
            });
            await hubConnection.StartAsync();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick!;
            timer.Start();

        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            await hubConnection.SendAsync("Send");
        }
    }
}
