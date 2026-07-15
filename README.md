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
cd Roadspoke