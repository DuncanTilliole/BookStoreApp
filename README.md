# Project .NET 8 : Blazor WEB APP, ASP.NET MVC with Entity Framework and SQL Server
This project is a web application developed with Blazor, utilizing ASP.NET MVC, Entity Framework, and SQL Server as the database. It uses .NET 8 and is up to date with the latest updates as of late 2023.

## Features

### Full Authentication:

* Complete authentication system with user, roles, and permissions management.
* User creation and login with secure password storage.


### Isolation System:

* Utilization of separation of concerns (SoC) to isolate different application features.

  
### Full CRUD Operations:

* CRUD operations (Create, Read, Update, Delete) on Author and Book entities.
* Use of the Repository pattern to organize data access.


### Image Insertion System:

* Ability to add images to Author and Book entities, stored in the database.


### Author Selection in Book Creation:

* Integration of a select input to choose an author when creating a book.

  
### Entity Details:

* Detailed display of information about authors and books.


### Performance Optimization with Virtualization:

* Utilization of virtualization to improve performance when displaying large amounts of data.


### Code-behind and Complete Code:

* Use of both code-behind and complete code to implement application features.


## Configuration
1. To run this application locally, follow these steps:

2. Make sure you have .NET 8 and SQL Server installed on your machine.

3. Clone this repository to your machine.

4. Configure the connection to the SQL Server database in the appsettings.json file.

5. Open a terminal window in the root directory of the application and run the following command to create the database:

`dotnet ef database update`

6. Then, run the application using the following command:

`dotnet run`

7. Access the application in your browser at https://localhost:7002.

## Author
This project was developed by TILLIOLE Duncan.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
