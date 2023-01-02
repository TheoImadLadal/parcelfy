# Parcelfy
One single page to track parcels from the biggest carriers in France (Laposte, Colissimo, Chronopost)

[![Publish 🚀](https://github.com/TheoImadLadal/parcelfy/actions/workflows/publish.yml/badge.svg)](https://github.com/TheoImadLadal/parcelfy/actions/workflows/publish.yml)

## Technologies

* [x] [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [ ] [AutoMapper](https://automapper.org/)
* [ ] [FluentValidation](https://fluentvalidation.net/)
* [ ] [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/moq)
* [ ] [Stryker testing](https://stryker-mutator.io/)
* [ ] [Specflow BDD](https://specflow.org/)
 
*And more to come.*

## Overview

### Api
This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection

### Application
This layer contains all application logic. It is dependent on the infrastructure layer, but has no dependencies on any other layer or project.

### Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. <br/>
These classes should be based on interfaces defined within the application layer.

---
## Task List

##### Init
- [x] Init Repo with Clean archi
- [X] AppSetting with "LaPoste","Collissimo" and "Chronopost" Data 

##### Code
- [X] Set up => [GET] method <br/>
💻 Pull Request = [A kind of a P.O.C - The worst architecture ever, go and take a look on the PR](https://github.com/TheoImadLadal/parcelfy/pull/3/files)
- [X] Set up => Clean architecture <br/> 
💻 Pull Request = [Setting up a clean architecture](https://github.com/TheoImadLadal/parcelfy/pull/5/files)
- [ ] Set up => Clean Code
- [ ] Set up => AutoMapper
- [ ] Set up => FluentValidation

##### Pattern 
- [X] CQRS
- [ ] Circuit Breaker

##### Test
- [ ] Set up => Nunit/FluentAssertion
- [ ] Set up => Stryker
- [ ] Set up => Specflow

##### Improving things
- [ ] Add authentication and authorization to the API to ensure that only authorized users can access the API and track parcels. You can use the ASP.NET Core Identity framework to manage user accounts and the [Authorize] attribute to require authentication for certain API actions.
- [ ] Handle errors and exceptions in the API by using try-catch blocks and returning appropriate error messages and status codes to the client. You can also use global error handling to catch any unhandled exceptions and return a consistent error response.
- [ ] Add logging to the API to record important events and errors, which can help you troubleshoot issues and monitor the API's performance. You can use the built-in logging framework in ASP.NET Core to write log messages to the console, a file, or a remote log service.
- [ ] Add integration tests to the API to ensure that it works as expected and can handle a variety of input and edge cases. You can use the built-in testing framework in .NET Core and the [Fact] and [Theory] attributes to create unit tests, and use the [ClassFixture] and [Collection] attributes to create integration tests that test the API end-to-end.
- [ ] Optimize the performance of the API by caching frequently accessed data, using asynchronous programming techniques, and minimizing the amount of data transferred over the network. You can use tools like the Stopwatch class and the ASP.NET Core middleware to measure the performance of the API and identify areas for improvement.

---
## Support

If you are having problems, please let me know.

