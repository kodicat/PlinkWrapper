namespace PlinkWrapper.Invokers
{
    class PlinkInvoker : IInvoker
    {
        private readonly string args;

        internal PlinkInvoker(string args)
        {
            this.args = args;
        }

        public void Invoke()
        {
            StartProcessUtils.StartPlink(args);
        }
    }
}