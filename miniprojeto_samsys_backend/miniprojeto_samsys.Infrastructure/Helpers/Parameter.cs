using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace miniprojeto_samsys.Infrastructure.Helpers
{
    public class Parameter
    {
        public string Name { get; init; }
        public string? Value { get; init; }
        public Parameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Returns the necessary parameters based in the necessaryParameters array. If one parameter name of the Origin Parameters List isn't in the necessary parameters, it will be ignore. If a necessary parameter isn't present, will be created a Parameter with the default value.
        /// </summary>
        /// <param name="necessaryParameters">Necessary Parameters</param>
        /// <param name="originParameters">List of Origin Parameters</param>
        /// <param name="valueWhenParameterNotFound">When a parameter isn't present, this value will be used to create a new one</param>
        /// <returns></returns>
        public static List<Parameter> LoadFrom(string[] necessaryParameters, List<Parameter>? originParameters, dynamic? valueWhenParameterNotFound)
        {
            List<Parameter> list = new List<Parameter>();

            if (necessaryParameters == null) return list;

            foreach (var parameter in necessaryParameters)
            {
                Parameter? parameterExists = null;
                if (originParameters != null)
                {
                    parameterExists = originParameters.FirstOrDefault(p => p.Name == parameter);
                }
                list.Add(new Parameter(parameter, parameterExists != null ? parameterExists.Value : valueWhenParameterNotFound));
            }

            return list;
        }

        /// <summary>
        /// Return just the parameters which name is contained in the availableParameters array. If a parameter has a name that isn't in the list, it will be ignored.
        /// </summary>
        /// <param name="availableParameters">Array of parameter names that are available</param>
        /// <param name="originParameters">Parameters to check</param>
        /// <returns></returns>
        public static List<Parameter> LoadFromAvailableParameters(string[] availableParameters, List<Parameter> originParameters)
        {
            List<Parameter> list = new List<Parameter>();

            if (originParameters == null) return list;

            foreach (var parameter in availableParameters)
            {
                var parameterExists = originParameters.FirstOrDefault(p => p.Name == parameter);
                if (parameterExists != null)
                {
                    list.Add(parameterExists);
                }
            }

            return list;
        }
    }
}