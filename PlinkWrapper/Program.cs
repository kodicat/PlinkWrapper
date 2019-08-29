namespace PlinkWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var wrapper = WrapperFactory.Create(args);

            wrapper.Run();
        }
    }
}