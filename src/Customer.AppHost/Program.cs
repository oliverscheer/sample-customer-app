var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("SqlServerSaPassword", secret: true);

var sql = builder.AddSqlServer("sql", password);
var sqldb = sql.AddDatabase("sqldb");

builder
    .AddProject<Projects.Customer_Web>("customer-web", launchProfileName: "https")
    .WithEndpoint("https", endpoint => endpoint.IsProxied = false)
    .WithReference(sqldb);

builder.Build().Run();
