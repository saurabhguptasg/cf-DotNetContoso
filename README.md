# cf-DotNetContoso
==================

There are two versions of the app in this repo: V1 has a gray background on the home page, and V2 has a green background: this is to show a 'blue green' deployment scenario

You need to create two "user provided services" in PCF. The first is a connection to an SQL server:

  cf cups contoso-demo-sql -p 'connectionString'

the system will then prompt you for the connection string to the SQL database by showing `connectionString:`, and you can enter your SQL Server connection string (the SQL Server database can be hosted anywhere, e.g. on Azure):

  connectionString: Data Source=xxxxxxxxx.database.windows.net;Integrated Security=False;User ID=xxxxxxx;Password=xxxxxx;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=ContosoUniversity2

The second service is to an Azure Service Bus queue:

  cf cups contoso-demo-queue -p 'queueConnectionString'

the command line will then prompt you for the queue connection string with the prompt `queueConnectionString:`, and you can enter the information as follows:

  queueConnectionString: Endpoint=sb://xxxxxxxxxx.servicebus.windows.net/;SharedAccessKeyName=read-write;SharedAccessKey=xxxxxxSomeBase64EncodedStringxxxxx

The code is provided pre-built so you can push without compiling the code.

There are batch files in each project folder (`demo1.bat` in contoso-university-V1 and `demo2.bat` in contoso-university-V2) that will issue the right push command and bind the right services.

