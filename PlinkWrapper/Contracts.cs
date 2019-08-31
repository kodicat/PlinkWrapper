namespace PlinkWrapper
{
    interface IWrapper
    {
        void Run();
    }

    interface IChainProvider<T>
    {
        T Provide(string args);
        IChainProvider<T> RegisterNext(IChainProvider<T> next);
    }
}