using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using SwitchPortConfigurator.Api.Repository.Entities;
using SwitchPortConfigurator.Api.Repository.Exceptions;
using SwitchPortConfigurator.Api.Repository.Interfaces;

namespace SwitchPortConfigurator.Api.Repository.Db.Implementations
{
    public class AreaRepository : Repository<AreaEntity, int, RepositoryDbContext>, IAreaRepository
    {
        public AreaRepository(ILogger<Repository<AreaEntity, int, RepositoryDbContext>> baseRepositoryLogger,
            RepositoryDbContext dbContext) : base(baseRepositoryLogger, dbContext)
        {
        }

        /*
        private void ThrowIfAnyError(Action<List<DetailsError>> action)
        {
            List<DetailsError> errors = new List<DetailsError>();

            action(errors);

            if (errors.Count > 0)

        }
        */

        protected override void ValidateCreate(AreaEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            //List<DetailsError> errors = new List<DetailsError>();

            /*
            if (entity.Name == null)
                throw new RepositoryCRUDException("Name is Reqiured.", "Area", new DetailsError(RepositoryErrorCode.Required, "Name"));
            
            if (entity.Name.Length < 2 || entity.Name.Length > 30)
                throw new RepositoryCRUDException("Name lenght must be beetween 2 and 30 inclusive.", "Area", new DetailsErrorStringLen("Name", 2, 30, true, true));
            */
            //"Exception was thrown during model validation, see details."

            if (entity.Name == null)
                throw new RepositoryCRUDException("Name is Reqiured.", "Area", new DetailsError(RepositoryErrorCode.Required, "Name"));
            
            if (entity.Name.Length < 2 || entity.Name.Length > 30)
                throw new RepositoryCRUDException("Name lenght must be beetween 2 and 30 inclusive.", "Area", new DetailsErrorStringLen("Name", 2, 30, true, true));

            /*
            if (errors.Count != 0)
                throw new RepositoryCRUDException("Exception was thrown during model validation, see details.", "Area", errors);
            */
        }

        protected override void ValidateCreateException(DbUpdateException exception)
        {
            throw exception switch
            {
                null => new ArgumentNullException(nameof(exception)),
                _ => exception.InnerException switch
                {
                    PostgresException postgresException => postgresException.ConstraintName switch
                    {
                        "PK_Areas" => new RepositoryCRUDException("Erorr add entity. Area with this Id arleady exist.", "Area", new DetailsError(RepositoryErrorCode.Already_Exist, "Id")),
                        "IX_Areas_Name" => new RepositoryCRUDException("Erorr add entity. Area with this Name arleady exist.", "Area", new DetailsError(RepositoryErrorCode.Already_Exist, "Name")),
                        _ => new RepositoryException("Error add entity. Not supported error Db."),
                    },
                    _ => new RepositoryException("Error add entity. Not supported Db provider.")
                }
            };

            /*
            ArgumentNullException.ThrowIfNull(exception, nameof(exception));

            if (exception.InnerException is not PostgresException postgresException)
                throw new RepositoryException("Error add entity. Not supported Db provider.");

            throw postgresException.ConstraintName switch
            {
                "PK_Areas" => new RepositoryCRUDException("Erorr add entity. Area with this Id arleady exist.", "Area", new DetailsError(RepositoryErrorCode.Already_Exist, "Id")),
                "IX_Areas_Name" => new RepositoryCRUDException("Erorr add entity. Area with this Name arleady exist.", "Area", new DetailsError(RepositoryErrorCode.Already_Exist, "Name")),
                _ => new RepositoryException("Error add entity. Not supported error Db."),
            };
            */
        }

        protected override void ValidateUpdate(AreaEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            List<DetailsError> errors = new List<DetailsError>();

            if (entity.Id <= 0)
                errors.Add(new DetailsErrorRange("Id", 1, int.MaxValue, true, true));

            if (entity.Name != null && (entity.Name.Length < 2 || entity.Name.Length > 30))
                errors.Add(new DetailsErrorRange("Name", 2, 30, true, true));

            if (errors.Count != 0)
                throw new RepositoryCRUDException("Exception was thrown during model validation, see details.", "Area", errors);
        }

        protected override void ValidateUpdateException(DbUpdateException exception)
        {
            throw exception switch
            {
                null => new ArgumentNullException(nameof(exception)),
                _ => exception.InnerException switch
                {
                    PostgresException postgresException => postgresException.ConstraintName switch
                    {
                        "PK_Areas" => new RepositoryCRUDException("Erorr update entity. Area with this Id not exist.", "Area", new DetailsError(RepositoryErrorCode.Not_Exist, "Id")),
                        "IX_Areas_Name" => new RepositoryCRUDException("Erorr update entity. Area with this Name arleady exist.", "Area", new DetailsError(RepositoryErrorCode.Already_Exist, "Name")),
                        _ => new RepositoryException("Error add update. Not supported error Db."),
                    },
                    _ => new RepositoryException("Error add entity. Not supported Db provider.")
                }
            };
        }

        protected override void ValidateDelete(int entity)
        {
            List<DetailsError> errors = new List<DetailsError>();

            if (entity <= 0)
                errors.Add(new DetailsErrorRange("Id", 1, int.MaxValue, true, true));

            if (errors.Count != 0)
                throw new RepositoryCRUDException("Exception was thrown during model validation, see details.", "Area", errors);
        }

        protected override void ValidateDeleteException(DbUpdateException exception)
        {
            throw exception switch
            {
                null => new ArgumentNullException(nameof(exception)),
                _ => exception.InnerException switch
                {
                    PostgresException postgresException => postgresException.ConstraintName switch
                    {
                        "PK_Areas" => new RepositoryCRUDException("Erorr delete entity. Area with this Id not exist.", "Area", new DetailsError(RepositoryErrorCode.Not_Exist, "Id")),
                        _ => new RepositoryException("Error add update. Not supported error Db."),
                    },
                    _ => new RepositoryException("Error add entity. Not supported Db provider.")
                }
            };
        }

        protected override AreaEntity GetEntityForDeleteWithId(int vId) =>
            new() { Id = vId };
    }
}
