namespace SwitchPortConfigurator.Api.Repository.Exceptions
{
    public class DetailsErrorRange : DetailsError
    {
        public int MinLen { get; }

        public int MaxLen { get; }

        public bool MinEqual { get; }

        public bool MaxEqual { get; }

        public DetailsErrorRange(string field, int minLen, int maxLen, bool minEqual, bool maxEqual) : base(RepositoryErrorCode.Range, field)
        {
            MinLen = minLen;
            MaxLen = maxLen;
            MinEqual = minEqual;
            MaxEqual = maxEqual;
        }
    }
}
