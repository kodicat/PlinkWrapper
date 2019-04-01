using PlinkWrapper.Utils;

namespace PlinkWrapper.Invokers
{
    class PlinkWithPageantInvoker : IInvoker
    {
        readonly string args;
        readonly string rsaKeyPath;

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