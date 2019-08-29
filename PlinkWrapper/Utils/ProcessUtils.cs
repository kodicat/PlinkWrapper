using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PlinkWrapper.Utils
{
    static class ProcessUtils
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
            var pagentProcess = Process.GetProcessesByName(PageantProcessName).FirstOrDefault();

            var tortoisePageantFullName = Path.Combine(TortoisePath, PageantName);

            if (pagentProcess is null)
            {
                // Dirty trick to de-elevate (if needed) pagent privelleges.
                // Calling pageant from explorer. Call as separate process (useShellExecute).
                RunProcess("explorer", tortoisePageantFullName, true);
            }

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