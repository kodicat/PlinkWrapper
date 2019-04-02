using PlinkWrapper.Factories;

namespace PlinkWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var invoker = WrapperFactory.Create(args);

            invoker.Run();
        }
    }
}