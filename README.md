User Management API

This project is a User Management API built using .NET 8 and C# 12, following Clean Architecture principles. The API provides functionality for user registration, login, profile management, and role-based authorization. It uses modern development practices such as the Repository and Unit of Work patterns, JWT tokens for authentication, and hashed passwords for security.

Features

User Registration: Create new users with a username, email, and password.
User Login: Authenticate users using JWT tokens.
User Profile Management: Retrieve and update user profile information.
Role-Based Authorization: Support for Admin and User roles.
Password Hashing: Secure storage of passwords using bcrypt or PBKDF2.
API Documentation: Interactive API documentation and testing using Swagger.

Technologies
.NET 8
C# 12
ASP.NET Core Web API
Entity Framework Core (Code First)
SQL Server / Postgres
Swagger (Swashbuckle.AspNetCore)
Serilog or NLog for logging
FluentValidation for input validation

Project Structure
The project is organized into the following layers:

Domain: Contains core entities, interfaces, and domain logic.
Application: Implements use cases and application logic. Uses CQRS pattern and MediatR for request handling.
Infrastructure: Contains implementations for data access, repositories, and external services. Uses Entity Framework Core for database operations.
API: The presentation layer exposing the HTTP endpoints. Configures middleware, dependency injection, and Swagger.

Prerequisites
.NET 8 SDK
IDE of your choice (Visual Studio, VS Code, etc.)

Installation
Clone the repository:
bash
Copy code
git clone https://github.com/yourusername/usermanagementapi.git
cd usermanagementapi

@ShotaOrmotsadze
