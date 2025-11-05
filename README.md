# 🚗 SMART PARKING SYSTEM API
# ASP.NET Core Web API + Clean Architecture + Unit of Work + JWT + Social Login + Email Confirmation + Excel Export

# ------------------------------------------------------------
# 📋 TABLE OF CONTENTS
# ------------------------------------------------------------
# 1. Features
# 2. System Architecture
# 3. Tech Stack
# 4. Project Structure
# 5. Database Design
# 6. Authentication
# 7. Email Notifications
# 8. Reservation Flow
# 9. Unit of Work + Repository
# 10. Export to Excel
# 11. Setup Instructions
# 12. Future Enhancements
# 13. Author
# ------------------------------------------------------------

# 🚀 FEATURES
# ------------------------------------------------------------
# ✅ Register + Email Confirmation
# ✅ Login with JWT (Normal / Social)
# ✅ Roles: Admin / User
# ✅ CRUD for Areas & Spots
# ✅ Reservation System (Active / Completed)
# ✅ Auto-complete expired reservations
# ✅ Export reservations to Excel file
# ✅ Send Email on Register & Reservation
# ✅ Clean Architecture + Unit of Work + Repository Pattern
# ✅ EF Core + SQL Server
# ✅ Integrated Google / Facebook Social Login

# ------------------------------------------------------------
# 🧩 PROJECT STRUCTURE
# ------------------------------------------------------------
# Smart-Parking-System/
# ├── Application/
# │   ├── Dtos/
# │   ├── Enums/
# │   ├── Interfaces/
# │   └── Services/
# │
# ├── DomainLayer/
# │   ├── Entities/
# │   └── Repositories/
# │
# ├── Infrastructure/
# │   ├── Data/
# │   ├── Repositories/
# │   └── Services/
# │       ├── EmailService.cs
# │       ├── ExportService.cs
# │       └── SocialAuthService.cs
# │
# ├── WebAPI/
# │   ├── Controllers/
# │   ├── Middleware/
# │   └── Program.cs
# │
# └── README.md

# ------------------------------------------------------------
# ⚙️ SETUP INSTRUCTIONS
# ------------------------------------------------------------
# 1️⃣ Clone the Repository
git clone https://github.com/tareq70/Smart-Parking-System.git
cd Smart-Parking-System

# 2️⃣ Configure appsettings.json
# Edit connection string, JWT, SMTP, and OAuth providers.

# 3️⃣ Apply Migrations
dotnet ef database update

# 4️⃣ Run the Project
dotnet run

# 📍 Swagger URL: https://localhost:5001/swagger

# ------------------------------------------------------------
# 🔐 AUTHENTICATION
# ------------------------------------------------------------
# JWT + Identity + Roles + Social Login
# Social login supports:
#   - Google
#   - Facebook

# Token Endpoints:
# POST /api/auth/login
# POST /api/auth/external-login/google
# POST /api/auth/external-login/facebook

# ------------------------------------------------------------
# 📧 EMAIL CONFIRMATION
# ------------------------------------------------------------
# - Confirmation email sent after registration
# - Email sent upon reservation creation (with details)
# - Managed by EmailService.cs via SMTP

# ------------------------------------------------------------
# 🅿️ RESERVATION FLOW
# ------------------------------------------------------------
# 1️⃣ User selects parking area + spot
# 2️⃣ Sends POST /api/reservations
# 3️⃣ System checks availability
# 4️⃣ Creates reservation + marks spot as reserved
# 5️⃣ Sends confirmation email
# 6️⃣ Auto-completes expired reservations

# ------------------------------------------------------------
# 🧱 UNIT OF WORK + REPOSITORY
# ------------------------------------------------------------
# Every entity has its own repository implementing GenericRepository<T>.
# Unit of Work ensures a single transaction across operations.

# Example:
# using (var uow = _unitOfWork)
# {
#     await uow.Reservations.CreateAsync(reservation);
#     await uow.SaveAsync();
# }

# ------------------------------------------------------------
# 📊 EXPORT TO EXCEL FEATURE
# ------------------------------------------------------------
# The system supports exporting reservation data to Excel (.xlsx)
# Implemented in ExportService.cs using OpenXML library.
# Used for Admin reports and monitoring activity.

# Endpoint:
# GET /api/reservations/export
# Response: Excel file containing columns
#   - Reservation Id
#   - User Id
#   - Parking Spot
#   - Parking Area
#   - Start / End Time
#   - Status

# ------------------------------------------------------------
# 🧠 TECH STACK
# ------------------------------------------------------------
# Backend         → ASP.NET Core 8 Web API
# Database        → SQL Server + EF Core
# Authentication  → Identity + JWT + Google/Facebook
# Architecture    → Clean Architecture + UoW + Repository
# Email Service   → SMTP (EmailService.cs)
# Excel Export    → OpenXML SDK (ExportService.cs)
# Logging         → Serilog (optional)
# Documentation   → Swagger

# ------------------------------------------------------------
# 🔮 FUTURE ENHANCEMENTS
# ------------------------------------------------------------
# - Real-time updates with SignalR
# - Online payments (InstaPay / Vodafone Cash)
# - Flutter Mobile App (User + Admin)
# - AI-based spot availability prediction
# - Dashboard analytics

# ------------------------------------------------------------
# 👤 AUTHOR
# ------------------------------------------------------------
# Tarek Elsabbagh
# 💻 Backend Developer (.NET)
# 🔗 LinkedIn: https://linkedin.com/in/tarekmmdoh
# 📧 Email: tarekelspagh707@gmail.com
