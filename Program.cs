using LogicMonitor.Api;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace LogicMonitor.Cli.ExecuteDebug
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                string account;
                string accessId;
                string accessKey;
                int collectorId;
                string commandText;

                switch (args.Length)
                {
                    case 3:
                        var logicMonitorCredentials = LoadLogicMonitorCredentialsFromJsonFileInfo(new FileInfo(args[0]));
                        account = logicMonitorCredentials.Account;
                        accessId = logicMonitorCredentials.AccessId;
                        accessKey = logicMonitorCredentials.AccessKey;
                        collectorId = int.Parse(args[1]);
                        commandText = args[2];
                        break;
                    case 5:
                        account = args[0];
                        accessId = args[1];
                        accessKey = args[2];
                        collectorId = int.Parse(args[3]);
                        commandText = args[4];
                        break;
                    default:
                        Console.WriteLine("Usage:");
                        Console.WriteLine("There are two modes of operation.");
                        Console.WriteLine("1. provide the credentials at the command line:");
                        Console.WriteLine("   dotnet run \"<accountName>\" \"<accessId>\" \"<accessKey\" <collectorId> \"<command>\"");
                        Console.WriteLine("2. provide the credentials in a JSON file (see example.json):");
                        Console.WriteLine("   dotnet run \"<credenitalFilename.json>\" <collectorId> \"<command>\"");
                        return;
                }

                // Get input params

                var portalClient = new PortalClient(account, accessId, accessKey);

                var response = await portalClient
                .ExecuteDebugCommandAndWaitForResultAsync(collectorId, commandText)
                .ConfigureAwait(false);
                Console.WriteLine(response.Output);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
            }
        }

        /// <summary>
        /// Load LogicMonitor credentials from file
        /// </summary>
        /// <param name="jsonFileInfo">The FileInfo of the JSON file</param>
        /// <returns></returns>
        private static LogicMonitorCredentials LoadLogicMonitorCredentialsFromJsonFileInfo(FileInfo jsonFileInfo)
        {
            using (StreamReader r = new StreamReader(jsonFileInfo.FullName))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<LogicMonitorCredentials>(json);
            }
        }
    }
}
