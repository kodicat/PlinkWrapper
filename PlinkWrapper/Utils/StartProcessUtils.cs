using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PlinkWrapper.Utils
{
    static class StartProcessUtils
    {
        const string TortoisePlinkName = "TortoiseGitPlink.exe";
        const string PageantProcessName = "Pageant";
        const string PageantName = "pageant.exe";
        const string DefaultPuttyPath = @"c:\Program Files\PuTTY\";
        const string DefaultTortoisePath = @"c:\Program Files\TortoiseGit\bin\";

        static string PuttyPath => ConfigurationManager.AppSettings["PuttyPath"] ?? DefaultPuttyPath;
        static string TortoisePath => ConfigurationManager.AppSettings["TortoisePath"] ?? DefaultTortoisePath;

        internal static void StartPlink(string args)
        {
            var tortoisePlinkFullName = Path.Combine(TortoisePath, TortoisePlinkName);
            RunProcess(tortoisePlinkFullName, args);
        }

        internal static void StartPageant(string keyPath, string args)
        {
            var agentProcess = Process.GetProcessesByName(PageantProcessName).FirstOrDefault();

            if (agentProcess is null)
            {
                // Dirty trick to de-elevate (if needed) pagent privelleges.
                // Calling pageant from explorer. Call as separate process (useShellExecute).
                var puttyPageantFullName = Path.Combine(PuttyPath, PageantName);
                RunProcess("explorer", puttyPageantFullName, true);
            }

            var tortoisePageantFullName = Path.Combine(TortoisePath, PageantName);
            RunProcess(tortoisePageantFullName, $"{keyPath}");
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