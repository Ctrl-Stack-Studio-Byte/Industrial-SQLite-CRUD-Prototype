# Industrial SQLite CRUD Prototype

A professional C#/.NET Windows application demonstrating industrial data persistence patterns using SQLite.

## 🚀 Project Overview
This prototype simulates a **Data Historian** or **Edge Gateway** logic, where real-time sensor data is captured, validated, and stored in a local database for traceability and historical analysis.

## ✨ Key Features
- **Auto-Initializing Schema**: Automatically creates the SQLite database and table structure on the first run.
- **Industrial Data Model**: Implements a `DeviceDataModel` including TagName, Value, Quality (Good/Bad), and high-precision Timestamps.
- **Layered Architecture**: Decoupled Data Access Layer (DAL) to ensure code maintainability.
- **Error Handling**: Robust connection management for local file-based database.

## 🛠️ Tech Stack
- **Language**: C#
- **Framework**: .NET (WPF)
- **Database**: SQLite (System.Data.SQLite)

## 📖 Getting Started
1. Clone the repository.
2. Ensure **System.Data.SQLite** NuGet package is installed.
3. Set the build target to **x64** (required for SQLite interop DLLs).
4. Run the application to start logging simulated data.

---
*Developed for Industrial Automation Software Portfolio.*
