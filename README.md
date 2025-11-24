# Flight-Control-Simulator

A robust real-time simulation of an airport's air traffic control system. The project demonstrates a distributed architecture separating the Logic/Server, the User Interface (Client), and a Traffic Simulator.

## 🚀 Key Features
* **Real-Time Communication:** Uses **SignalR** to push live updates (plane movements, status changes) from the server to the monitoring client.
* **Complex Logic & Concurrency:** The server manages a state machine for each aircraft, handling critical sections (runways/stations) where only one plane is allowed at a time (using Semaphores/Locks).
* **REST API:** Exposes endpoints for external simulators to inject new flights into the system.
* **Persistence:** Saves flight history and current airport state to **SQL Server** using **Entity Framework**.

## 🛠 Tech Stack
* **Backend:** C# .NET, Web API, SignalR Hubs.
* **Frontend:** WPF (Windows Presentation Foundation).
* **Database:** SQL Server, Entity Framework Core.
* **Architecture:** Layered Architecture (Logic, Data, API, Client).

## 🏗 Architecture Overview
The solution consists of four main components:
1.  **FlightControlWebAPI (Server):** The core logic manager. Holds the `FlightLogic` and manages airport resources.
2.  **Simulator:** A console application that generates random flights and sends them to the Server via API.
3.  **FlightControl-GUI:** A WPF application visualizing the airport map and flight status via SignalR.
4.  **CommonModels:** Shared library for data entities (Flight, Plane, Station).
