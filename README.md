# cf-DotNetContoso
***

There are two versions of the app in this repo: V1 has a gray background on the home page, and V2 has a green background: this is to show a 'blue green' deployment scenario

You need to create two "user provided services" in PCF. The first is a connection to an SQL server:

    cf cups contoso-demo-sql -p 'connectionString'

the system will then prompt you for the connection string to the SQL database by showing `connectionString:`, and you can enter your SQL Server connection string (the SQL Server database can be hosted anywhere, e.g. on Azure). Replace the following parameters with the appropriate values: YOUR_IP, YOUR_USERID, YOUR_PASSWORD, YOUR_DATABASE

    Data Source=YOUR_IP;Integrated Security=False;User ID=YOUR_USERID;Password=YOUR_PASSWORD;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=YOUR_DATABASE

The second service is to an Azure Service Bus queue:

    cf cups contoso-demo-queue -p 'queueConnectionString'

the command line will then prompt you for the queue connection string with the prompt `queueConnectionString:`, and you can enter the information as follows. Replace the following parameters with the appropriate values: YOUR_QUEUE_HOST, YOUR_QUEUE_NAME, YOUR_QUEUE_KEY

    Endpoint=sb://YOUR_QUEUE_HOST;SharedAccessKeyName=YOUR_QUEUE_NAME;SharedAccessKey=YOUR_QUEUE_KEY

When creating the above services, please be sure to use only the variable names `connectionString` and `queueConnectionString` since the apps look for these strings when parsing the `VCAP_SERVICES` environment variable.

The code is provided pre-built so you can push without compiling the code.

There are batch files in each project folder (`demo1.bat` in contoso-university-V1 and `demo2.bat` in contoso-university-V2) that will issue the right push command and bind the right services.

Please see the supporting java app [available here](https://github.com/saurabhguptasg/servicebus "servicebus") that pumps messages into the Azure Service Bus: please note that the `contoso-demo-queue` service must be provided to this app before it can function correctly.

## Usage

The `https://app.domain.com/Messages` link in the app will give a list of messages from the queue

There are two actions on the top of the Messages page *only in the V1 app version* : Kill Instance and Expensive Task

The Kill Instance link will kill an instance; this can be used to show auto-recovery

The Expensive Task link will make the system undergo an expensive process, and the following endpoint can be hit with something like JMeter to stress the system and trigger and autoscaling operation: `https://app.domain.com/Message/PerformExpensiveTask`

Also, the bottom of each page contains an IP address and a random number uniquely assigned to each instance, so you can show load balancing with multiple instances, as the random number will change with each refresh to one of the n assigned numbers corresponding to each of the n instances of the app.

## Cloud Foundry

Use the following commands to deploy to Pivotal Cloud Foundry after you have created the User Provided Servies in the earlier steps.

On Linux:

    cd contoso-university-v1
    bash demo1.bat

    cd ../contoso-university-v2
    bash demo2.bat

On Windows:

    cd contoso-university-v1
    demo1.bat

    cd cd contoso-university-v2
    demo2.bat
