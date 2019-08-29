﻿using PlinkWrapper.Utils;

namespace PlinkWrapper.Wrappers
{
    class PlinkWithPageantWrapper : IWrapper
    {
        readonly string args;
        readonly string rsaKeyPath;

        public PlinkWithPageantWrapper(string args, string rsaKeyPath)
        {
            this.args = args;
            this.rsaKeyPath = rsaKeyPath;
        }

        public void Run()
        {
            ProcessUtils.StartPageant(rsaKeyPath, args);
            ProcessUtils.StartPlink($"-i {rsaKeyPath} {args}");
        }
    }
}