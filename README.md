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
