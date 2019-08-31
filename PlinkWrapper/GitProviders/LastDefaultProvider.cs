using PlinkWrapper.Wrappers;

namespace PlinkWrapper.GitProviders
{
    class LastDefaultProvider : ProviderBase
    {
        protected override bool IsResponsible()
        {
            return true;
        }

        protected override IWrapper GetWrapper()
        {
            return new DefaultWrapper(arguments);
        }
    }
}