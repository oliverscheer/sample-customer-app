var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Customer_Web>("customer-web");

builder.Build().Run();
