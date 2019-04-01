using Microsoft.Win32;
using PlinkWrapper.Invokers;
using System.Linq;

namespace PlinkWrapper
{
    static class InvokerFactory
    {
        internal static IInvoker Create(string[] args)
        {
            var arguments = args.Aggregate((curr, next) => curr + " " + next);

            if (!args.Contains("git@github.com"))
            {
                return new PlinkInvoker(arguments);
            }
            var account = GetAccountInfo(args);
            var rsaKeyPath = GetRsaPuttyKeyPath(account);

            if (rsaKeyPath is null)
            {
                return new PlinkInvoker(arguments);
            }

            return new PlinkWithPageantInvoker(arguments, rsaKeyPath);
        }

        static string GetAccountInfo(string[] args)
        {
            var repositoryArg = args.FirstOrDefault(x => x.Contains(".git"))?.Split(' ');

            if (repositoryArg != null && repositoryArg.Length == 2)
                return repositoryArg[1].Split('/')[0].Trim('\'');

            return null;
        }

        static string GetRsaPuttyKeyPath(string account)
        {
            if (string.IsNullOrEmpty(account)) return null;

            var sessionPath = $@"Software\SimonTatham\PuTTY\Sessions\{account}";
            using (var session = Registry.CurrentUser.OpenSubKey(sessionPath))
            {
                if (session is null) return null;

                object o = session.GetValue("PublicKeyFile");
                if (o is null) return null;

                return o as string;
            }
        }
    }
}