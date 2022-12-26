# Parcelfy
One single page to track parcels from the biggest carriers in France (Laposte, Colissimo, Chronopost, DHL, UPS)

### Task List

- [x] Init Repo with Clean archi
- [x] Downgrade to .Net 6 (.Net 7 Not supported)
- [X] AppSetting with LAPOSTE Data 
- [X] Get on LAPOSTE / [A kind of a P.O.C - The worst architecture ever, go and take a look on the PR](https://github.com/TheoImadLadal/parcelfy/pull/3/files)
- [ ] Clean code/archi
- [ ] UnitTest LAPOSTE
- [ ] Stryker LAPOSTE
- [ ] WebUi LAPOSTE
- [ ] DHL, UPS, ...



### Tasks to come v2:

1. Add authentication and authorization to the API to ensure that only authorized users can access the API and track parcels. You can use the ASP.NET Core Identity framework to manage user accounts and the [Authorize] attribute to require authentication for certain API actions.

2. Handle errors and exceptions in the API by using try-catch blocks and returning appropriate error messages and status codes to the client. You can also use global error handling to catch any unhandled exceptions and return a consistent error response.

3. Add logging to the API to record important events and errors, which can help you troubleshoot issues and monitor the API's performance. You can use the built-in logging framework in ASP.NET Core to write log messages to the console, a file, or a remote log service.

4. Add unit and integration tests to the API to ensure that it works as expected and can handle a variety of input and edge cases. You can use the built-in testing framework in .NET Core and the [Fact] and [Theory] attributes to create unit tests, and use the [ClassFixture] and [Collection] attributes to create integration tests that test the API end-to-end.

5. Optimize the performance of the API by caching frequently accessed data, using asynchronous programming techniques, and minimizing the amount of data transferred over the network. You can use tools like the Stopwatch class and the ASP.NET Core middleware to measure the performance of the API and identify areas for improvement.
