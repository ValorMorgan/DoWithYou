using DoWithYou.Interface.Shared;
using DoWithYou.Shared.Repositories.Settings;

namespace DoWithYou.Shared.Repositories
{
    public class LoggerTemplates : ILoggerTemplates
    {
        #region VARIABLES
        private readonly AppConfig _config;
        #endregion

        #region PROPERTIES
        public string Configuring => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.CONFIGURING);

        public string ConnectionType => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.CONNECTION_TYPE);

        public string Constructor => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.CONSTRUCTOR);

        public string ConvertTo => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.CONVERT_TO);

        public string DataDelete => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_DELETE);

        public string DataGet => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_GET);

        public string DataGetAll => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_GET_ALL);

        public string DataInsert => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_INSERT);

        public string DataMap => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_MAP);

        public string DataMapKeys => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_MAP_KEYS);

        public string DataMapProperties => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_MAP_PROPERTIES);

        public string DataMapRelationships => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_MAP_RELATIONSHIPS);

        public string DataMapTables => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_MAP_TABLES);

        public string DataSaveChanges => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_SAVE_CHANGES);

        public string DataUpdate => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DATA_UPDATE);

        public string Dispose => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.DISPOSE);

        public string RegisterEvent => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REGISTER_EVENT);

        public string RequestDelete => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_DELETE);

        public string RequestGet => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_GET);

        public string RequestGetDynamic => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_GET_DYNAMIC);

        public string RequestInsert => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_INSERT);

        public string RequestSaveChanges => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_SAVE_CHANGES);

        public string RequestUpdate => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_UPDATE);

        public string RequestUpdateDynamic => _config.Serilog.FindTemplate(Constants.LoggerTemplateNames.REQUEST_UPDATE_DYNAMIC);
        #endregion

        #region CONSTRUCTORS
        public LoggerTemplates(AppConfig config)
        {
            _config = config;
        }
        #endregion
    }
}