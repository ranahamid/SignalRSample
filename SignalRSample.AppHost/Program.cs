var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SignalRSample>("signalrsample");

builder.Build().Run();
