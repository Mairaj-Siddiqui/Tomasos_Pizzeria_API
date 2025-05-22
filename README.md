# Tomasos Pizzeria API
This is a full-featured backend Web API for **Tomasos Pizzeria**, built with **ASP.NET Core (.NET 8)** and deployed to **Microsoft Azure**.

# Project Goals
This project was developed to fulfill backend requirements for an online food ordering system, following real-world architecture and cloud deployment practices.

> Live Swagger API:  
> [https://tomasospizzeriaapi20250520103159.azurewebsites.net/swagger/index.html]
>
> ## Features
- User registration, login, update, and delete
- Admin and user role-based JWT authentication
- Create and manage dishes, categories, and orders
- Token-based authorization using **JWT**
- Fully documented with **Swagger UI**
- Clean architecture with Repositories and DTOs
- Built using **Code-First Entity Framework Core**
- SQL Server hosted on **Azure SQL Database**
- Secrets securely managed with **Azure Key Vault**
- Connected using **Managed Identity**
- Deployed via **Azure App Service**

---

## Technologies Used
Tool/Service
.NET 8 (ASP.NET Core) - Backend API Framework         
Entity Framework Core - ORM + Code First Migrations   
Swagger / Swashbuckle - API Documentation             
Microsoft SQL Server - Azure-hosted database         
Azure App Service  - Web API Hosting               
Azure Key Vault  - Secret storage (JWT, conn str)
Azure Identity  - Managed Identity for security 
JWT - Token Authentication 

## Authentication
- Auth is implemented using **JWT Bearer Tokens**
- Token is returned on successful login
- Use the **Authorize button** in Swagger to authenticate
- Protected routes require a valid token

---


Mairaj Siddiqui

Cloud Developer Azure – Tomasos Pizzeria Project
