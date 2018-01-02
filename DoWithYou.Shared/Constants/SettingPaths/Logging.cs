using System;
using System.Collections.Generic;
using System.Text;

namespace DoWithYou.Shared.Constants.SettingPaths
{
    public static class Logging
    {
        public static string Root => "Logging";

        public static string IncludeScopes => $"{Root}:IncludeScopes";

        public static class LogLevel
        {
            public static string Root => $"{Logging.Root}:LogLevel";
            
            public static string Default => $"{Root}:Default";
        }
    }
}