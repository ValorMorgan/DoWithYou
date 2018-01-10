namespace DoWithYou.Interface.Shared
{
    public interface ILoggerTemplates
    {
        string Constructor { get; }

        string ConvertTo { get; }

        string DataDelete { get; }

        string DataGet { get; }

        string DataGetAll { get; }

        string DataInsert { get; }

        string DataSaveChanges { get; }

        string DataUpdate { get; }

        string Dispose { get; }

        string RegisterEvent { get; }

        string RequestDelete { get; }

        string RequestGet { get; }

        string RequestGetDynamic { get; }

        string RequestInsert { get; }

        string RequestSaveChanges { get; }

        string RequestUpdate { get; }

        string RequestUpdateDynamic { get; }
    }
}