# Parcelfy
One single page to track parcels from the biggest carriers in France (Laposte, Colissimo, Chronopost, DHL, UPS)

### Task List

- [x] Init Repo with Clean archi
- [x] Downgrade to .Net 6 (.Net 7 Not supported)
- [ ] Clean code/archi
- [ ] AppSetting with LAPOSTE Data 
- [ ] Get on LAPOSTE
- [ ] UnitTest LAPOSTE
- [ ] Stryker LAPOSTE
- [ ] WebUi LAPOSTE
- [ ] DHL, UPS, ...



### Tasks to come v1:

~~1. Install the .NET Core SDK and create a new .NET Core project in Visual Studio or using the dotnet command-line interface.~~

2. In your project, create a new class called ParcelTracker and add a method called TrackParcel that takes a tracking number as a parameter and returns a Parcel object.

3. In the TrackParcel method, use the tracking number to retrieve the parcel's information from a database or a third-party tracking service.

4. Return the Parcel object with the updated information.

5. To create a clean architecture, you can use the dependency inversion principle to decouple the ParcelTracker class from the database or tracking service implementation. To do this, create an interface called IParcelTracker that defines the TrackParcel method, and make the ParcelTracker class implement this interface.

6. In the TrackParcel method, inject an instance of the IParcelTracker interface using the constructor, and use it to retrieve the parcel's information.

7. To expose the TrackParcel method as an API endpoint, use the [HttpGet] attribute and specify the route template for the endpoint in the [Route] attribute. For example:
```
[HttpGet]
[Route("api/parcels/{trackingNumber}")]
public Parcel TrackParcel(string trackingNumber)
{
    // Retrieve and return the parcel information
}
```


8. To test the API, start the project in Visual Studio or using the dotnet run command, and then use a tool like Postman to send a GET request to the api/parcels/{trackingNumber} endpoint, where {trackingNumber} is the tracking number of the parcel you want to track. You should receive a response with the parcel's information in the body of the response.




### Tasks to come v2:

1. Add authentication and authorization to the API to ensure that only authorized users can access the API and track parcels. You can use the ASP.NET Core Identity framework to manage user accounts and the [Authorize] attribute to require authentication for certain API actions.

2. Handle errors and exceptions in the API by using try-catch blocks and returning appropriate error messages and status codes to the client. You can also use global error handling to catch any unhandled exceptions and return a consistent error response.

3. Add logging to the API to record important events and errors, which can help you troubleshoot issues and monitor the API's performance. You can use the built-in logging framework in ASP.NET Core to write log messages to the console, a file, or a remote log service.

4. Add unit and integration tests to the API to ensure that it works as expected and can handle a variety of input and edge cases. You can use the built-in testing framework in .NET Core and the [Fact] and [Theory] attributes to create unit tests, and use the [ClassFixture] and [Collection] attributes to create integration tests that test the API end-to-end.

5. Optimize the performance of the API by caching frequently accessed data, using asynchronous programming techniques, and minimizing the amount of data transferred over the network. You can use tools like the Stopwatch class and the ASP.NET Core middleware to measure the performance of the API and identify areas for improvement.
