using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwitchPortConfigurator.Api.Repository.Exceptions;
using SwitchPortConfigurator.Api.Repository.Interfaces;

namespace SwitchPortConfigurator.Api.Repository.Db.Implementations
{
    public abstract class Repository<TEntity, VId, EDbContext> : IRepository<TEntity, VId>
        where TEntity : class
        where EDbContext : DbContext
    {
        private readonly ILogger<Repository<TEntity, VId, EDbContext>> _logger;
        protected readonly EDbContext _dbContext;

        public Repository(ILogger<Repository<TEntity, VId, EDbContext>> logger, EDbContext dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected virtual void ValidateCreate(TEntity entity) { }
        protected virtual void ValidateCreateException(DbUpdateException exception) { }


        protected virtual void ValidateUpdate(TEntity entity) { }
        protected virtual void ValidateUpdateException(DbUpdateException exception) { }


        protected virtual void ValidateDelete(VId entity) { }
        protected virtual void ValidateDeleteException(DbUpdateException exception) { }

        protected abstract TEntity GetEntityForDeleteWithId(VId vId);

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default) =>
            await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);

        public async Task<TEntity> GetById(VId id, CancellationToken cancellationToken = default) =>
            await _dbContext.FindAsync<TEntity>(id, cancellationToken);

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            ValidateCreate(entity);

            TEntity result = null;

            try
            {
                _logger.LogTrace("Adding entity @{TEntity}.", entity);

                result = (await _dbContext.AddAsync(entity, cancellationToken)).Entity;
                
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Entity added: @{TEntity}.", result);
            }
            catch(OperationCanceledException e)
            {
                _logger.LogWarning(e, "Added entity: @{TEntity}, canceled.", entity);
                throw;
            }
            catch (DbUpdateException e)
            {
                _logger.LogWarning(e, "Error add entity: @{TEntity}.", entity);

                ValidateCreateException(e);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unknow error add entity: @{TEntity}.", entity);
                throw new RepositoryException("Unknow error add entity.", e);
            }

            return result;
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            ValidateUpdate(entity);

            TEntity result = null;

            try
            {
                _logger.LogTrace("Update entity @{TEntity}.", entity);

                result = _dbContext.Update(entity).Entity;

                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Entity updated: @{TEntity}.", result);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogWarning(e, "Updated entity: @{TEntity}, canceled.", entity);
                throw;
            }
            catch (DbUpdateException e)
            {
                _logger.LogWarning(e, "Error update entity: @{TEntity}.", entity);

                ValidateUpdateException(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unknow error update entity: @{TEntity}.", entity);
                throw new RepositoryException("Unknow error update entity.", e);
            }

            return result;
        }

        public async Task<TEntity> Delete(VId id, CancellationToken cancellationToken = default)
        {
            ValidateDelete(id);

            TEntity result = null;

            try
            {
                _logger.LogTrace("Delete entity with Id: @{VId}.", id);

                result = await _dbContext.FindAsync<TEntity>(id);

                _dbContext.Remove(GetEntityForDeleteWithId(id));

                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Entity updated: @{TEntity}.", result);
            }
            catch (OperationCanceledException e)
            {
                _logger.LogWarning(e, "Delete entity with Id: @{VId}, canceled.", id);
                throw;
            }
            catch (DbUpdateException e)
            {
                _logger.LogWarning(e, "Error delete entity: @{VId}.", id);

                ValidateUpdateException(e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unknow error delete entity: @{VId}.", id);
                throw new RepositoryException("Unknow error delete entity.", e);
            }

            return result;
        }
    }
}
