using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace PlinkWrapper.Wrappers
{
    abstract class WrapperBase : IWrapper
    {
        const string TortoisePlinkName = "TortoiseGitPlink.exe";
        const string DefaultTortoisePath = @"c:\Program Files\TortoiseGit\bin\";

        protected static string TortoisePath => ConfigurationManager.AppSettings["TortoisePath"] ?? DefaultTortoisePath;

        public abstract void Run();

        protected static void StartPlink(string args)
        {
            StartPlinkInternal(args);
        }

        protected static void StartPlink(string rsaKeyPath, string args)
        {
            StartPlinkInternal($"-i {rsaKeyPath} {args}");
        }

        protected static void StartProcess(string program, string args = "")
        {
            StartProcessInternal(program, args);
        }

        static void StartPlinkInternal(string args)
        {
            var tortoisePlinkFullName = Path.Combine(TortoisePath, TortoisePlinkName);
            StartProcessInternal(tortoisePlinkFullName, args);
        }

        static void StartProcessInternal(string program, string args)
        {
            var startInfo = new ProcessStartInfo
            {
                Arguments = args,
                UseShellExecute = false,
                FileName = program
            };

            using (var process = Process.Start(startInfo))
            {
                process?.WaitForExit();
            }
        }
    }
}