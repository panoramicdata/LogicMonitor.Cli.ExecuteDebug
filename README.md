# LogicMonitor.Cli.ExecuteDebug
Run a LogicMonitor debug command at the command line

Usage:

Provide credentials at the command line:
````
dotnet run <accountName> "<accessId>" "<accessKey" <collectorId> "<command>"
````

Provide credentials in a json file:
````
dotnet run filename.json <collectorId> "<command>"
````

The json file should be formatted as:
````json
{
    "account": "<accountName>",
    "accessId": "<accessId>",
    "accessKey": "<accessKey>",
}
````