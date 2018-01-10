namespace DoWithYou.Shared.Repositories.Settings
{
    public class AppConfig
    {
        #region PROPERTIES
        public ConnectionStrings[] ConnectionStrings { get; set; }

        public Logging Logging { get; set; }

        public Serilog Serilog { get; set; }
        #endregion
    }
}