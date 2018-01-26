namespace DoWithYou.Shared.Repositories.Settings
{
    public class Serilog
    {
        #region PROPERTIES
        public string[] Enrich { get; set; }

        public MinimumLevel MinimumLevel { get; set; }

        public WriteTo[] WriteTo { get; set; }
        #endregion
    }

    public class MinimumLevel
    {
        #region PROPERTIES
        public string Default { get; set; }

        public Override Override { get; set; }
        #endregion
    }

    public class Override
    {
        #region PROPERTIES
        public string Microsoft { get; set; }

        public string System { get; set; }
        #endregion
    }

    public class WriteTo
    {
        #region PROPERTIES
        public Args Args { get; set; }

        public string Name { get; set; }
        #endregion
    }

    public class Args
    {
        #region PROPERTIES
        public string OutputTemplate { get; set; }

        public string PathFormat { get; set; }
        #endregion
    }
}