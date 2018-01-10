namespace DoWithYou.Shared.Repositories.Settings
{
    public class Logging
    {
        #region PROPERTIES
        public bool IncludeScopes { get; set; }

        public LogLevel LogLevel { get; set; }
        #endregion
    }

    public class LogLevel
    {
        #region PROPERTIES
        public string Default { get; set; }
        #endregion
    }
}