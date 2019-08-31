using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PlinkWrapper.Wrappers
{
    class WrapperWithPageant : WrapperBase
    {
        const string PageantProcessName = "Pageant";
        const string PageantName = "pageant.exe";

        readonly string arguments;
        readonly string rsaKeyPath;

        internal WrapperWithPageant(string arguments, string rsaKeyPath)
        {
            this.arguments = arguments;
            this.rsaKeyPath = rsaKeyPath;
        }

        public override void Run()
        {
            StartPageant(rsaKeyPath);
            StartPlink(rsaKeyPath, arguments);
        }

        static void StartPageant(string rsaKeyPath)
        {
            var pagentProcess = Process.GetProcessesByName(PageantProcessName).FirstOrDefault();

            var tortoisePageantFullName = Path.Combine(TortoisePath, PageantName);

            if (pagentProcess is null)
            {
                // Dirty trick to de-elevate (if needed) pagent privelleges.
                // Calling pageant from explorer. Call as separate process (useShellExecute).
                StartProcess("explorer", args: tortoisePageantFullName, useShellExecute: true);
            }

            StartProcess(tortoisePageantFullName, $"{rsaKeyPath}");
        }
    }
}