namespace PlinkWrapper.Wrappers
{
    class DefaultWrapper : WrapperBase
    {
        readonly string arguments;

        internal DefaultWrapper(string arguments)
        {
            this.arguments = arguments;
        }

        public override void Run()
        {
            StartPlink(arguments);
        }
   }
}