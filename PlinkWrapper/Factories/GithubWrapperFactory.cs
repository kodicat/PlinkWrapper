using System.Linq;
using PlinkWrapper.Utils;
using PlinkWrapper.Wrappers;

namespace PlinkWrapper.Factories
{
   sealed class GithubWrapperFactory
    {
        public static IWrapper Create(string arguments)
        {
            var accountName = GetAccountName(arguments);
            var rsaKeyPath = RepositoryUtils.GetPuttySessionKeyPath(accountName);

            if (rsaKeyPath is null)
            {
              return new SimpleWrapper(arguments);
            }
        
            return new PlinkWithPageantWrapper(arguments, rsaKeyPath);
        }
        
        static string GetAccountName(string arguments)
        {
            var repositoryPath = arguments
                ?.Split(' ')
                .FirstOrDefault(x => x.Contains(".git"));

            return repositoryPath?.Split('/').FirstOrDefault()?.Trim('\'');
        }
    }
}