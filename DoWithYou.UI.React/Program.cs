using System;
using DoWithYou.Shared.Core;

namespace DoWithYou.UI.React
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                using (var host = new WebHost(args, typeof(Startup)))
                    return host.Run();
            }
            catch
            {
                return 1;
            }
        }
    }
}