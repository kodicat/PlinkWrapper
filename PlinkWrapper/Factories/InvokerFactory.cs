using System.Linq;
using PlinkWrapper.Invokers;

namespace PlinkWrapper.Factories
{
    static class InvokerFactory
    {
        internal static IInvoker Create(string[] args)
        {
            var arguments = args.Aggregate((current, next) => current + " " + next);

            if (args.Contains("git@github.com"))
            {
                return GithubInvokerFactory.Create(arguments);
            }

            return new SimpleInvoker(arguments);
        }
    }
}