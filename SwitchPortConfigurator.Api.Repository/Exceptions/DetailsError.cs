namespace SwitchPortConfigurator.Api.Repository.Exceptions
{
    public class DetailsError
    {
        public string Field { get; }

        public RepositoryErrorCode ErrorCode { get; }

        public DetailsError()
        {
            Field = string.Empty;
            ErrorCode = RepositoryErrorCode.Unknown;
        }

        public DetailsError(RepositoryErrorCode errorCode, string field) 
        {
            ErrorCode = errorCode;
            Field = field;
        }

        /*
        public virtual string[] DetailsErorToArrayString() =>
            new string[] { ErrorCode.ToString(), Field };
        */
    }
}
