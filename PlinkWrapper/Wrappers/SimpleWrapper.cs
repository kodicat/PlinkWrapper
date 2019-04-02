using PlinkWrapper.Utils;

namespace PlinkWrapper.Wrappers
{
    class SimpleWrapper : IWrapper
    {
        readonly string args;

        internal SimpleWrapper(string args)
        {
            this.args = args;
        }

        public void Run() => StartProcessUtils.StartPlink(args);
   }
}