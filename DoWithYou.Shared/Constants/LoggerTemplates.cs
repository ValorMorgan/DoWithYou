namespace DoWithYou.Shared.Constants
{
    public static class LoggerTemplates
    {
        #region VARIABLES
        public const string Configuring = "Configuring {Class}";

        public const string ConnectionType = "{Class} to use {ConnectionType} with {ConnectionString} connection string";

        public const string Constructor = "Constructing {Class}";

        public const string ConvertTo = "Converting {Value} to \"{Type}\"";

        public const string DataDelete = "Deleting {Type}[{EntityId}]";

        public const string DataGet = "Getting {Type}[{EntityId}]";

        public const string DataGetAll = "Getting all {Type}";

        public const string DataInsert = "Inserting {Type}[{EntityId}]";

        public const string DataMap = "Mapping {Table} for {Class}";

        public const string DataMapKeys = "Mapping {Table} Keys for {Class}";

        public const string DataMapProperties = "Mapping {Table} Properties for {Class}";

        public const string DataMapRelationships = "Mapping {Table} Relationships for {Class}";

        public const string DataMapTables = "Mapping table names for {Class}";

        public const string DataSaveChanges = "Saving Changes for {Type}";

        public const string DataUpdate = "Updating {Type}[{EntityId}]";

        public const string Disposing = "Disposing {Class}";

        public const string MigrateDown = "Migrating Down on {Migration}";

        public const string MigrateUp = "Migrating Up on {Migration}";

        public const string RegisterEvent = "Registering {Class} to event \"{Event}\"";

        public const string RequestDelete = "Requested to Delete {Type}[{EntityId}]";

        public const string RequestGet = "Requested to Get {Type}[{EntityId}]";

        public const string RequestGetDynamic = "Requested to Get {Type} via dynamic request";

        public const string RequestInsert = "Requested to Insert {Type}[{EntityId}]";

        public const string RequestSaveChanges = "Requested to SaveChanges for {Type}";

        public const string RequestUpdate = "Requested to Update {Type}[{EntityId}]";

        public const string RequestUpdateDynamic = "Requested to Update {Type} via dynamic request";
        #endregion
    }
}