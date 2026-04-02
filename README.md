# QusayShopApi 🛒

A robust, enterprise-level Full-Stack E-commerce Backend API built with **ASP.NET Core**. This project follows the **N-Layer Architecture** (PL, BLL, DAL) and implements modern software design patterns like the **Repository Pattern** to ensure scalability, maintainability, and clean code.

## 🚀 Key Features

- **🔐 Robust Identity & Security**: 
  - JWT-based authentication and authorization.
  - Role-based access control (Admin, Customer).
  - Password lockout and security token providers using ASP.NET Core Identity.
- **📦 Product Management**: 
  - Comprehensive catalog management for Products, Brands, and Categories.
  - Image upload and file management services.
- **🛒 Shopping Experience**:
  - Fully functional Shopping Cart system.
  - Seamless Checkout process integrated with **Stripe API** for secure payments.
- **📝 Order & Reviews**:
  - Detailed Order processing and tracking.
  - Product Review and Rating system for customer feedback.
- **📊 Reporting**:
  - Automatic **PDF Report Generation** for orders and invoices.
- **🛠 Developer Tools**:
  - Interactive API documentation using **Scalar**.
  - Automated data seeding for initial setup.
  - Global Exception handling and CORS support.

## 🏗 Architecture

The project is structured into three main layers:

1.  **QusayShopApi.PL (Presentation Layer)**: The entry point (Web API). Handles HTTP requests, controllers, and API configurations.
2.  **QusayShopApi.BLL (Business Logic Layer)**: Contains the core business logic, services, and domain-specific rules.
3.  **QusayShopApi.DAL (Data Access Layer)**: Manages database interactions, entities, repositories, and migrations.

## 🛠 Tech Stack

- **Framework**: ASP.NET Core 9.0+
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Tokens) & ASP.NET Core Identity
- **Payments**: Stripe SDK
- **Documentation**: Scalar (Modern OpenAPI/Swagger alternative)
- **Reporting**: QuestPDF / iTextSharp (Service-based PDF generation)

## ⚙️ Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/Qusay-1/Full-Stack-BackEnd-QusayShopApi.git
    cd Full-Stack-BackEnd-QusayShopApi
    ```

2.  **Configure the Database**:
    Update the `ConnectionString` in `QusayShopApi.PL/appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefultConnection": "Server=YOUR_SERVER; Database=QusayShopDb; Integrated Security=True;"
    }
    ```

3.  **Apply Migrations**:
    ```bash
    dotnet ef database update --project QusayShopApi.DAL --startup-project QusayShopApi.PL
    ```

4.  **Run the Application**:
    ```bash
    dotnet run --project QusayShopApi.PL
    ```

## 📖 API Documentation

Once the application is running, you can explore and test the API using Scalar:
- **URL**: `https://localhost:PORT/scalar/v1`

## 🤝 Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please feel free to open an issue or submit a pull request.

---
Developed by **Qusay** 💻
