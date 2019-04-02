using System.Linq;
using PlinkWrapper.Wrappers;

namespace PlinkWrapper.Factories
{
    static class WrapperFactory
    {
        internal static IWrapper Create(string[] args)
        {
            var arguments = args.Aggregate((current, next) => current + " " + next);

            if (args.Contains("git@github.com"))
            {
                return GithubWrapperFactory.Create(arguments);
            }

            return new SimpleWrapper(arguments);
        }
    }
}