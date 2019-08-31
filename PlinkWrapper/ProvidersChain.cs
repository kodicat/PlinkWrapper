using PlinkWrapper.GitProviders;

namespace PlinkWrapper
{
    static class ProvidersChain
    {
        static readonly IChainProvider<IWrapper> chainStart;

        static ProvidersChain()
        {
            chainStart = new GithubProvider();

            // chain providers here
            chainStart
                .RegisterNext(new LastDefaultProvider());
        }

        public static IWrapper GetWrapper(string[] args)
        {
            return chainStart.Provide(args.ToJoinedArguments());
        }

        static string ToJoinedArguments(this string[] args)
        {
            return string.Join(" ", args);
        }
    }
}