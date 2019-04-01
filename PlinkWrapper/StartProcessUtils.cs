using System.Diagnostics;
using System.Linq;

namespace PlinkWrapper
{
    class StartProcessUtils
    {
        // TODO: consider to make config variables
        const string ClientFullName = @"c:\Program Files\TortoiseGit\bin\TortoiseGitPlink.exe";
        const string AgentProcessName = "Pageant";
        const string AgentFullName = @"c:\Program Files\PuTTY\pageant.exe";
        const string LocalAgentFullName = @"c:\Program Files\TortoiseGit\bin\pageant.exe";

        internal static void StartPlink(string args)
        {
            RunProcess(ClientFullName, args);
        }

        internal static void StartPageant(string keyPath, string args)
        {
            var agentProcess = Process.GetProcessesByName(AgentProcessName).FirstOrDefault();

            if (agentProcess is null)
            {
               // Dirty trick to de-elevate (if needed) pagent privelleges.
               // Calling pageant from explorer.
               RunProcess("explorer", AgentFullName, true);
            }

            RunProcess(LocalAgentFullName, $"{keyPath}");
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
                process?.WaitForExit();
            }
        }
    }
}