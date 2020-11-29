using JobFinder.Domain.Entities;
using JobFinder.Domain.Exceptions;

namespace JobFinder.Domain.Extensions
{
    public static class Guard
    {
        public static void CheckIfExist<TEntity>(this TEntity entity) where TEntity : BaseEntity
        {
            if (entity == default)
                throw new SmartException(typeof(TEntity).Name + " not found");
        }

        public static void CheckIfExist<TEntity>(this TEntity entity, string errorMessage) where TEntity : BaseEntity
        {
            if (entity == default)
                throw new SmartException(errorMessage);
        }

        public static void NotNull(object arg, string argName)
        {
            if (arg == default)
                throw new SmartException(argName + " not found");
        }
    }
}