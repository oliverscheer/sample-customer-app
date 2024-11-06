var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("SqlServerSaPassword", secret: true);

var sql = builder.AddSqlServer("sql", password);
var sqldb = sql.AddDatabase("sqldb");

builder
    .AddProject<Projects.Customer_Web>("customer-web")
    .WithReference(sqldb);

builder.Build().Run();
