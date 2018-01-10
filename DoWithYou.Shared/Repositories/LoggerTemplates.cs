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
        public string Constructor => _config.Serilog.FindTemplate(Constants.LoggerTemplates.CONSTRUCTOR);

        public string ConvertTo => _config.Serilog.FindTemplate(Constants.LoggerTemplates.CONVERT_TO);

        public string DataDelete => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DATA_DELETE);

        public string DataGet => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DATA_GET);

        public string DataGetAll => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DATA_GET_ALL);

        public string DataInsert => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DATA_INSERT);

        public string DataSaveChanges => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DATA_SAVE_CHANGES);

        public string DataUpdate => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DATA_UPDATE);

        public string Dispose => _config.Serilog.FindTemplate(Constants.LoggerTemplates.DISPOSE);

        public string RegisterEvent => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REGISTER_EVENT);

        public string RequestDelete => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_DELETE);

        public string RequestGet => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_GET);

        public string RequestGetDynamic => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_GET_DYNAMIC);

        public string RequestInsert => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_INSERT);

        public string RequestSaveChanges => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_SAVE_CHANGES);

        public string RequestUpdate => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_UPDATE);

        public string RequestUpdateDynamic => _config.Serilog.FindTemplate(Constants.LoggerTemplates.REQUEST_UPDATE_DYNAMIC);
        #endregion

        #region CONSTRUCTORS
        public LoggerTemplates(AppConfig config)
        {
            _config = config;
        }
        #endregion
    }
}