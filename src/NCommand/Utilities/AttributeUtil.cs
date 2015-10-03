using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Tectil.NCommand.Contract;

namespace Tectil.NCommand.Utilities
{
    /// <summary>
    /// Utility functions for attributes.
    /// </summary>
    internal static class AttributeUtil
    {
        /// <summary>
        /// Get methodes by attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyNameRegex"></param>
        /// <returns></returns>
        public static MethodResult<T> GetMethodByAttribute<T>(string assemblyNameRegex)
            where T : class
        {
            return GetMethodByAttribute<T>( AppDomain.CurrentDomain.GetAssemblies().Where(x => Regex.Match(x.FullName, assemblyNameRegex).Success ) );
        }

        /// <summary>
        /// Get methodes by attribute.
        /// </summary>
        /// <typeparam name="T">Attribute</typeparam>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static MethodResult<T> GetMethodByAttribute<T>(IEnumerable<Assembly> assemblies)
            where T: class
        {
            var exceptions = new List<Exception>();
            var methods = assemblies.SelectMany(assembly =>
            {
                try
                {
                    var methodes = assembly.GetTypes()
                        .SelectMany(t => t.GetMethods())
                        .Where(m => m.GetCustomAttributes(typeof(T), false).Length > 0);
                    return methodes;
                }
                catch (ReflectionTypeLoadException ex)
                {
                    exceptions.AddRange(ex.LoaderExceptions);
                    return new List<MethodInfo>();
                }
            }).Select(m => new MethodeAttribute<T>(){ MethodInfo = m, Attribute = m.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T } ).ToList();
            return new MethodResult<T>()
            {
                Methodes = methods,
                Exceptions = exceptions
            };
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
