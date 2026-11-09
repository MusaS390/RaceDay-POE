# RaceDay – Part 3 POE

## 1. Project Overview

RaceDay is a race event management system developed to help organisers manage running events and allow participants to register, enrol in events and view their race results.

This project focuses on developing an **ASP.NET Core MVC frontend** that communicates with the RaceDay REST API developed in Part 2.

The MVC application communicates with the API using HTTP requests. It does not directly access the SQL Server database or duplicate the business logic implemented in the API.

## 2. Project Objectives

The main objectives of the RaceDay system are to:

- Provide a user-friendly interface for participants and organisers.
- Allow users to register and log in securely.
- Allow participants to browse available race events.
- Allow participants to enrol in event categories.
- Allow participants to view their enrolments and race results.
- Allow organisers to create and manage race events.
- Allow organisers to manage event categories and enrolments.
- Allow organisers to record and update race results.
- Provide a consistent and responsive user interface.

## 3. Technologies Used

The following technologies were used in developing the RaceDay application:

- **C#** – Main programming language.
- **ASP.NET Core MVC** – Used to develop the frontend application.
- **ASP.NET Core Web API** – Handles backend operations and data processing.
- **Razor Views** – Used to create dynamic user interface pages.
- **HTML5** – Structures the web pages.
- **CSS3** – Styles the application.
- **JavaScript** – Adds client-side functionality.
- **SQL Server** – Stores application data.
- **HttpClient** – Allows the MVC application to communicate with the API.
- **ASP.NET Core Session** – Maintains user session information.
- **Visual Studio** – Development environment.
- **Git and GitHub** – Used for version control and source code management.

## 4. Solution Structure

The solution contains the following main projects:

### RaceDay.Api
The backend Web API responsible for handling application requests, business logic, authentication and database operations.

### RaceDay.Mvc
The frontend MVC application responsible for displaying pages and allowing users to interact with the RaceDay system.

### RaceDay.Tests
The testing project used to perform automated tests on the application where applicable.

## 5. User Roles

The RaceDay application supports two main user roles.

### Participant

Participants can:

- Register for an account.
- Log in and log out.
- View available race events.
- View event details and categories.
- Enrol in event categories.
- View their enrolled events.
- View their race results.
- Update their profile information.

### Organiser

Organisers can:

- Access the organiser dashboard.
- Create new race events.
- View existing events.
- Update event information.
- Delete events.
- Manage event categories.
- View participant enrolments.
- Record and update race results.

## 6. Installation Requirements

Before running the application, ensure that the following software is installed:

1. Visual Studio.
2. The .NET SDK version required by the solution.
3. SQL Server LocalDB or the configured SQL Server instance.
4. Git (if cloning the project from GitHub).

The Visual Studio installation should include the **ASP.NET and web development** workload.

## 7. Database Configuration

The application uses SQL Server for data storage through the RaceDay API.

Open the following file:

`RaceDay.Api/appsettings.json`

Check that the database connection string points to the correct SQL Server instance.

Example LocalDB connection string:

`Server=(localdb)\MSSQLLocalDB;Database=RaceDay;Trusted_Connection=True;TrustServerCertificate=True;`

Make sure the RaceDay database and its required tables have been created using the SQL scripts supplied with the previous POE parts.

## 8. API Configuration

The MVC application communicates with the API through an HTTP client.

Open:

`RaceDay.Mvc/appsettings.json`

The API base URL should match the address where the API is running.

Example:

```json
{
  "Api": {
    "BaseUrl": "http://localhost:5194/"
  }
}
```

If the API uses a different port on your computer, update the URL accordingly.

## 9. How to Run the Application

The API and MVC frontend must both be running for the complete application to work.

### Step 1: Open the Solution

Open the RaceDay solution in Visual Studio.

### Step 2: Build the Solution

Select:

**Build → Build Solution**

Wait for the build process to finish and check that there are no compilation errors.

### Step 3: Start the API

1. In Solution Explorer, right-click `RaceDay.Api`.
2. Select **Set as Startup Project**.
3. Press F5 or Ctrl + F5.
4. If Swagger is enabled, open:

`http://localhost:5194/swagger`

Keep the API running.

### Step 4: Start the MVC Application

1. Return to Solution Explorer.
2. Right-click `RaceDay.Mvc`.
3. Select **Set as Startup Project**.
4. Press F5 or Ctrl + F5.

The MVC application should open at its configured launch address.

Example:

`https://localhost:7294/Auth/Login`

### Step 5: Log In or Register

Use the registration or login page to access the application.

The available pages and functionality depend on the user's assigned role.

**Important:** Do not stop the API while using the MVC application. The frontend depends on the API to retrieve and update information.

## 10. Authentication and Session Management

The application uses authentication and session management to maintain user information while navigating between pages.

The MVC application configures ASP.NET Core session services with:

- An HTTP-only session cookie.
- An essential session cookie.
- A 30-minute session timeout.

Users must log in before accessing protected application functionality.

## 11. Version Control and GitHub

Git and GitHub are used to manage the project's source code and track development progress.

Meaningful commits should be made throughout development to document changes and improvements.

Examples of commit messages include:

- Initial RaceDay MVC setup.
- Configure API communication.
- Add authentication views.
- Implement participant dashboard.
- Add organiser event management.
- Implement event category management.
- Add participant enrolment functionality.
- Implement race results functionality.
- Improve application navigation.
- Fix API connection configuration.
- Update project documentation.

Each commit should represent an actual development change.

## 12. Troubleshooting

### API Connection Error

If the MVC application cannot connect to the API:

- Ensure that `RaceDay.Api` is running.
- Check the API port.
- Verify the `BaseUrl` in `RaceDay.Mvc/appsettings.json`.
- Restart the API and MVC applications if necessary.

### Database Connection Error

If the API cannot connect to SQL Server:

- Check that SQL Server LocalDB is installed.
- Verify the connection string.
- Confirm that the RaceDay database exists.
- Ensure that the database tables have been created.

### Build Errors

If the solution does not build:

- Check the Error List in Visual Studio.
- Restore NuGet packages.
- Save all modified files.
- Rebuild the solution.

### Git Commit Problems

If the Commit All button is disabled:

- Save the modified files.
- Open the Git Changes window.
- Check whether Git detects any changes.
- Ensure that the correct repository is selected.
- Enter a commit message before committing.

## 13. Conclusion

The RaceDay Part 3 project provides an MVC frontend that allows participants and organisers to interact with the RaceDay system through a web-based interface.

By integrating the MVC application with the REST API developed in Part 2, the project separates the frontend from backend processing and database operations.

The application demonstrates the use of ASP.NET Core MVC, API integration, session management, database-backed functionality and Git version control in developing a web-based race event management system.

Youtube video link : 
Github link : https://github.com/MusaS390/RaceDay-POE
---

**Project Name:** RaceDay  
**POE Part:** Part 3 – MVC Frontend  
**Student Name:** Musa Simelane 
**Student Number:** ST10496330  
**Module:** Programming  
**Submission Date:** 09/11/2026
