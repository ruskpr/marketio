# marketplace.io
### A web application that allows you to connect with others to buy, sell, and advertise items, services, real estate, and many more... 

## Project technologies
- Backend: SQL Entity Framework Core / SQL design with stored proecdures and LINQ
- Frontend: Blazor Web App
- Web API: ASP.NET Core Web API (to handle REST/HTTP operations, token authentication) 

## Project overview
marketplace.io will allow users to register and login to personal accounts to list their items, homes for sale/rent, jobs, and other types of listings to the public. They will also be able to access the entire catalog of items posted by other users in the system. They will be able to enquire and send private chat messages between each other to negotiate and agree upon transactions.

## Proposed solution
-Session tokens for user account will be handled by the Web API using JWT authorization.
-Listings will will be handled throught the Web API then stored in the SQL database.

## Software architechture
There will be 3 separate projects in my solution: Client (Blazor client side app), Web API(to handle crud operations with SQL data), Common class library (for models, helper classes, etc.) 

### Client-side Blazor app
- Home page: contains information about site, links for listings, profile, login, register)
- Listings page: list of listings, (filter options if I have time to implement) 
- Listing detail page: brief information about listing, buttons to inquires, save
- Chat page / component: A chat page to display chat messages, an input to send a chat; I will try to make chat feature a static hovering component that follows you on every page (if I can 
### Executive Summary
- A brief overview of the proposed project, its goals, and benefits
### Project Overview
- The problem being addressed
- The proposed solution
- Key features and functionalities
- Target users
### Project Scope and Objectives
- The project scope and objectives
- Project deliverables
- Milestones and timeline
### Technical Approach
- The software architecture
- Technologies and tools to be used
- Design patterns and methodologies
- Data storage and management
### Project Management
- Project team structure and responsibilities
- Risk assessment and management
- Project budget and resources
- Communication and collaboration tools
### Quality Assurance and Testing
- Testing strategy and methodology
- Types of testing to be performed
- Acceptance criteria and quality metrics
### Future Enhancements
- Planned future enhancements
- Potential scalability and performance improvements
- Long-term goals and vision
### Conclusion
- Summary of the proposed project and its benefits
- Call to action for stakeholders and decision-makers
### References
- Any relevant sources, research, or prior work that informed the proposal
