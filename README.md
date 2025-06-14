## User Authentication & Authorization API (ASP.NET Core + JWT)


A secure and scalable Authentication & Authorization REST API built with ASP.NET Core 8 Web API. It enables modern applications to register users, authenticate via JWT tokens, and authorize access to protected endpoints — all while following security best practices.

 ## Perfect for integrating as a backend for your web or mobile apps, this API uses Entity Framework Core, MS SQL Server, and BCrypt for secure and stateless user authentication.

## Features:

1: JWT-Based Login:

- Authenticates users and returns a signed JSON Web Token (JWT).

- Stateless and scalable authentication mechanism.
  
2: Secure Registration:

- Passwords hashed using BCrypt.Net-Next with unique salts.

- Defends against rainbow tables and brute-force attacks.

3: Protected Endpoints:

- Middleware validates incoming JWTs.
  
- Restricts access to authorized users only.

4: Role-Based Authorization (RBAC):

- Role support (e.g., User, Admin).
  
- Fine-grained access control based on roles.

5: Secure Password Practices:

- Uses BCrypt's adaptive hash function.
  
- Resistant to offline cracking attempts.

## Technologies Used:

- Backend Framework: ASP.NET Core 8.0 Web API (compatible with .NET SDK 8.0+).
  
- Database: MS SQL Server 2022.
  
- ORM (Object-Relational Mapper): Entity Framework Core 8.0+.
  
- Password Hashing Library: BCrypt.Net-Next.
  
- Authentication & Authorization Standard: JSON Web Tokens (JWT).
  
- API Documentation Tool: Swashbuckle.AspNetCore (for Swagger UI generation).
  
- Development Environment: Visual Studio Code 2022.
  
- API Testing Tool: Thunder Client (VS Code Extension), compatible with Postman/Insomnia.


