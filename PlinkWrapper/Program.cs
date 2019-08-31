namespace PlinkWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var wrapper = ProvidersChain.GetWrapper(args);
            wrapper.Run();
        }
    }
}