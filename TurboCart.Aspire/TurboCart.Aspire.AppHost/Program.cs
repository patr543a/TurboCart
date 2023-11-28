var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.TurboCart_Tests_UoWRazorTestApp>("turbocart.tests.uowrazortestapp");
//builder.AddProject<Projects.TurboCart_Tests_Api>("turbocart.tests.api");

var authApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_Auth>("AuthenticationAPI");
var sessionApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_Session>("SessionAPI");
var turboApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_TurboCart>("TurboCartAPI");

var gatewayApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_Gateway>("GatewayAPI")
    .WithReference(authApi)
    .WithReference(turboApi)
    .WithReference(sessionApi);

builder.AddProject<Projects.TurboCart_Presentation_Websites_TurboCartDK>("TurboCartDK")
    .WithReference(gatewayApi);

builder.AddProject<Projects.TurboCart_Presentation_Websites_TurboCartManagement>("TurboCartManagement")
    .WithReference(gatewayApi);

builder.Build().Run();