# Siemens

A simple ASP.NET Core Web API for managing warehouse inventory, built with .NET 8.

## Features
- **CRUD Operations**: Manage products (Create, Read, Update).
- **Persistent Storage**: Data is saved to a local `inventory.json` file.
- **Architecture**: Implements Dependency Injection and the Service Repository pattern.
- **Validation**: Prevents negative prices or stock quantities.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022

### Installation
1. Clone the repository.
2. Open `Siemens.sln` in Visual Studio.

### Running the API
1. Set `Siemens` as the startup project.
2. Press F5 (or click Run).
3. The Swagger UI will launch automatically at `https://localhost:xxxx/swagger`.
4. Use the Swagger UI to interact with the API endpoints.

### Running Tests
1. Open the Test Explorer in Visual Studio (`Test` > `Test Explorer`).
2. Click "Run All Tests".
3. The project includes unit tests covering the Controller logic using xUnit and Moq.

## Project Structure
- **Controllers**: Handles HTTP requests and input validation.
- **Services**: Contains business logic and file I/O operations.
- **Models**: Defines the data structure (`Product`).
- **Tests**: Contains xUnit tests for the controller.
