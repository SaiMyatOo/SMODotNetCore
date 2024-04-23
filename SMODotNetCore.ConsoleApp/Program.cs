using SMODotNetCore.ConsoleApp;

Console.WriteLine("CRUD With EFCore");
//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.read();
//adoDotNetExample.create("A Tree","Josha","josh@gmail.com");
//adoDotNetExample.update(1,"Resident Evil","Capcom","capcom@gmail.com");
//adoDotNetExample.delete(1);
//adoDotNetExample.edit(2);

//DapperExample de = new DapperExample(); 
//de.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.RunEFCore();