using Microsoft.Win32;

namespace PlinkWrapper
{
    static class Utils
    {
        const string publicKey = "PublicKeyFile";
        const string defaultSessionName = "Default%20Settings";
        const string puttySessionsregistryPath = @"Software\SimonTatham\PuTTY\Sessions\";

        internal static string GetPuttySessionKeyPath(string sessionName)
        {
            if (string.IsNullOrEmpty(sessionName))
                return null;

            var sessionPath = puttySessionsregistryPath + sessionName;
            return GetRegistryValue(sessionPath, publicKey)
                ?? GetRegistryValue(puttySessionsregistryPath + defaultSessionName, publicKey);
        }

        static string GetRegistryValue(string registryPath, string value)
        {
            using (var session = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                return session?.GetValue(value) as string;
            }
        }
    }
}