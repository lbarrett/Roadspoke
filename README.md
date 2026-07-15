# Roadspoke Demo

Welcome to the **Roadspoke** demo repository! Roadspoke is a GPS-triggered, hands-free audio tour application designed to turn long road trips and interstate drives into interactive talking tours and curated shopping experiences. 

This repository contains the core codebase for the Roadspoke .NET-based web backend and service layer.

---

## 🏗️ Architecture Overview

The solution follows a classic multi-tier architecture to separate business logic, data management, and the user-facing web services:

*   **`RoadSpoken.sln`**: The main Visual Studio solution file containing all sub-projects.
*   **`BAL` (Business Access Layer)**: Contains the core business logic, validation rules, and service orchestration.
*   **`DAL` (Data Access Layer)**: Handles interactions with the database, repository patterns, and data persistence.
*   **`RoadSpokenService`**: An ASP.NET Web Service (API) providing endpoints for GPS-trigger logic, tour metadata, and backend integrations.
*   **`RoadSpokenWeb`**: The web front-end containing the administration portal, UI assets, and presentation files.

---

## 🚀 Getting Started

Follow these steps to set up your environment and run the Roadspoke demo locally.

### Prerequisites

To build and run this application, you will need:
*   [Visual Studio](https://visualstudio.microsoft.com/) (version 2019 or later recommended) or [JetBrains Rider](https://www.jetbrains.com/rider/).
*   [.NET Framework SDK](https://dotnet.microsoft.com/download/dotnet-framework) (compatible with the `.sln` and `.suo` configuration).
*   An active database instance (SQL Server or equivalent) if you intend to configure real-time data storage via the DAL.

---

## 🛠️ Step-by-Step Setup

### 1. Clone the Repository
Clone this repository to your local machine:
```bash
git clone [https://github.com/lbarrett/Roadspoke.git](https://github.com/lbarrett/Roadspoke.git)

2. Open the Solution
To open the project in your IDE:

Open Visual Studio.

Go to File > Open > Project/Solution.

Navigate to your cloned folder and select RoadSpoken.sln to load the workspace.

3. Restore NuGet Packages
Restore all external dependencies required by the layers:

Right-click on the solution RoadSpoken at the top of the Solution Explorer pane.

Select Restore NuGet Packages from the context menu.

Alternatively, open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console) and run:

PowerShell
Update-Package -reinstall
4. Database & Configuration
Before starting the web services, configure your connection strings:

Locate the web.config or app.config file inside RoadSpokenService and RoadSpokenWeb.

Modify the connection strings to point to your local SQL Server instance.

If database initialization scripts are included in the DAL, run them against your target database.

5. Running the Application
To run both the frontend and web services simultaneously:

Right-click the Solution in the Solution Explorer and select Properties.

Under Common Properties, select Startup Project.

Choose Multiple startup projects.

Set the Action for both RoadSpokenService and RoadSpokenWeb to Start.

Press F5 or click Start in the toolbar to run the solution in your browser.

📱 How the Demo Works
Once running, the system demonstrates the core flow of the Roadspoke experience:

The Web API (RoadSpokenService) simulates how GPS coordinates sent from a mobile client trigger specific location-based tour audio track waypoints.

The Web UI (RoadSpokenWeb) serves as the administrative portal to manage tours, define waypoint coordinates, and upload audio assets.

🤝 Contributing
Contributions are welcome! If you find a bug or have a suggestion for improving the demo, please:

Fork the repository.

Create a new branch (git checkout -b feature/AmazingFeature).

Commit your changes (git commit -m 'Add some AmazingFeature').

Push to the branch (git push origin feature/AmazingFeature).

Open a Pull Request.
