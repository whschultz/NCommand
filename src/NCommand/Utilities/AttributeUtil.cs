using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tectil.NCommand.Utilities
{
    internal static class AttributeUtil
    {
        /// <summary>
        /// Get methodes by attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyNameRegex"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<MethodInfo, T>> GetMethodByAttribute<T>(string assemblyNameRegex)
            where T : class
        {
            return GetMethodByAttribute<T>( AppDomain.CurrentDomain.GetAssemblies().Where(x => Regex.Match(x.FullName, assemblyNameRegex).Success ) );
        }

        /// <summary>
        /// Get methodes by attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<MethodInfo, T>> GetMethodByAttribute<T>(IEnumerable<Assembly> assemblies)
            where T: class 
        {
            var methods = assemblies.SelectMany(assembly => assembly.GetTypes()
                      .SelectMany(t => t.GetMethods())
                      .Where(m => m.GetCustomAttributes(typeof(T), false).Length > 0)
                      ).Select(m => new Tuple<MethodInfo, T> (m, m.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T) ).ToList();
            return methods;
        }

        /// <summary>
        /// Get parameters and attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodeInfo"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<ParameterInfo, T, Type>> GetParametersAndAttributes<T>(MethodInfo methodeInfo)
            where T : class
        {
            var lst = methodeInfo.GetParameters()
                .Select(m => new Tuple<ParameterInfo, T, Type>(m, m.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T, m.ParameterType)).ToList();
            return lst;
        }
    }
}
