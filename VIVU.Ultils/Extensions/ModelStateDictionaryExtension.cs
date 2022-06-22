using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIVU.Ultils.Extensions
{
    public static class ModelStateDictionaryExtension
    {
        public static string GetError(this ModelStateDictionary modelState)
        {
            string result = string.Empty;

            modelState.Values.ToList().ForEach(value =>
            {
                result += "-";
                result += string.Join("-", value.Errors.Select(x => x.ErrorMessage));
            });

            return result;
        }
    }
}
