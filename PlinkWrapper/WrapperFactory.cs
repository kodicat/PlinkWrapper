using System.Linq;
using PlinkWrapper.Utils;
using PlinkWrapper.Wrappers;

namespace PlinkWrapper
{
    static class WrapperFactory
    {
        internal static IWrapper Create(string[] args)
        {
            var arguments = args.Aggregate((current, next) => current + " " + next);

            if (args.Contains("git@github.com"))
            {
                return CreateForGithub(arguments);
            }

            return new SimpleWrapper(arguments);
        }

        static IWrapper CreateForGithub(string arguments)
        {
            var accountName = GetGithubAccountName(arguments);
            var rsaKeyPath = RegistryUtils.GetPuttySessionKeyPath(accountName);

            if (rsaKeyPath is null)
            {
                return new SimpleWrapper(arguments);
            }

            return new PlinkWithPageantWrapper(arguments, rsaKeyPath);
        }

        static string GetGithubAccountName(string arguments)
        {
            var repositoryPath = arguments
                ?.Split(' ')
                .FirstOrDefault(x => x.Contains(".git"));

            return repositoryPath?.Split('/').FirstOrDefault()?.Trim('\'');
        }
    }
}