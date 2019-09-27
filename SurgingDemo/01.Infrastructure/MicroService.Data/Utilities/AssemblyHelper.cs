using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;

namespace MicroService.Data.Utilities
{
   public class AssemblyHelper
    {
        public static List<Assembly> CreateModulesByFilter(List<Assembly> assemblies, string filter)
        {
            List<Assembly> modules = new List<Assembly>();
            modules.AddRange(
                assemblies.Where(item => Regex.IsMatch(Path.GetFileNameWithoutExtension(item.CodeBase), filter)));
            return modules;
        }
        public static List<Assembly> GetAssemblyList(string path)
        {
            //dynamic type = (new Program()).GetType();

            //var assemblys= AppDomain.CurrentDomain.GetAssemblies();
            //return assemblys.ToList();
            string currentDirectory = Path.GetDirectoryName(path);
            var files = Directory.GetFiles(currentDirectory, "*.dll");
            var assemblys = new List<Assembly>();

            foreach (var file in files)
            {
                assemblys.Add(Assembly.LoadFrom(file));
            }

            return assemblys;
        }
        public static Assembly GetAssembly(string assemblyName)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
            return assembly;
        }
    }
}
