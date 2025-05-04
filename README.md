# Employee Management System

This is a full-stack web application built with **.NET Core Web API** for the backend and **Angular** for the frontend. It allows users to manage a list of employees with basic CRUD operations.

---

## 🔧 Features

### 🖥️ Backend (.NET Core Web API)

- Manage employees with the following properties:
  - `Id` (int, auto-generated)
  - `FirstName` (string)
  - `LastName` (string)
  - `Email` (string)
  - `Position` (string)


### 🌐 Frontend (Angular)

- Display a list of employees
- Add, edit, and delete employees
- Angular service to handle API communication
- Form validation and error handling
- User feedback for all operations
---

## 🚀 Bonus Features

- Search/filter employees by name or position
- Pagination for the employee list
---

## 🛠️ Tech Stack

- **Backend:** .NET Core Web API
- **Frontend:** Angular
- **Styling:**  Bootstrap
- **Database:** SQL Server Express 

---
Backend Endpoints 
![image](https://github.com/user-attachments/assets/ca14f4c1-0ee3-43bd-ae9b-c04550b667b2)

### ⚙️ Run Backend

```bash
cd Backend with Asp.net Web Api
dotnet restore
dotnet run
```

### 🌐 Run Frontend
```bash
cd Frontend/employee-app
npm install
ng serve
```
### 🤝 Acknowledgments
Thanks to Ahmed Altaher for the task and guidance.
