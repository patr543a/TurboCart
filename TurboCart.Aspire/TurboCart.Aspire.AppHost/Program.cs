var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.TurboCart_Tests_UoWRazorTestApp>("turbocart.tests.uowrazortestapp");

builder.AddProject<Projects.TurboCart_Presentation_Websites_TurboCartDK>("turbocart.presentation.websites.turbocartdk");

builder.Build().Run();
