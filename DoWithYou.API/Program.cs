using DoWithYou.Shared.Core;

namespace DoWithYou.API
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
