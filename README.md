# SMART PARKING SYSTEM API
# ASP.NET Core Web API + Clean Architecture + Unit of Work + JWT + Social Login + Email Confirmation

 ------------------------------------------------------------
# Table of Contents
# ------------------------------------------------------------

- [Features](#-features)
- [System Architecture](#-system-architecture)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Database Design](#-database-design)
- [Authentication](#-authentication)
- [Email Notifications](#-email-notifications)
- [API Endpoints](#-api-endpoints)
- [Setup Instructions](#-setup-instructions)
- [Future Enhancements](#-future-enhancements)
- [License](#-license)


# ------------------------------------------------------------
# FEATURES
# ------------------------------------------------------------
# ✅ Register + Email Confirmation
# ✅ Login with JWT (Normal / Social)
# ✅ Roles: Admin / User
# ✅ CRUD for Areas & Spots
# ✅ Reservation System (Active / Completed)
# ✅ Auto-complete expired reservations
# ✅ Export reservations to Excel
# ✅ Send Email on Register & Reservation
# ✅ Clean Architecture + Unit of Work + Repository Pattern
# ✅ EF Core + SQL Server

# ------------------------------------------------------------
# PROJECT STRUCTURE
# ------------------------------------------------------------
.
#├── Application/
#│   ├── Dtos/
#│   ├── Enums/
#│   ├── Interfaces/
#│   └── Services/
#├── DomainLayer/
#│   ├── Entities/
#│   └── Repositories/
#├── Infrastructure/
#│   ├── Data/
#│   ├── Repositories/
#│   └── Services/   # EmailService, SocialAuthService
#├── WebAPI/
#│   ├── Controllers/
#│   ├── Middleware/
#│   └── Program.cs
#└── README.md

# ------------------------------------------------------------
# SETUP
# ------------------------------------------------------------
# 1️⃣ Clone the repo
git clone https://github.com/tareq70/Smart-Parking-System.git
cd Smart-Parking-System

# 2️⃣ Configure appsettings.json
# Edit connection string, JWT, SMTP, and OAuth providers.

# 3️⃣ Apply migrations
dotnet ef database update

# 4️⃣ Run the project
dotnet run
# Swagger: https://localhost:5001/swagger

# ------------------------------------------------------------
# AUTHENTICATION
# ------------------------------------------------------------
# JWT + Identity + Roles + Social Login
# Social login supports:
#  - Google
#  - Facebook
# Tokens are generated via:
#   POST /api/auth/login
#   POST /api/auth/external-login/google
#   POST /api/auth/external-login/facebook

# ------------------------------------------------------------
# EMAIL CONFIRMATION
# ------------------------------------------------------------
# - When user registers, a confirmation email is sent.
# - When reservation is created, user gets email with details.
# - Handled by EmailService.cs via SMTP.

# ------------------------------------------------------------
# RESERVATION FLOW
# ------------------------------------------------------------
# 1️⃣ User selects parking area + spot
# 2️⃣ Sends POST /api/reservations
# 3️⃣ System checks conflicts + spot availability
# 4️⃣ Creates reservation + marks spot as reserved
# 5️⃣ Sends confirmation email
# 6️⃣ Auto-completes expired reservations

# ------------------------------------------------------------
# UNIT OF WORK + REPOSITORY
# ------------------------------------------------------------
# Every entity has its own repository implementing GenericRepository<T>.
# UoW ensures single transaction across multiple operations.

# Example:
# using (var uow = _unitOfWork)
# {
#   await uow.Reservations.CreateAsync(reservation);
#   await uow.SaveAsync();
# }

# ------------------------------------------------------------
# EXPORT FEATURE
# ------------------------------------------------------------
# GET /api/reservations/export
# Exports all reservations (Id, UserId, Spot, Area, Time, Status)
# Generates Excel file via ExportService.cs

# ------------------------------------------------------------
# TECH STACK
# ------------------------------------------------------------
# Backend     → ASP.NET Core 8 Web API
# Database    → SQL Server + EF Core
# Auth        → Identity + JWT + Google/Facebook
# Architecture → Clean Architecture + UoW + Repository
# Email        → SMTP Service
# Logging      → Serilog (optional)
# Docs         → Swagger

# ------------------------------------------------------------
# FUTURE ENHANCEMENTS
# ------------------------------------------------------------
# - Real-time updates with SignalR
# - Online payments (InstaPay / Vodafone Cash)
# - Flutter Mobile App (User + Admin)
# - AI-based spot availability prediction
# - Dashboard analytics

# ------------------------------------------------------------
# AUTHOR
# ------------------------------------------------------------
# 👤 Tarek Elsabbagh
# 💻 Backend Developer (.NET)
# 🔗 LinkedIn: https://linkedin.com/in/tarekmmdoh
# 📧 Email: tarekelspagh707@gmail.com
