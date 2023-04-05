# market.io
### A web application that allows you to connect with others to buy, sell, and advertise items, services, real estate, and many more... 

## Project Technologies
- Backend: SQL Entity Framework Core / SQL design with stored proecdures and LINQ
- Frontend: Blazor Web App
- Web API: ASP.NET Core Web API (to handle REST/HTTP operations, token authentication) 

## Project Overview
market.io allows users to register and login to personal accounts to list their items, homes for sale/rent, jobs, and other types of listings to the public. They will also be able to access the entire catalog of items posted by other users in the system. This includes the ability to enquire and send private chat messages between each other to negotiate and agree upon transactions freely.

## Proposed Solutions
- User authentication will be handled by the Web API using JWT.
- Data will be handled throught the Web API then stored in the SQL database.

## Software Architechture
There will be 4 separate projects in the solution
- Client (Blazor client side app)
- Web API (to handle CRUD/REST operations with SQL database)
- Common class library (for models, helper classes, etc.)
- Test project (xUnit to perform logic test and ensure the code has no bugs)

### Blazor Application
- Bootstrap 5
- Home page: contains information about site, links for listings, profile, login, register)
- Listings page: list of listings, (filter options if I have time to implement) 
- Listing detail page: brief information about listing, buttons to inquires, save
- Chat page / chat component: A chat page to display chat messages, an input to send a chat; static hovering chat component that 

### ASP.NET Core WebAPI Server
- Will contain REST Operation for data contained in the SQL database
- To save time, I will most likely scaffold this with Entity Framework
- I will use RestSharp (https://restsharp.dev/) for my client-side HTTP requests

### Common C# Library 
- Contains models, helper classes/methods

### Testing
- xUnit
- Contains all the tests for this project

## Quality Assurance
- Testing will be done with xUnit or MSTest
- Tests will mostly include Http helper methods, classes
