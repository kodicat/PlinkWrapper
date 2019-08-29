using Microsoft.Win32;

namespace PlinkWrapper.Utils
{
    static class RegistryUtils
    {
        const string publicKey = "PublicKeyFile";
        const string defaultSessionName = "Default%20Settings";
        const string puttySessionsregistryPath = @"Software\SimonTatham\PuTTY\Sessions\";

        internal static string GetPuttySessionKeyPath(string sessionName)
        {
            if (string.IsNullOrEmpty(sessionName)) return null;

            var sessionPath = puttySessionsregistryPath + sessionName;
            return GetValue(puttySessionsregistryPath + sessionName, publicKey)
                ?? GetValue(puttySessionsregistryPath + defaultSessionName, publicKey);
        }

        static string GetValue(string registryPath, string value)
        {
            using (var session = Registry.CurrentUser.OpenSubKey(registryPath))
            {
                return session?.GetValue(value) as string;
            }
        }
    }
}