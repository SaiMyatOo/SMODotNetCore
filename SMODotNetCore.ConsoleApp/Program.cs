using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMODotNetCore.ConsoleApp;
using SMODotNetCore.ConsoleApp.DapperExamples;
using SMODotNetCore.ConsoleApp.EFCoreExamples;
using SMODotNetCore.ConsoleApp.Services;
using System.Data.SqlClient;

Console.WriteLine("CRUD With EFCore");
//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.read();
//adoDotNetExample.create("A Tree","Josha","josh@gmail.com");
//adoDotNetExample.update(1,"Resident Evil","Capcom","capcom@gmail.com");
//adoDotNetExample.delete(1);
//adoDotNetExample.edit(2);

//DapperExample de = new DapperExample(); 
//de.Run();

//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.RunEFCore();

var connectionString = ConnectionStrings.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
//adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadLine();