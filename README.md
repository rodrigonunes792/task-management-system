# Task Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue.svg)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![DDD](https://img.shields.io/badge/Pattern-DDD-orange.svg)](https://en.wikipedia.org/wiki/Domain-driven_design)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A professional task management system built with Clean Architecture and Domain-Driven Design (DDD) principles, demonstrating enterprise-level software development practices.

**Author:** Rodrigo Nunes

## 🏗️ Architecture Overview

This project implements Clean Architecture with clear separation of concerns across four layers:

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│                   (TaskManagement.API)                   │
│              Controllers, Middleware, Filters            │
└───────────────────────┬─────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────┐
│                   Application Layer                      │
│              (TaskManagement.Application)                │
│        Commands, Queries, DTOs, Validators, Mappings    │
└───────────────────────┬─────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────┐
│                     Domain Layer                         │
│                (TaskManagement.Domain)                   │
│     Entities, Value Objects, Aggregates, Domain Events  │
└─────────────────────────────────────────────────────────┘
                        ▲
┌───────────────────────┴─────────────────────────────────┐
│                 Infrastructure Layer                     │
│             (TaskManagement.Infrastructure)              │
│        Persistence, External Services, Repositories      │
└─────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

1. **Domain Layer** (Core Business Logic)
   - Entities and Aggregates
   - Value Objects
   - Domain Events
   - Business Rules
   - Repository Interfaces

2. **Application Layer** (Use Cases)
   - CQRS Commands and Queries
   - DTOs (Data Transfer Objects)
   - AutoMapper Profiles
   - FluentValidation Validators
   - Application Services

3. **Infrastructure Layer** (External Concerns)
   - Entity Framework Core DbContext
   - Repository Implementations
   - External Service Integrations
   - File System Access
   - Email Services

4. **Presentation Layer** (API)
   - REST API Controllers
   - SignalR Hubs (Real-time notifications)
   - Middleware
   - Filters and Attributes
   - Swagger Configuration

## 🚀 Features

### Core Functionality
- ✅ **Project Management** - Create and manage projects with teams
- ✅ **Task Management** - Full CRUD operations for tasks
- ✅ **Sprint Planning** - Agile sprint management
- ✅ **Kanban Boards** - Visual task organization
- ✅ **User Management** - Team members and roles
- ✅ **Real-time Updates** - SignalR for live notifications
- ✅ **Comments & Attachments** - Task collaboration features
- ✅ **Time Tracking** - Log work hours on tasks
- ✅ **Reporting** - Burndown charts, velocity tracking

### Technical Features
- ✅ **Clean Architecture** - Maintainable and testable code structure
- ✅ **Domain-Driven Design** - Rich domain models with business logic
- ✅ **CQRS Pattern** - Separate read and write operations
- ✅ **MediatR** - In-process messaging for commands/queries
- ✅ **FluentValidation** - Robust input validation
- ✅ **AutoMapper** - Object-to-object mapping
- ✅ **Entity Framework Core** - ORM with migrations
- ✅ **Repository Pattern** - Data access abstraction
- ✅ **Unit of Work** - Transaction management
- ✅ **SignalR** - Real-time communication
- ✅ **Swagger/OpenAPI** - Interactive API documentation
- ✅ **Health Checks** - Service monitoring
- ✅ **Logging** - Structured logging with Serilog

## 🛠️ Technology Stack

- **.NET 8.0** - Latest LTS framework
- **ASP.NET Core Web API** - RESTful API
- **Entity Framework Core 8.0** - ORM
- **MediatR** - CQRS implementation
- **FluentValidation** - Validation library
- **AutoMapper** - Object mapping
- **SignalR** - Real-time communication
- **Serilog** - Structured logging
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Unit testing
- **Moq** - Mocking framework
- **FluentAssertions** - Test assertions

## 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) or [PostgreSQL](https://www.postgresql.org/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/) (optional)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/task-management-system.git
cd task-management-system
```

### 2. Update Database Connection

Edit `src/TaskManagement.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Apply Database Migrations

```bash
cd src/TaskManagement.API
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- **HTTPS**: https://localhost:7001
- **HTTP**: http://localhost:5001
- **Swagger UI**: https://localhost:7001/swagger

## 📚 API Documentation

### Projects

```http
GET    /api/projects              # Get all projects
GET    /api/projects/{id}         # Get project by ID
POST   /api/projects              # Create new project
PUT    /api/projects/{id}         # Update project
DELETE /api/projects/{id}         # Delete project
```

### Tasks

```http
GET    /api/tasks                 # Get all tasks
GET    /api/tasks/{id}            # Get task by ID
POST   /api/tasks                 # Create new task
PUT    /api/tasks/{id}            # Update task
DELETE /api/tasks/{id}            # Delete task
PATCH  /api/tasks/{id}/status     # Update task status
PATCH  /api/tasks/{id}/assign     # Assign task to user
```

### Sprints

```http
GET    /api/sprints               # Get all sprints
GET    /api/sprints/{id}          # Get sprint by ID
POST   /api/sprints               # Create new sprint
PUT    /api/sprints/{id}          # Update sprint
POST   /api/sprints/{id}/start    # Start sprint
POST   /api/sprints/{id}/complete # Complete sprint
```

### Example Requests

#### Create a Project

```bash
curl -X POST "https://localhost:7001/api/projects" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "E-Commerce Platform",
    "description": "Build a new e-commerce platform",
    "startDate": "2024-01-01",
    "endDate": "2024-12-31"
  }'
```

#### Create a Task

```bash
curl -X POST "https://localhost:7001/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Implement user authentication",
    "description": "Add JWT-based authentication",
    "projectId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "priority": "High",
    "estimatedHours": 8
  }'
```

## 🧪 Testing

### Run All Tests

```bash
dotnet test
```

### Run with Coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverageReportFormat=opencover
```

### Test Structure

```
tests/
├── TaskManagement.Domain.Tests/       # Domain logic tests
├── TaskManagement.Application.Tests/  # Use case tests
└── TaskManagement.API.Tests/          # Integration tests
```

## 🐳 Docker Support

### Build Docker Image

```bash
docker build -t task-management-api -f src/TaskManagement.API/Dockerfile .
```

### Run with Docker Compose

```bash
docker-compose up -d
```

## 📊 Project Structure

```
task-management-system/
├── src/
│   ├── TaskManagement.Domain/
│   │   ├── Entities/
│   │   │   ├── Project.cs
│   │   │   ├── Task.cs
│   │   │   ├── Sprint.cs
│   │   │   └── User.cs
│   │   ├── ValueObjects/
│   │   ├── Events/
│   │   └── Interfaces/
│   ├── TaskManagement.Application/
│   │   ├── Commands/
│   │   │   ├── CreateProject/
│   │   │   ├── CreateTask/
│   │   │   └── UpdateTask/
│   │   ├── Queries/
│   │   │   ├── GetProjects/
│   │   │   └── GetTasks/
│   │   ├── DTOs/
│   │   ├── Mappings/
│   │   └── Validators/
│   ├── TaskManagement.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   └── Migrations/
│   │   ├── Repositories/
│   │   └── Services/
│   └── TaskManagement.API/
│       ├── Controllers/
│       ├── Hubs/
│       ├── Middleware/
│       └── Program.cs
├── tests/
├── docs/
├── docker-compose.yml
└── README.md
```

## 🎯 Design Patterns & Principles

### Patterns Implemented
- **Clean Architecture** - Dependency inversion and separation of concerns
- **Domain-Driven Design** - Rich domain models
- **CQRS** - Command Query Responsibility Segregation
- **Repository Pattern** - Data access abstraction
- **Unit of Work** - Transaction management
- **Mediator Pattern** - Decoupled communication (MediatR)
- **Specification Pattern** - Reusable query logic
- **Factory Pattern** - Object creation

### SOLID Principles
- **S**ingle Responsibility Principle
- **O**pen/Closed Principle
- **L**iskov Substitution Principle
- **I**nterface Segregation Principle
- **D**ependency Inversion Principle

## 🔧 Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "SecretKey": "your-secret-key-here",
    "Issuer": "TaskManagementAPI",
    "Audience": "TaskManagementClient",
    "ExpirationMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 📈 Database Schema

### Main Entities

- **Projects** - Project information and settings
- **Tasks** - Individual work items
- **Sprints** - Time-boxed iterations
- **Users** - Team members
- **Comments** - Task discussions
- **Attachments** - File uploads
- **WorkLogs** - Time tracking entries

## 🚧 Roadmap

- [ ] Add email notifications
- [ ] Implement file upload for attachments
- [ ] Add advanced reporting dashboard
- [ ] Implement task dependencies
- [ ] Add Gantt chart view
- [ ] Implement webhooks
- [ ] Add API rate limiting
- [ ] Implement caching with Redis
- [ ] Add GraphQL endpoint
- [ ] Mobile app integration

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Rodrigo Nunes**

- GitHub: [@rodrigonunes792](https://github.com/rodrigonunes792)
- LinkedIn: [Rodrigo Nunes](https://www.linkedin.com/in/rodrigonunes79/)

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- Domain-Driven Design by Eric Evans
- Microsoft .NET Documentation
- MediatR by Jimmy Bogard

---

⭐ If you find this project useful, please consider giving it a star!
