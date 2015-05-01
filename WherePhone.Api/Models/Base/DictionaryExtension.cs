﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WherePhone.Api.Models.Base
{
    public static class DictionaryExtension
    {
        public static Dictionary<string, object> ToDictionary(this Object ob)
        {
            return ob.GetType()
                .GetRuntimeProperties()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(ob, null));

        } 
    }
}
