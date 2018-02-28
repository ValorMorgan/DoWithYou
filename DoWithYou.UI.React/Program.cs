using DoWithYou.Shared.Core;

namespace DoWithYou.UI.React
{
    public class Program
    {
        public static int Main(string[] args)
        {
            using (var host = new WebHost(args, typeof(Startup)))
                return host.Run();
        }
    }
}