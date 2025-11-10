
var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Api>("apiwithmodules").WithExternalHttpEndpoints().WithHttpHealthCheck("/health");

builder.Build().Run();
