namespace DoWithYou.Shared.Constants
{
    public static class LoggerTemplates
    {
        #region VARIABLES
        public const string CONFIGURING = "Configuring {Class}";

        public const string CONNECTION_TYPE = "{Class} to use {ConnectionType} with {ConnectionString} connection string";

        public const string CONSTRUCTOR = "Constructing {Class}";

        public const string CONVERT_TO = "Converting {Value} to \"{Type}\"";

        public const string DATA_DELETE = "Deleting {Entity}[{EntityId}]";

        public const string DATA_GET = "Getting {Entity}[{EntityId}]";

        public const string DATA_GET_ALL = "Getting all {Entity}";

        public const string DATA_INSERT = "Inserting {Entity}[{EntityId}]";

        public const string DATA_MAP = "Mapping {Table} for {Class}";

        public const string DATA_MAP_KEYS = "Mapping {Table} Keys for {Class}";

        public const string DATA_MAP_PROPERTIES = "Mapping {Table} Properties for {Class}";

        public const string DATA_MAP_RELATIONSHIPS = "Mapping {Table} Relationships for {Class}";

        public const string DATA_MAP_TABLES = "Mapping table names for {Class}";

        public const string DATA_SAVE_CHANGES = "Saving Changes for {Entity}";

        public const string DATA_UPDATE = "Updating {Entity}[{EntityId}]";

        public const string DISPOSING = "Disposing {Class}";

        public const string LOG_WEB_REQUEST = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";

        public const string MAP_MODEL_TO_ENTITY_1 = "Mapping {Model} to {Entity}";

        public const string MAP_ENTITY_TO_MODEL_1 = "Mapping {Entity} to {Model}";

        public const string MAP_MODEL_TO_ENTITY_2 = "Mapping {Model} to {Entity1} & {Entity2}";

        public const string MAP_ENTITY_TO_MODEL_2 = "Mapping {Entity1} & {Entity2} to {Model}";

        public const string MIGRATE_DOWN = "Migrating Down on {Migration}";

        public const string MIGRATE_UP = "Migrating Up on {Migration}";

        public const string REGISTER_EVENT = "Registering {Class} to event \"{Event}\"";

        public const string REQUEST_DELETE = "Requested to Delete {Entity}[{EntityId}]";

        public const string REQUEST_GET = "Requested to Get {Entity}[{EntityId}]";

        public const string REQUEST_GET_DYNAMIC = "Requested to Get {Entity} via dynamic request";

        public const string REQUEST_INSERT = "Requested to Insert {Entity}[{EntityId}]";

        public const string REQUEST_SAVE_CHANGES = "Requested to SaveChanges for {Entity}";

        public const string REQUEST_UPDATE = "Requested to Update {Entity}[{EntityId}]";

        public const string REQUEST_UPDATE_DYNAMIC = "Requested to Update {Entity} via dynamic request";
        #endregion
    }
}