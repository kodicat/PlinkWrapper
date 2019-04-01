using Microsoft.Win32;

namespace PlinkWrapper.Utils
{
    static class RepositoryUtils
    {
       internal static string GetPuttySessionKeyPath(string sessionName)
       {
           if (string.IsNullOrEmpty(sessionName)) return null;
           
           var sessionPath = $@"Software\SimonTatham\PuTTY\Sessions\{sessionName}";
           using (var session = Registry.CurrentUser.OpenSubKey(sessionPath))
           {
               return session?.GetValue("PublicKeyFile") as string;
           }
       }
   }
}