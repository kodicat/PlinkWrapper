namespace PlinkWrapper.Wrappers
{
    class NoRSAWrapper : WrapperBase
    {
        readonly string arguments;

        internal NoRSAWrapper(string arguments)
        {
            this.arguments = arguments;
        }

        public override void Run()
        {
            StartPlink(arguments);
        }
   }
}