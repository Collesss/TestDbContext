namespace SwitchPortConfigurator.Api.Repository.Exceptions
{
    public class DetailsErrorNotExistForeginKey : DetailsError
    {
        public string RefferenceTable { get; }
        
        public string RefferenceKey { get; }

        public DetailsErrorNotExistForeginKey(string field, string refferenceTable, string refferenceKey) : base(RepositoryErrorCode.Not_Exist_Foregin_Key, field)
        {
            RefferenceTable = refferenceTable;
            RefferenceKey = refferenceKey;
        }
    }
}
