# Banking System API

## Overview
This is a **Banking System API** built with **.NET 8**, following best practices in software architecture, security, and maintainability. It provides a secure and efficient way for users to manage their accounts, cards, loans, and transactions.

## Features

### 🔒 Authentication & Authorization
- **JWT Authentication** with **access tokens** and **refresh tokens**.
- **Email confirmation** for new accounts.
- **Password reset** functionality.
- **Role-based access control**:
  - **Admin**: Full access to all resources.
  - **User**: Access only to their owned accounts, cards, and loans.

### 💳 Banking Features
- **Accounts**: Users can create and manage their bank accounts.
- **Cards**: Secure card management.
- **Loans**: Loan application and tracking.
- **Transactions**:
  - Secure **money transfers** between accounts.
  - Automatic **email notifications** for transactions.
  - **Authorization enforcement** to ensure users can only access their own financial data.

### 📧 Email Notification Service
- **Transactional Emails**: Users receive email notifications for **transaction confirmations, password resets, and account-related alerts**.

### ⚡ Technology Stack
- **.NET 8** with **C#**
- **Entity Framework Core** for database access
- **SQL Server** as the database
- **JWT & Refresh Tokens** for authentication
- **AutoMapper** for object mapping

### 🏗 Design Patterns & Best Practices
- **Repository & Unit of Work** patterns for clean data access
- **Dependency Injection (DI)** for maintainability and flexibility
- **SOLID Principles** for a scalable and well-structured codebase

## Getting Started
### Prerequisites
- .NET 8 SDK
- SQL Server

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/MohamedAbdelaziz177/Banking-System-API.git
   ```
2. Navigate to the project directory:
   ```sh
   cd Banking system
   ```
3. Set up the database:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```

## API Documentation
Swagger is enabled for API testing and documentation.
- Access it at: `http://localhost:5205/swagger`

## Contributing
Pull requests are welcome! For major changes, please open an issue first.

## License
MIT License

