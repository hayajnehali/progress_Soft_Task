# progressSoftTask
Business Card API with Angular and .NET 7
Overview
This project is a Business Card API built using .NET 7 API and Angular. It apply the Onion Architecture for better separation of concerns and maintainability. 



Features
.NET 7 API Backend built with Onion Architecture

Generic Repository and Services  

DTO and Entity Mapping for clean data transfer between layers

Import data from csv ,xml and Qr code

Export data to csv ,xml and Qr code

Unit Tests for all business logic


Technologies Used
Backend:

.NET 7 Web API

Entity Framework Core 

xUnit for Unit Testing

Automapper for DTO and Entity Mapping

Frontend:

Angular 


Project Structure
1. Core Layer (Core) The Core Layer contains the interfaces for the business logic.
 Key Components:
Models Folder:
Request folder for DTOs and Response folder for Entities
shared folder containing mapper


2. Infrastructure Layer (Infra)
This layer contains the implementation of logic and interactive  with the database using Entity Framework.

ApplicationDbContext : contains The Model Builder and Configures database. 


3. API Layer (API)
This layer exposes the endpoints to interact with the backend.

Controllers:

BusinessCardController: The main API controller handling business card operations like creating, updating, deleting, and exporting business cards as CSV.

4. Frontend Layer (ClientApp)
Angular Components:

BusinessCardListComponent: view all business card with actions(delete, add ,export ,generate Qr Code)

BusinessCardImportComponent: import business card from xml ,csv and Qr Code

BusinessCardManageComponent; for add business card

 

Services:

BusinessCardService: Angular service responsible for hit on  API .
