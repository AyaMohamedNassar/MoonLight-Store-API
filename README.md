# Moonlight Store - ASP.NET Core E-Commerce API

Welcome to the **Moonlight Store** API! This project is a robust and scalable e-commerce backend built using ASP.NET Core with a 3-tier architecture. It is designed to handle modern e-commerce functionalities with high performance and extensibility in mind.

---

## 🚀 Features

### **Architecture & Design Patterns**
1. **3-Tier Architecture**: Core, API, and Infrastructure layers for separation of concerns.
2. **Repository Pattern** and **Unit of Work**: Clean and maintainable data access.
3. **Specification Pattern**: Simplifies complex query logic and promotes reuse.
4. **Deferred Executions**: Optimized query execution.

### **Functionalities**
5. **Customer Orders Management**: Seamlessly handle orders with validations and updates.
6. **Seeding Data**: Populate the database with initial test data.
7. **Eager Loading**: Improve performance by fetching related data upfront.
8. **Redis**: Used for storing the client basket.
9. **API Validation**: Ensures data integrity with robust request validations.
10. **Error Handling**: Centralized exception handling for consistent responses.
11. **Authentication & Authorization**: Secured with JWT tokens.
12. **Email Confirmation**: Email-based verification for signing up users.

### **Enhanced Features**
13. **Structured Logging**: Logging powered by Serilog, integrated with Seq for observability.
14. **Swagger Documentation**: Interactive API documentation.
15. **Filter & Pagination**: Seamless handling of data browsing.

### **Upcoming Features**
- **Reset Password**
- **External Login with Google**: Integration with Google OAuth and MS Identity.
- **Refresh Token**
- **Localization**
- **Caching with Redis**

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **Caching**: Redis
- **Logging**: Serilog and Seq
- **Containerization**: Docker (for Redis and Seq)
- **.NET SDK**: 8.0+

---

## 📝 Get Started

### Prerequisites
1. **Install .NET SDK 8.0+**: [Download Here](https://dotnet.microsoft.com/download)
2. **Install Docker**: [Download Here](https://www.docker.com/)
3. **SQL Server**: Ensure a running SQL Server instance.

### Installation Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/MoonlightStore.git
   cd MoonlightStore
   ```
2. Configure the database connection string in `appsettings.json`.
3. Run the database migrations:
   ```bash
   dotnet ef database update
   ```
4. Build and run the API:
   ```bash
   dotnet run
   ```
5. Start Redis and Seq containers using Docker:
   ```bash
   docker-compose up
   ```

---

## 🤝 Contributing

Contributions are welcome! Feel free to fork the project, create a branch, and submit a pull request.  

---

## 📧 Contact

For questions or suggestions, reach out at [aya.m.nasar@gmail.com].

---

Enjoy exploring **Moonlight Store**! ✨
