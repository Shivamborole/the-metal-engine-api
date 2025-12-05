🚀 Metal Engine – Smart Invoicing for CNC & Metal Fabrication

A full-stack invoicing platform built for manufacturing, CNC laser tube cutting, and metal fabrication businesses.
Designed to generate invoices, delivery challans, rejection notes, and email PDF documents with a clean UI and enterprise-grade backend.

📌 Live Demo

🔹 Frontend (Angular): Coming soon
🔹 Backend API (Azure): Coming soon

🧱 Tech Stack
Frontend

Angular 17

SCSS Modular Styling

Responsive Metal-Engine UI

Backend

ASP.NET Core 8 Web API

Entity Framework Core

SQLite (demo) / SQL Server (local dev)

JWT Authentication

PDF Generation & Emailing

Infrastructure

Azure App Service (Free Tier)

Netlify Hosting for Angular

GitHub Actions CI/CD

Brevo SMTP for email sending

📸 Screenshots

Add screenshots here later.

⚙️ Features

Create + manage invoices

Delivery Challan module

Rejection note module

PDF generation

Email invoice via SMTP

User authentication + role-based modules

Clean UI with modern design

🧪 Running Locally
Backend
dotnet restore
dotnet ef database update
dotnet run

Frontend
npm install
ng serve -o

🧱 Project Architecture
API/
 ┣ Application/
 ┣ Domain/
 ┣ Infrastructure/
 ┣ Persistence/
 ┣ Controllers/
UI/
 ┣ src/
 ┣ app/
 ┣ components/

☁ Deployment

Azure App Service for backend

Netlify for frontend

GitHub Actions for CI/CD

📬 Email Sending (SMTP)

Using Brevo free SMTP (300 emails/day).

💼 Author

Shivam Borole
Full Stack .NET + Angular Engineer
