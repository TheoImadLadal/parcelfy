# <p align="center">Parcelfy</p>
  
One single page to track parcels from the biggest carriers in France (Laposte, Colissimo, Chronopost)

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

    
        
## 🛠️ Tech Stack
* [x] [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [x] [EF CORE 6](https://learn.microsoft.com/fr-fr/ef/core/what-is-new/ef-core-6.0/whatsnew)
* [x] [Mapster](https://github.com/MapsterMapper/Mapster)
* [x] [FluentValidation](https://fluentvalidation.net/)
* [x] [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/moq)
* [X] [Stryker testing](https://stryker-mutator.io/)
* [X] [Specflow BDD](https://specflow.org/)
* [ ] [Authentication API JWT](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/6.0.6)
* [ ] [Circuit Breaker setup](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-circuit-breaker-pattern)
 
*And more to come.*    
     

## ➤ API Reference 

### Get tracking details by parcelId
```http
GET /parcel-tracker/parcelId
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `parcelId` | `string` | **Required**.Your parcel Id |
        
   
        
## 🙇 Acknowledgements      

### Api
This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection

### Application
This layer contains all application logic. It is dependent on the infrastructure layer, but has no dependencies on any other layer or project.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. <br/>
These classes should be based on interfaces defined within the application layer.
        

        
