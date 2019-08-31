using PlinkWrapper.Wrappers;
using System.Linq;

namespace PlinkWrapper.GitProviders
{
    class GithubProvider : ProviderBase
    {
        const string providerSshAddress = "git@github.com";

        static string rsaKeyPath;

        protected override bool IsResponsible()
        {
            var args = arguments.Split(' ').ToArray();
            if (!args.Contains(providerSshAddress))
                return false;

            var accountName = GetGithubAccountName(args);
            if (accountName is null)
                return false;

            rsaKeyPath = Utils.GetPuttySessionKeyPath(accountName);
            if (rsaKeyPath is null)
                return false;

            return true;
        }

        protected override IWrapper GetWrapper()
        {
            return new RSAWrapper(arguments, rsaKeyPath);
        }

        static string GetGithubAccountName(string[] args)
        {
            return args
                .FirstOrDefault(x => x.Contains(".git"))
                ?.Split('/')
                .FirstOrDefault()
                ?.Trim('\'');
        }
    }
}