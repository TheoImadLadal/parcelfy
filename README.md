# <p align="center">Parcelfy</p>
  
One single page to track parcels from the biggest carriers in France (Laposte, Colissimo, Chronopost)

[![🚀 Build and deploy ASP.Net Core app to Azure Web App - Parcelfy](https://github.com/TheoImadLadal/parcelfy/actions/workflows/main_parcelfy.yml/badge.svg)](https://github.com/TheoImadLadal/parcelfy/actions/workflows/main_parcelfy.yml)
    
        
## 🛠️ Tech Stack
* [x] [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [x] [EF CORE 6](https://learn.microsoft.com/fr-fr/ef/core/what-is-new/ef-core-6.0/whatsnew)
* [x] [Mapster](https://github.com/MapsterMapper/Mapster)
* [x] [FluentValidation](https://fluentvalidation.net/)
* [ ] [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/moq)
* [ ] [Stryker testing](https://stryker-mutator.io/)
* [ ] [Specflow BDD](https://specflow.org/)
* [ ] [Authentication API JWT]
* [ ] [Circuit Breaker setup]
 
*And more to come.*    
     

## ➤ API Reference 

### Get tracking details by parcelId
```http
GET /parcel-tracker/parcelId
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `parcelId` | `string` | **Required**.Your parcel Id |
        



## 🧐 Pull Request
##### Code
- [X] MVP <br/>
💻 Pull Request => [A kind of a P.O.C - The worst architecture ever, go and take a look on the PR](https://github.com/TheoImadLadal/parcelfy/pull/3/files)
- [X] Clean architecture <br/> 
💻 Pull Request => [Setting up a clean architecture](https://github.com/TheoImadLadal/parcelfy/pull/5/files)
- [X] Clean Code <br/>
💻 Pull Request => [Setting up a clean code](https://github.com/TheoImadLadal/parcelfy/pull/8/files)
- [X] Mapster instead of manual mapping <br/>
💻 Pull Request => [Setting up Mapster](https://github.com/TheoImadLadal/parcelfy/pull/9/files)
- [ ] FluentValidation

##### Pattern 
- [X] CQRS
- [ ] Circuit Breaker

##### Test
- [ ] Nunit/FluentAssertion
- [ ] Stryker
- [ ] Specflow        
        
## 🙇 Acknowledgements      

### Api
This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection

### Application
This layer contains all application logic. It is dependent on the infrastructure layer, but has no dependencies on any other layer or project.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. <br/>
These classes should be based on interfaces defined within the application layer.
        

        
