# Hospital Appointment Booking System

A **full-stack web application** for managing hospital appointments for patients, doctors, and administrators.  
Built with **ASP.NET Core Web API**, **Angular**, and **SQL Server**, following clean architecture principles.

---

## 🚀 Features
- **JWT Authentication & Role-Based Authorization**  
  - Separate dashboards for patients, doctors, and administrators.
- **Appointment Management**  
  - Book, update, or cancel appointments easily.
- **Clean and Responsive UI**  
  - Built with Angular and Bootstrap for a seamless user experience.
- **Optimized Backend**  
  - Repository-Service-Controller pattern for clean code and maintainability.
- **Secure Data Management**  
  - Entity Framework Core for migrations and data handling.

---

## 🛠 Tech Stack
**Frontend:** Angular, TypeScript, Bootstrap  
**Backend:** ASP.NET Core Web API, C#, LINQ  
**Database:** SQL Server, Entity Framework Core  
**Tools:** Visual Studio, VS Code, Postman, Git  

---

## 📂 Project Structure
hospital-appointment-booking/
│
├── backend/ # ASP.NET Core Web API
│ ├── Controllers/
│ ├── Models/
│ ├── Repositories/
| ├── Interfaces/
│ └── Services/
│
├── frontend/ # Angular application
│ ├── src/app/
│ └── assets/
│
└── README.md # Project documentation

## ⚙️ Getting Started
1. **Clone the repository**
2. **Backend setup**
   i) Open the backend folder in Visual Studio.
  ii) Update the appsettings.json file with your SQL Server connection string.
        Run migrations:
        dotnet ef database update
 iii) Start the API:
        dotnet run
4. **Frontend setup**
   i) Open the frontend folder in VS Code.
  ii) Install dependencies:
        npm install
 iii) Run the app:
        ng serve
  iv) Navigate to http://localhost:4200 in your browser.
