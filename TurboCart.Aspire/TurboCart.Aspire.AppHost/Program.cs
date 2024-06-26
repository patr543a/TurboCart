var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.TurboCart_Tests_UoWRazorTestApp>("turbocart.tests.uowrazortestapp");
//builder.AddProject<Projects.TurboCart_Tests_Api>("turbocart.tests.api");

var authApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_Auth>("AuthenticationAPI");
var turboApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_TurboCart>("TurboCartAPI");

var gatewayApi = builder.AddProject<Projects.TurboCart_Presentation_Apis_Gateway>("GatewayAPI")
    .WithReference(authApi)
    .WithReference(turboApi);

var grpcServer = builder.AddProject<Projects.TurboCart_Infrastructure_Networking_Grpc>("GrpcSessionServer")
    .WithLaunchProfile("https");

builder.AddProject<Projects.TurboCart_Presentation_GrpcClients_SessionClient>("GrpcSessionClient")
    .WithReference(grpcServer);

builder.AddProject<Projects.TurboCart_Presentation_Websites_TurboCartSession>("TurboCartSession")
    .WithReference(grpcServer);


builder.AddProject<Projects.TurboCart_Presentation_Websites_TurboCartDK>("TurboCartDK")
    .WithReference(gatewayApi);

builder.AddProject<Projects.TurboCart_Presentation_Websites_TurboCartManagement>("TurboCartManagement")
    .WithReference(gatewayApi);


builder.Build().Run();