# Shared Notes

https://shared-notess.azurewebsites.net

# Shared Notes Web Application

This is a web application built with ASP.NET MVC and Entity Framework Core. It allows users to create and manage shared notes with other users. Each note can have a title, description, and can be shared with multiple users. Just small free time project for fun.

## Features

- User registration and authentication
- Create new lists with titles and descriptions
- Share lists with other sign up users
- Grant access rights to other users for editing the list
- Edit list titles and descriptions
- Delete lists
- View shared lists and their details

## Technologies Used

- ASP.NET MVC
- Entity Framework Core (Code First approach)
- PostgreSQL
- Azure App Service (for hosting)
- Azure App Configuration (for managing configuration settings)
- Google reCapcha
- Facebook authentication

## Prerequisites

- Visual Studio (or any other preferred code editor)
- .NET 6 SDK
- PostgreSQL (with minor changes you can use database of your choice like: Microsoft SQL Server (or SQL Server express))

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/your-repo.git
   
2. Open the solution file (SharedList.sln) in Visual Studio.

3. Configure the database connection:

- Open the appsettings.json file.
- Update the DefaultConnection connection string with your SQL Server connection details.

4. Build the solution in Visual Studio.

5. Run the application locally by pressing F5 or clicking on the "Start" button in Visual Studio.

6. The application should open in your default web browser.

## Deployment
This project can be deployed to Azure App Service:

1. Create an Azure App Service.
2. Publish the application to the App Service using Visual Studio or any other preferred deployment method.
3. Configure the necessary environment variables, including the database connection string, in the App Service settings.
Note: if you use PostgreSQL(like me) there are known issues, refer:
For detailed instructions on deploying an ASP.NET MVC application to Azure App Service, refer to the official documentation: Deploy an ASP.NET MVC app to Azure App Service
