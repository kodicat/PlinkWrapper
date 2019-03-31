using Microsoft.Win32;
using System.Diagnostics;
using System.Linq;

namespace PlinkWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsAsString = args.Aggregate((curr, next) => curr + " " + next);

            var account = GetAccountInfo(args);
            if (account is null)
            {
                StartPlink(argsAsString);
                return;
            }

            var keyPath = GetKeyPath(account);
            if (keyPath is null)
            {
                StartPlink(argsAsString);
                return;
            }

            StartPageant(keyPath, argsAsString);
            StartPlink($"-i {keyPath} {argsAsString}");
        }

        private static string GetAccountInfo(string[] args)
        {
            var repositoryArg = args.FirstOrDefault(x => x.Contains(".git"))?.Split(' ');

            if (repositoryArg != null && repositoryArg.Length == 2)
                return repositoryArg[1].Split('/')[0].Trim('\'');

            return null;
        }

        private static string GetKeyPath(string account)
        {
            var sessionPath = $@"Software\SimonTatham\PuTTY\Sessions\{account}";
            using (var session = Registry.CurrentUser.OpenSubKey(sessionPath))
            {
                if (session is null) return null;

                object o = session.GetValue("PublicKeyFile");
                if (o is null) return null;

                return o as string;
            }
        }

        private static void StartPlink(string args)
        {
            using (var process = StartProcess("TortoiseGitPlink.exe", args))
            {
                process.WaitForExit();
            }
        }

        private static void StartPageant(string keyPath, string args)
        {
            var existingPageant = Process.GetProcessesByName("PAGEANT").FirstOrDefault();

            if (existingPageant is null)
            {
                using (var mainPageant = StartProcess("pageant.exe", useShellExecute: true))
                {
                    mainPageant.WaitForExit();
                }
            }

            using (var tortoisePageant = StartProcess(@"c:\Program Files\TortoiseGit\bin\pageant.exe", $"{keyPath}"))
            {
                tortoisePageant.WaitForExit();
            }
        }

        private static Process StartProcess(string program, string args = "", bool useShellExecute = false)
        {
            return Process.Start(new ProcessStartInfo
            {
                Arguments = args,
                UseShellExecute = useShellExecute,
                FileName = program
            });
        }
    }
}