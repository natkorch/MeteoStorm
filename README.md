# MeteoStorm
Collects data from an external service, stores it in a database and analyzes in periodic reports

This project is an example of how to use several cutting-edge technologies and programming concepts in practice. It includes the following aspects:

- Well-structured project architecture divided into modules: this approach enables the easy addition of new components that can efficiently communicate with existing project blocks.
- Worker Service: a .NET 7.0 Core hosting solution that enables long-running background tasks.
- Configuration and Dependency Injection: the application is configured using custom settings in appsettings.json, which are injected into appropriate classes using the built-in DI container.
- Logging: structured logging is implemented using the popular Serilog library, which provides a flexible and extensible way to log events.
- Error handling: exceptions are properly handled and logged to ensure the stability and reliability of the application in the face of errors or unexpected events.
- Periodic Jobs with Quartz: a third-party library that makes it easy to schedule and execute recurring jobs.
- Entity Framework Core: a modern object-relational mapper that simplifies database access and manipulation.
- Code-First Database Creation: the database schema is defined using C# code and then applied to the database at runtime using Entity Framework Migrations.
- SQL Data Seeding: pre-defined data is inserted into the database using raw SQL scripts during the Entity Framework Core migration process.
- External API Integration: the project demonstrates how to consume an external RESTful API service and store the results in the database.
- Dependency Inversion Principle through interfaces: service client created through a factory based on settings in appsettings.json.

By combining all these components, this project showcases how to build a robust, scalable, and maintainable .NET Core application that leverages the latest tools and methodologies available.
