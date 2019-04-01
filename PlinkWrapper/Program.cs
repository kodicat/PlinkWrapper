namespace PlinkWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var invoker = InvokerFactory.Create(args);

            invoker.Invoke();
        }
    }
}