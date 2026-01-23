# 🎫 Ticket Portal – Support Ticket Management System
 
## 📌 Introduction
 
The **Ticket Portal** is a full-stack web-based application designed to manage support tickets within an organization in a **structured, transparent, and efficient** manner.  
It replaces traditional manual processes such as emails or phone calls with a **centralized digital platform** that enables seamless communication between employees, departments, and administrators.
 
The system supports ticket creation, categorization, assignment, SLA tracking, and conversation-based replies, ensuring issues are resolved within defined timelines.
 
---
 
## 🎯 Project Objectives
 
- Centralize issue reporting and resolution
- Improve communication between ticket creators and assignees
- Enforce Service Level Agreements (SLAs)
- Provide a clean and user-friendly interface
- Maintain a complete audit trail of ticket conversations
- Reduce resolution time and operational overhead
 
---
 
## 🛠️ Technology Stack
 
### Frontend
- Angular (Standalone Components)
- Bootstrap 5
- HTML5, CSS3
- TypeScript
 
### Backend
- ASP.NET Core Web API
- Entity Framework Core
- C#
 
### Database
- Microsoft SQL Server
 
### Tools & Libraries
- Bootstrap Icons
- Angular Router & Forms
- RESTful APIs
- Postman (API Testing)
 
---
 
## 🧩 System Architecture
 
The application follows a **layered architecture**:
 
1. Presentation Layer – Angular UI  
2. Business Logic Layer – ASP.NET Core Services  
3. Data Access Layer – Entity Framework Core  
4. Database Layer – SQL Server  
 
Communication between frontend and backend is achieved through **REST APIs** using JSON.
 
---
 
## 🧩 Functional Modules
 
### 1️⃣ Authentication Module
- User Registration
- Secure Login and Logout
- Role-based access control (Admin / Employee)
 
---
 
### 2️⃣ Department Management
- Create, update, and delete departments
- Store department descriptions
- Map departments to ticket types
 
---
 
### 3️⃣ SLA (Service Level Agreement) Management
- Define response time and resolution time
- Attach SLAs to ticket types
- Track SLA compliance for tickets
 
---
 
### 4️⃣ Ticket Type Management
- Categorize tickets (IT, HR, Admin, etc.)
- Assign departments and SLAs to ticket types
- Enable structured ticket classification
 
---
 
### 5️⃣ Ticket Management
- Create tickets with title and description
- Assign tickets to employees
- Track ticket lifecycle:
  - Open
  - In Progress
  - Resolved
  - Closed
- Maintain creation and resolution timestamps
 
---
 
### 6️⃣ Ticket Reply (Conversation Module)
- Chat-style conversation interface
- Creator replies aligned to the left
- Assignee replies aligned to the right
- Display sender role, message, date, and time
- Complete conversation history per ticket
 
---
 
### 7️⃣ Employee Management
- Add and manage employee records
- Assign roles and departments
- View employee details
 
---
 
## 🖥️ User Interface Design
 
- Professional enterprise-style UI
- Form on the left and data/table on the right
- Soft and consistent color theme
- WhatsApp-style chat interface for ticket replies
- Fully responsive layout
 
---
 
## 🗂️ Project Structure

```text
TicketPortal/
├── Backend/
│   └── TicketPortalAPI/
│       ├── Controllers/
│       ├── Models/
│       ├── Repositories/
│       ├── Services/
│       ├── Data/
│       └── Program.cs
│
├── Frontend/
│   └── src/
│       ├── app/
│       │   ├── components/
│       │   │   ├── department/
│       │   │   ├── ticket/
│       │   │   ├── ticketreply/
│       │   │   ├── employee/
│       │   │   └── sla/
│       │   ├── services/
│       │   └── app.component.*
│       │
│       ├── assets/
│       └── styles.css
│
└── README.md
```

## ⚙️ Installation & Setup

### 🔹 Backend Setup
1. Open the backend project in Visual Studio
2. Configure SQL Server connection string in `appsettings.json`
3. Navigate to the Web API project directory:

```bash
cd Backend/TicketPortalWebAPI
dotnet run
```
```bash
The backend API will start at:
http://localhost:5082
```
---

### 🔹 Frontend Setup (Angular Application)

Follow the steps below to set up and run the Angular frontend of the **Ticket Portal** application.

---

#### ✅ Prerequisites

Ensure the following software is installed on your system:

- **Node.js** (v16 or above recommended)
- **npm** (comes bundled with Node.js)
- **Angular CLI**

Install Angular CLI globally if it is not already installed:

```bash
npm install -g @angular/cli
```
Verify Angular CLI installation:
```bash
ng version
```
## 📁 Navigate to Frontend Directory

From the root project folder, navigate to the frontend directory:
```bash
cd TicketPortal
```

## 📦 Install Dependencies

Install all required npm packages listed in package.json:
```bash
npm install
```
This command will:
- Download Angular core libraries
- Install Bootstrap and UI-related dependencies
- Create the node_modules folder
### ⚙️ Configure API Base URL

- Ensure the frontend services are configured to communicate with the backend Web API.
- Open the Angular service files inside the services/ folder
- Verify that the API base URL matches the backend URL:
```bash
http://localhost:5082/api
```
### ⚠️ Ensure the ASP.NET Core Web API is running before starting the Angular frontend.

### ▶️ Run the Angular Application

- Start the Angular development server:
```bash
ng serve
```
- The application will be available at:
```bash
http://localhost:4200
```
### 🔁 Live Reload Support

- Any changes made to HTML, CSS, or TypeScript files will automatically reload the browser
- No manual refresh is required

### 🛑 Stop the Application
To stop the Angular development server, press:
```bash
Ctrl + C
```
### ⚠️ Common Issues & Solutions

- Backend API not reachable
- Ensure the backend is running using:
```bash
dotnet run
```
- Port already in use
Start Angular on a different port:
```bash
ng serve --port 4300
```
- Module not found or dependency errors
Delete node_modules and reinstall dependencies:
```bash
rm -rf node_modules
npm install
```
### ✅ Frontend Startup Checklist

 - [ ] Node.js installed
 - [ ] Angular CLI installed
 - [ ] Backend Web API running
 - [ ] npm dependencies installed
 - [ ] Angular app running at http://localhost:4200

## 📸 Screenshots

### 🏠 Home Page
![Home Page](screenshots/home.png)

---

### 🔐 Login Page
![Login Page](screenshots/login.png)

---

### 🏢 Department Management
![Department Management](screenshots/department.png)

---

### 🧾 Ticket Management
![Ticket Management](screenshots/ticket.png)

---

### 💬 Ticket Reply Conversation
![Ticket Reply Conversation](screenshots/ticket-reply.png)

---

### 👥 Employee Management
![Employee Management](screenshots/employee.png)

---

### ⏱️ SLA Management
![SLA Management](screenshots/sla.png)
