# market.io
### A web application that allows you to connect with others to buy, sell, and advertise items, services, real estate, and many more... 

## Project technologies
- Backend: SQL Entity Framework Core / SQL design with stored proecdures and LINQ
- Frontend: Blazor Web App
- Web API: ASP.NET Core Web API (to handle REST/HTTP operations, token authentication) 

## Project overview
marketplace.io will allow users to register and login to personal accounts to list their items, homes for sale/rent, jobs, and other types of listings to the public. They will also be able to access the entire catalog of items posted by other users in the system. This includes the ability to enquire and send private chat messages between each other to negotiate and agree upon transactions freely.

## Proposed solutions
-Session tokens for user account will be handled by the Web API using JWT authorization.
-Listings will will be handled throught the Web API then stored in the SQL database.
-Bootstrap will handle frontend styling

## Software architechture
There will be 4 separate projects in the solution: Client (Blazor client side app), Web API(to handle crud operations with SQL data), Common class library (for models, helper classes, etc.), Test project(xUnit or MSTest)

### Client-side Blazor app
- Home page: contains information about site, links for listings, profile, login, register)
- Listings page: list of listings, (filter options if I have time to implement) 
- Listing detail page: brief information about listing, buttons to inquires, save
- Chat page / component: A chat page to display chat messages, an input to send a chat; If I can, I will try to make chat feature a static hovering component that follows you on every page (if I can successfully manage this with Blazor)

### WebAPI server
-Will contain REST Operation for data contained in the SQL database
-To save time, I will most likely scaffold this with Entity Framework
-I will use RestSharp (https://restsharp.dev/) for my client-side HTTP requests

### Common libary 
-Contains models, helper classes/methods

### Testing
-xUnit or MSTest
-Contains all the tests for this project

## Quality assurance
-testing will be done with xUnit or MSTest
-Tests will include RestSharp methods and trying to make them function without any bugs

## For the future
-The database will be set up so the there is room for enhancements
-The project is using up-to-date technology and software

### Conclusion
-This project is going to be a step forward in my web development and database designing skills. I also see this as a great resume project.
