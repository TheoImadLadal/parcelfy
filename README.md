# <p align="center">Parcelfy</p>
  
  
Welcome to "**Parcelfy**" - your one-stop solution for tracking parcels from the largest carriers in France. The app is designed to provide a seamless experience for monitoring the delivery status of your packages. 


## 🚧 Under Construction
The app is currently in its early stages, and we are actively working to expand our carrier support. <br/>
Presently, Parcelfy supports tracking for :
[La Poste](www.laposte.fr),
[Chronopost](www.chronopost.fr) and
[Colissimo](www.colissimo.fr)<br/>
The future vision is to include support for all major carriers, ~~both domestic and international~~. In the upcoming releases, we plan to support tracking for: 
[Dhl](www.dhl.com),
[Ups](www.ups.com) 
and [Fedex](www.fedex.com) <br/>
Stay tuned.


[![🚀 Build and Deploy](https://github.com/TheoImadLadal/parcelfy/actions/workflows/buildAndDeploy.yml/badge.svg)](https://github.com/TheoImadLadal/parcelfy/actions/workflows/buildAndDeploy.yml)
[![✍ Unit test coverage - threshold 85%](https://github.com/TheoImadLadal/parcelfy/actions/workflows/unitTestCoverage.yml/badge.svg)](https://github.com/TheoImadLadal/parcelfy/actions/workflows/unitTestCoverage.yml)
[![🔎 CodeQL](https://github.com/TheoImadLadal/parcelfy/actions/workflows/codeql.yml/badge.svg)](https://github.com/TheoImadLadal/parcelfy/actions/workflows/codeql.yml)
[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FTheoImadLadal%2Fparcelfy%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/TheoImadLadal/parcelfy/main)
[![👌 Specflow tests](https://github.com/TheoImadLadal/parcelfy/actions/workflows/specflow.yml/badge.svg)](https://github.com/TheoImadLadal/parcelfy/actions/workflows/specflow.yml)
![GitHub language count](https://img.shields.io/github/languages/count/theoimadladal/parcelfy)
![GitHub top language](https://img.shields.io/github/languages/top/theoimadladal/parcelfy)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/theoimadladal/parcelfy)
![GitHub repo size](https://img.shields.io/github/repo-size/theoimadladal/parcelfy)
![GitHub commit activity](https://img.shields.io/github/commit-activity/w/theoimadladal/parcelfy)

## 🧐 Features    
- **Unified Tracking**: Enjoy the convenience of tracking all your parcels from different carriers on a single page.
- **Real-time Updates**: Real-time updates about the status of your parcels.

   
        
## 🛠️ Tech Stack

**FRONTEND** *~~Coming soon~~*

* [ ] [BLAZOR](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) - BACKEND - Hosted in this Github Repo : [Parcelfy-Blazor](https://github.com/TheoImadLadal/parcelfy-blazor)
* [ ] [REACT](https://react.dev/) - FRONTEND - Hosted in this Github Repo : [Parcelfy-React](https://github.com/TheoImadLadal/parcelfy-react)

**BACKEND**

* [x] [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core) - (Latest release - Available since Novembre 2023)
* [x] [Mapster](https://github.com/MapsterMapper/Mapster)
* [x] [FluentValidation](https://fluentvalidation.net/)
* [x] [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/moq)
* [X] [Stryker testing](https://stryker-mutator.io/)
* [X] [Specflow BDD](https://specflow.org/)
* [X] [Authentication API JWT](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/6.0.6)

 
**DATABASE**
* [x] [Azure SQL Database](https://azure.microsoft.com/fr-fr/products/azure-sql/database/)
* [x] [EF CORE 6](https://learn.microsoft.com/fr-fr/ef/core/what-is-new/ef-core-6.0/whatsnew)
* [ ] [POSTGRESql](https://www.postgresql.org/) - As a replacement for Azure SQL DB 

**PATTERN** *~~Coming soon~~*
* [ ] [Hexagonal Architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture_(software))
* [ ] [Circuit Breaker](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-circuit-breaker-pattern)

*And more to come.*    
     

## ➤ API Reference 

#### API Doc : https://parcelfy.azurewebsites.net/swagger/index.html
### Get tracking details by parcelId
```http
GET /parcel-tracker/{parcelId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `parcelId` **Required**. | `string` |  Your parcel Id |
        
   
        
## 🙇 Acknowledgements      

### Api
This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection

### Application
This layer contains all application logic. It is dependent on the infrastructure layer, but has no dependencies on any other layer or project.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. <br/>
These classes should be based on interfaces defined within the application layer.
        

        
