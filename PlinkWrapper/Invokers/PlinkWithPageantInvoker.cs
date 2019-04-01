namespace PlinkWrapper.Invokers
{
    class PlinkWithPageantInvoker : IInvoker
    {
        private readonly string args;
        private readonly string rsaKeyPath;

        public PlinkWithPageantInvoker(string args, string rsaKeyPath)
        {
            this.args = args;
            this.rsaKeyPath = rsaKeyPath;
        }

        public void Invoke()
        {
            StartProcessUtils.StartPageant(rsaKeyPath, args);
            StartProcessUtils.StartPlink($"-i {rsaKeyPath} {args}");
        }
    }
}