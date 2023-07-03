namespace SwitchPortConfigurator.Api.Repository.Exceptions
{
    public class DetailsErrorStringLen : DetailsError
    {
        public int MinLen { get; }

        public int MaxLen { get; }

        public bool MinEqual { get; }

        public bool MaxEqual { get; }

        public DetailsErrorStringLen(string field, int minLen, int maxLen, bool minEqual, bool maxEqual) : base(RepositoryErrorCode.String_Len, field)
        {
            MinLen = minLen;
            MaxLen = maxLen;
            MinEqual = minEqual;
            MaxEqual = maxEqual;
        }
    }
}
