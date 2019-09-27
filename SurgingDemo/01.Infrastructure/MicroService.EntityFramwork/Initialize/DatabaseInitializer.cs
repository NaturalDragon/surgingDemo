using MicroService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MicroService.EntityFramwork.Initialize
{
  public static  class DatabaseInitializer
    {

        private static readonly ICollection<Assembly> MapperAssemblies = new List<Assembly>();

        public static ICollection<IEntity> EntityMappers { get { return AllEntityMapper; } }

        private static ICollection<IEntity> AllEntityMapper
        {
            get
            {
                Type baseType = typeof(IEntity);
                Type[] mapperTypes = MapperAssemblies.SelectMany(assembly => assembly.GetTypes())
                    .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
                ICollection<IEntity> result = mapperTypes.Select(type => Activator.CreateInstance(type) as IEntity).ToList();
                return result;
            }
        }
    }
}
