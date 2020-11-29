using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JobFinder.Domain.Entities;
using Microsoft.Extensions.DependencyModel;

namespace JobFinder.Infrastructure.Data.DbContexts
{
    internal sealed class DbContextHelper
    {
        public static IList<Type> GetEntityTypes()
        {
            if (EntityTypes != null)
                return EntityTypes.ToArray();
            EntityTypes = GetReferencingAssemblies().SelectMany(x => x.DefinedTypes).Where(x => x.BaseType == typeof(BaseEntity)).Select(x => x.AsType()).ToArray();
            return EntityTypes;
        }

        public static readonly MethodInfo SetGlobalQueryMethod = typeof(SqlDbContext).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        private static IList<Type> EntityTypes { get; set; }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }
    }
}