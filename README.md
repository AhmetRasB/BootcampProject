# Bootcamp Management System

A comprehensive .NET 9 Web API project for managing bootcamp applications, instructors, applicants, and blacklists.

## Project Structure

The project follows Clean Architecture principles with the following layers:

- **Entities**: Domain entities and enums
- **Data**: Entity Framework configurations and repositories
- **Business**: Business logic, services, and business rules
- **DTOs**: Data Transfer Objects for API requests/responses
- **WebApplication1**: Web API controllers and configuration

## Features

### Entities
- **User**: Base user entity with authentication fields
- **Applicant**: User who can apply to bootcamps
- **Instructor**: User who can create and manage bootcamps
- **Employee**: User with administrative roles
- **Bootcamp**: Training programs with start/end dates
- **Application**: Applications from applicants to bootcamps
- **Blacklist**: System to prevent certain applicants from applying

### Business Rules
- **ApplicantBusinessRules**: Validates applicant operations
- **BootcampBusinessRules**: Validates bootcamp operations
- **ApplicationBusinessRules**: Validates application operations
- **BlacklistBusinessRules**: Validates blacklist operations

### API Endpoints

#### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

#### Applicants
- `GET /api/applicant` - Get all applicants
- `GET /api/applicant/{id}` - Get applicant by ID
- `POST /api/applicant` - Create new applicant
- `PUT /api/applicant/{id}` - Update applicant
- `DELETE /api/applicant/{id}` - Delete applicant

#### Instructors
- `GET /api/instructor` - Get all instructors
- `GET /api/instructor/{id}` - Get instructor by ID
- `POST /api/instructor` - Create new instructor
- `PUT /api/instructor/{id}` - Update instructor
- `DELETE /api/instructor/{id}` - Delete instructor

#### Bootcamps
- `GET /api/bootcamp` - Get all bootcamps
- `GET /api/bootcamp/{id}` - Get bootcamp by ID
- `POST /api/bootcamp` - Create new bootcamp
- `PUT /api/bootcamp/{id}` - Update bootcamp
- `DELETE /api/bootcamp/{id}` - Delete bootcamp

#### Applications
- `GET /api/application` - Get all applications
- `GET /api/application/{id}` - Get application by ID
- `POST /api/application` - Create new application
- `PUT /api/application/{id}/state` - Update application state
- `DELETE /api/application/{id}` - Delete application

#### Blacklist
- `GET /api/blacklist` - Get all blacklist entries
- `GET /api/blacklist/{id}` - Get blacklist entry by ID
- `POST /api/blacklist` - Add applicant to blacklist
- `DELETE /api/blacklist/{id}` - Remove from blacklist

## Setup Instructions

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd WebApplication1
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update connection string**
   Edit `WebApplication1/appsettings.json` and update the connection string to point to your SQL Server instance.

4. **Create and apply migrations**
   ```bash
   cd WebApplication1
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   Navigate to `https://localhost:7001/swagger` to view the API documentation.

## Database Schema

### Users Table (TPH - Table Per Hierarchy)
- Id (Primary Key)
- FirstName
- LastName
- DateOfBirth
- NationalityIdentity (Unique)
- Email (Unique)
- PasswordHash
- PasswordSalt
- UserType (Discriminator)
- CreatedDate
- UpdatedDate
- DeletedDate

### Bootcamps Table
- Id (Primary Key)
- Name (Unique)
- InstructorId (Foreign Key)
- StartDate
- EndDate
- BootcampState
- CreatedDate
- UpdatedDate
- DeletedDate

### Applications Table
- Id (Primary Key)
- ApplicantId (Foreign Key)
- BootcampId (Foreign Key)
- ApplicationState
- CreatedDate
- UpdatedDate
- DeletedDate

### Blacklists Table
- Id (Primary Key)
- Reason
- Date
- ApplicantId (Foreign Key)
- CreatedDate
- UpdatedDate
- DeletedDate

## Business Rules

### Applicant Rules
- Same TC Kimlik No cannot be used for multiple registrations
- Blacklisted applicants cannot register
- Non-existent applicants cannot perform operations

### Bootcamp Rules
- Start date must be before end date
- Bootcamp names must be unique
- Instructor must exist in the system
- Applications cannot be accepted for closed bootcamps

### Application Rules
- Same applicant cannot apply to the same bootcamp multiple times
- Bootcamp must be active for applications
- Blacklisted applicants cannot apply
- Application state transitions follow specific rules

### Blacklist Rules
- Only one active blacklist entry per applicant
- Reason cannot be empty

## Technologies Used

- **.NET 9**: Latest .NET framework
- **Entity Framework Core**: ORM for database operations
- **AutoMapper**: Object-to-object mapping
- **Swagger/OpenAPI**: API documentation
- **SQL Server**: Database
- **Clean Architecture**: Project structure

