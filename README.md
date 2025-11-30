# Gym Management System

A full-featured Gym Management System built using **C#**, **.NET**, **SQL Server**, and **HTML/CSS**.  
This system allows gym owners to manage members, subscriptions, payments, and coaches through a clean, scalable layered architecture.

---

## 🚀 Features
- Manage gym members (Add, Update, Delete, View)
- Subscription & membership plan management
- Coach management (Add / Update / Assign)
- Search & filtering for members and plans
- Layered architecture (DAL – BLL – UI)
- SQL Server relational database integration

---

## 🛠️ Tech Stack
**Languages:**  
- C#

**Frameworks & Tools:**  
- .NET  
- EFCore.NET 
- SQL Server  
- Git & GitHub

**Frontend (UI):**  
- HTML, CSS 

---

## 📁 Project Architecture
```
/BusinessLogicLayer (BLL)
/DataAccessLayer (DA)
/PresentationLayer (UI)
/Database Scripts
```

- **DA:** Handles all database operations  
- **BLL:** Implements business rules and validation  
- **UI:** User interface layer  
- Clean 3-tier structure improves maintainability and scalability

---

## 🗄️ Database
Built using **SQL Server**, including tables for:
- Members  
- Subscriptions  
- Coaches   
- Plans  
- Attendance 

---

## ▶️ How to Run
1. Clone the repository.  
2. Restore the database using the included SQL script.  
3. Update the connection string in the configuration file.  
4. Run the project.

---

## 🔗 GitHub Repository
[Gym Management System](https://github.com/mohamed-fathi610/GymManagementSystem)
