namespace PlinkWrapper.GitProviders
{
    abstract class ProviderBase : IChainProvider<IWrapper>
    {
        IChainProvider<IWrapper> next;

        protected string arguments;

        protected abstract bool IsResponsible();
        protected abstract IWrapper GetWrapper();

        public IWrapper Provide(string arguments)
        {
            this.arguments = arguments;

            return IsResponsible()
                ? GetWrapper()
                : next.Provide(arguments);
        }

        public IChainProvider<IWrapper> RegisterNext(IChainProvider<IWrapper> next)
        {
            if (next is null)
                throw new System.ArgumentNullException(nameof(next));

            this.next = next;
            return next;
        }
    }
}