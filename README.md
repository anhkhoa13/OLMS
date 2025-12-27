# OLMS - Online Learning Management System

A comprehensive Learning Management System built with Clean Architecture and Domain-Driven Design (DDD) principles. The system provides a complete platform for managing online courses, quizzes, assignments, and student-instructor interactions.

## 📋 Table of Contents

- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
- [Configuration](#configuration)

---

## 🏗️ Architecture

This project implements **Clean Architecture** combined with **Domain-Driven Design (DDD)** principles to ensure:

- **Separation of Concerns**: Each layer has distinct responsibilities
- **Testability**: Business logic is independent of frameworks and external dependencies
- **Maintainability**: Changes to one layer don't cascade through the system
- **Domain-Centric Design**: Business logic and rules are prioritized

### Architectural Principles

1. **Dependency Inversion**: All dependencies point inward toward the domain layer
2. **Aggregate Roots**: Domain entities are organized as aggregates (Course, User, Quiz, Forum, etc.)
3. **Value Objects**: Type-safe primitives (Email, Username, Password, Code, FullName)
4. **Repository Pattern**: Data access abstraction through interfaces
5. **CQRS Pattern**: Separation of commands and queries using MediatR
6. **Unit of Work**: Transaction management for data consistency

---

## 📁 Project Structure

The solution is organized into the following layers:

### 1. **OLMS.Domain** (Core Layer)

The innermost layer containing enterprise business rules and domain models.

- **Entities/**: Domain entities organized by aggregates
  - `CourseAggregate/`: Course, enrollment, and related entities
  - `ForumAggregate/`: Forum, posts, comments, votes
  - `QuizEntity/`: Quiz, questions, quiz attempts
  - `StudentAggregate/`: Student entity and related domain logic
  - `InstructorAggregate/`: Instructor entity and capabilities
  - `SectionEntity/`: Course sections and section items
  - `AssignmentEntity/`: Assignments and exercises
  - `ProgressAggregate/`: Student progress tracking
- **ValueObjects/**: Type-safe domain primitives (Email, Username, Password, Code, FullName)
- **Primitives/**: Base classes (Entity, AggregateRoot)
- **Repositories/**: Repository interfaces (IUnitOfWork, ICourseRepository, etc.)
- **Result/**: Result pattern for error handling

**Key DDD Patterns:**

- Aggregate roots encapsulate related entities
- Value objects ensure domain integrity
- Rich domain models with business logic

### 2. **OLMS.Application** (Use Cases Layer)

Application business rules and orchestration logic.

- **Features/**: Use cases organized by domain area
  - `User/`: User registration, login, profile management
  - `CourseUC/`: Course creation, update, material upload
  - `StudentUC/`: Course enrollment, dashboard, exercise submission
  - `InstructorUC/`: Course management, grading, student oversight
  - `QuizUC/`: Quiz creation, questions management, submission
  - `ForumUC/`: Post creation, voting, commenting
  - `LessonUC/`: Lesson and assignment management
  - `SectionUC_2/`: Section organization and ordering
  - `AnnouncementUC/`: Course announcements
  - `AdminUC/`: Course approval and administration
- **Services/**: Application services (AuthService, JwtService, GradeAnswerService)
- **QuerySide/**: CQRS query handlers and DTOs
- **DependencyInjection.cs**: Application layer service registration

**Technologies:**

- **MediatR**: CQRS implementation (commands and queries)
- **FluentValidation**: Input validation
- **Mapster**: Object mapping

### 3. **OLMS.Infrastructure** (Data & External Services Layer)

Implementation of data access and external dependencies.

- **Database/**: DbContext and configurations
  - `ApplicationDbContext`: Entity Framework Core context
  - **Repositories/**: Concrete repository implementations
- **Migrations/**: Entity Framework database migrations
- **DependencyInjection.cs**: Infrastructure service registration

**Technologies:**

- **Entity Framework Core**: ORM
- **SQL Server**: Database provider

### 4. **OLMS.API** (Presentation Layer - Backend)

RESTful API endpoints and HTTP concerns.

- **Controllers/**: API endpoints
  - `AuthenticationController`: User authentication
  - `CourseController`: Course management
  - `StudentController`: Student operations
  - `InstructorController`: Instructor operations
  - `QuizController`: Quiz management
  - `ForumController`: Discussion forums
  - `PostController`: Forum posts
  - `LessonController`: Lessons and materials
  - `SectionController`: Course sections
  - `AssignmentController`: Assignments
  - `AnnouncementsController`: Announcements
  - `AdminController`: Admin operations
  - `UserController`: User profile
- **Middleware/**: Global exception handler
- **Program.cs**: API configuration and startup

**Features:**

- JWT authentication
- CORS configuration
- Global exception handling
- Swagger documentation

### 5. **olms-react-web** (Frontend - SPA)

Modern React single-page application for user interface.

- **src/**
  - **pages/**: Main application pages (Homepage, Login, Signup, Dashboard, Explore)
  - **features/**: Feature-specific components
    - `Course/`: Course views, enrollment, editing
    - `Quiz/`: Quiz taking and creation
    - `Forum/`: Discussion forum interface
    - `Lesson/`: Lesson viewing and assignments
    - `Section/`: Section management
    - `Admin/`: Administrative interfaces
    - `Announcement/`: Announcements display
  - **components/**: Reusable UI components
  - **contexts/**: React Context (AuthContext)
  - **layouts/**: Layout components (Layout, CourseLayout)
  - **utils/**: Utilities (RoleProtectedRoute, API clients)

**Technologies:**

- **React 19**: UI library
- **React Router**: Client-side routing
- **Axios**: HTTP client
- **React Hook Form**: Form management
- **Yup**: Form validation
- **Tailwind CSS**: Styling
- **Vite**: Build tool

### 6. **OLMS.Shared**

Shared DTOs and contracts between layers.

### 7. **OLMS.Testing**

Testing projects for unit and integration tests.

- **UnitTests/**: Domain and application logic tests
- **IntegrationTests/**: API and database integration tests

---

## ✨ Features

### For Students

- 🎓 **Course Enrollment**: Browse and enroll in available courses
- 📚 **Learning Dashboard**: Track enrolled courses and progress
- 📖 **Course Content**: Access lessons, materials, and resources
- ✍️ **Quizzes**: Take quizzes and receive instant feedback
- 📝 **Assignments**: Submit assignments with file attachments
- 💬 **Discussion Forums**: Participate in course discussions
- 🔔 **Announcements**: Receive course updates from instructors
- 📊 **Progress Tracking**: Monitor learning progress

### For Instructors

- 📚 **Course Creation**: Create and manage courses
- 📑 **Content Management**: Organize courses into sections and lessons
- 📹 **Material Upload**: Add videos, documents, and resources
- 🧪 **Quiz Builder**: Create quizzes with multiple question types
- ✅ **Assignment Management**: Create and grade assignments
- 👥 **Student Management**: View enrolled students and track progress
- 📢 **Announcements**: Communicate with students
- ⭐ **Grading**: Grade student submissions and provide feedback
- 🗂️ **Section Organization**: Drag-and-drop section ordering

### For Administrators

- ✅ **Course Approval**: Review and approve instructor-created courses
- 👤 **User Management**: Manage system users
- 🔧 **System Administration**: Oversee platform operations

### Core Features

- 🔐 **Authentication & Authorization**: JWT-based security with role-based access
- 🎨 **Modern UI**: Responsive design with Tailwind CSS
- 🔄 **Real-time Updates**: Dynamic content updates
- 📱 **Responsive Design**: Works on desktop and mobile devices
- 🌐 **RESTful API**: Clean API architecture
- 💾 **Data Persistence**: SQL Server database with Entity Framework Core

---

## 🛠️ Technology Stack

### Backend

- **.NET 8**: Web API framework
- **Entity Framework Core**: ORM and data access
- **MediatR**: CQRS and mediator pattern
- **FluentValidation**: Input validation
- **Mapster**: Object-object mapping
- **SQL Server**: Relational database
- **JWT**: Authentication tokens

### Frontend

- **React 19**: UI framework
- **Vite**: Build tool and dev server
- **React Router 7**: Client-side routing
- **Axios**: HTTP requests
- **React Hook Form**: Form management
- **Yup**: Schema validation
- **Tailwind CSS 4**: Utility-first CSS
- **Lucide React**: Icon library
- **date-fns**: Date manipulation
- **@dnd-kit**: Drag and drop functionality

---

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- **.NET 8 SDK** or later - [Download](https://dotnet.microsoft.com/download)
- **Node.js 18+** and **npm** - [Download](https://nodejs.org/)
- **SQL Server** (LocalDB, Express, or Full) - [Download](https://www.microsoft.com/sql-server/sql-server-downloads)
- **Visual Studio 2022** or **VS Code** (recommended)
- **Git** - [Download](https://git-scm.com/)

### Installation

#### 1. Clone the Repository

```bash
git clone <repository-url>
cd OLMS
```

#### 2. Database Setup

1. Open [appsettings.json](OLMS.API/appsettings.json)
2. Update the connection string to match your SQL Server instance:

```json
"ConnectionStrings": {
  "TestConnection": "Server=localhost;Database=OLMSTest;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Apply database migrations:

```bash
cd OLMS.API
dotnet ef database update --project ../OLMS.Infrastructure
```

Alternatively, the database will be created automatically on first run.

#### 3. Backend Setup (API)

```bash
# Navigate to API project
cd OLMS.API

# Restore dependencies
dotnet restore

# Run the API
dotnet run
```

The API will start at:

- **HTTPS**: https://localhost:7212
- **HTTP**: http://localhost:5000

Swagger documentation available at: https://localhost:7212/swagger

#### 4. Frontend Setup (React)

Open a new terminal window:

```bash
# Navigate to React project
cd olms-react-web

# Install dependencies
npm install

# Start development server
npm run dev
```

The frontend will start at:

- **Local**: http://localhost:5173

#### 5. Access the Application

1. Open your browser and navigate to **http://localhost:5173**
2. Create a new account (Signup page)
3. Login with your credentials
4. Explore the features based on your role (Student/Instructor)

### Default Test Accounts

After database initialization, you can create test accounts:

**Student Account:**

- Register with role: `Student`

**Instructor Account:**

- Register with role: `Instructor`

**Admin Account:**

- Requires manual database setup or admin creation endpoint

---

## ⚙️ Configuration

### Backend Configuration (appsettings.json)

```json
{
  "ConnectionStrings": {
    "TestConnection": "Your SQL Server connection string"
  },
  "JwtSettings": {
    "Secret": "Your-Secret-Key-Min-32-Characters",
    "Issuer": "https://localhost:7212",
    "Audience": "https://localhost:7001",
    "ExpirationMinutes": 60
  },
  "CorsSettings": {
    "OLMSOrigin": "https://localhost:7212"
  }
}
```

### Frontend Configuration

Update API endpoint in [olms-react-web/src/utils/](olms-react-web/src/utils/) or environment files if needed.

Default API URL: `http://localhost:5000`

### Building for Production

#### Backend

```bash
cd OLMS.API
dotnet publish -c Release -o ./publish
```

#### Frontend

```bash
cd olms-react-web
npm run build
```

The production build will be in the `dist/` folder.

---

## 🧪 Running Tests

```bash
# Run all tests
dotnet test

# Run unit tests only
dotnet test OLMS.Testing/OLMS.Testing.csproj --filter Category=Unit

# Run integration tests
dotnet test OLMS.Testing/OLMS.Testing.csproj --filter Category=Integration
```
