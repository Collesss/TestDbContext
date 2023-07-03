namespace SwitchPortConfigurator.Api.Repository.Exceptions
{
    public class RepositoryCRUDException : RepositoryException
    {
        public string Table { get; } = string.Empty;

        //public List<DetailsError> Details { get; } = new List<DetailsError>();

        private readonly List<DetailsError> _details = new();

        public IReadOnlyList<DetailsError> Details { get => _details; }

        public RepositoryCRUDException(string message) : base(message) { }

        public RepositoryCRUDException(string message, string table, DetailsError detailsError) : base(message)
        {
            Table = table;
            _details.Add(detailsError);
        }

        
        public RepositoryCRUDException(string message, string table, IEnumerable<DetailsError> details) : base(message)
        {
            Table = table;
            _details.AddRange(details);
        }
        

        public RepositoryCRUDException(string message, Exception innerException) : base(message, innerException) { }

        public RepositoryCRUDException(string message, Exception innerException, string table) : base(message, innerException)
        {
            Table = table;
        }

        public RepositoryCRUDException(string message, Exception innerException, string table, DetailsError details) : base(message, innerException)
        {
            Table = table;
            _details.Add(details);
        }

        
        public RepositoryCRUDException(string message, Exception innerException, string table, IEnumerable<DetailsError> details) : base(message, innerException)
        {
            Table = table;
            _details.AddRange(details);
        }
        
    }
}
