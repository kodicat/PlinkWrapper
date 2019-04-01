using PlinkWrapper.Utils;

namespace PlinkWrapper.Invokers
{
    class SimpleInvoker : IInvoker
    {
        readonly string args;

        internal SimpleInvoker(string args)
        {
            this.args = args;
        }

        public void Invoke()
        {
            StartProcessUtils.StartPlink(args);
        }
    }
}