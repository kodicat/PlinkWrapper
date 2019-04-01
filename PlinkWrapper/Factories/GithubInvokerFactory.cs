using System.Linq;
using PlinkWrapper.Invokers;
using PlinkWrapper.Utils;

namespace PlinkWrapper.Factories
{
    sealed class GithubInvokerFactory
    {
        public static IInvoker Create(string arguments)
        {
            var accountName = GetAccountName(arguments);
            var rsaKeyPath = RepositoryUtils.GetPuttySessionKeyPath(accountName);
        
            if (rsaKeyPath is null)
            {
              return new SimpleInvoker(arguments);
            }
        
           return new PlinkWithPageantInvoker(arguments, rsaKeyPath);
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