using System.Diagnostics;
using System.Linq;

namespace PlinkWrapper
{
    class StartProcessUtils
    {
        // ToDo: consider to make config variables
        const string client = "TortoiseGitPlink.exe";
        const string agent = "Pageant";
        const string localAgent = @"c:\Program Files\TortoiseGit\bin\pageant.exe";

        internal static void StartPlink(string args)
        {
            RunProcess(client, args);
        }

        internal static void StartPageant(string keyPath, string args)
        {
            var agentProcess = Process.GetProcessesByName(agent).FirstOrDefault();

            if (agentProcess is null)
            {
                RunProcess(agent, useShellExecute: true);
            }

            RunProcess(localAgent, $"{keyPath}");
        }

        static void RunProcess(string program, string args = "", bool useShellExecute = false)
        {
            var startInfo = new ProcessStartInfo
            {
                Arguments = args,
                UseShellExecute = useShellExecute,
                FileName = program
            };

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }
    }
}