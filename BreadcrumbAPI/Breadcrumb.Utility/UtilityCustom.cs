using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using FastMember;

namespace Breadcrumb.Utility
{
    public static class UtilityCustom
    {
        public static T ConvertReaderToObject<T>(this SqlDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();
            try
            {
                for (int i = 0; i < rd.FieldCount; i++)
                {
                    if (!rd.IsDBNull(i))
                    {
                        string fieldName = rd.GetName(i);

                        if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                        {
                            accessor[t, fieldName] = rd.GetValue(i);
                        }
                    }
                }
            }
            catch(Exception ex) {
                var x = ex;
            }
            

            return t;
        }

        //var t = UtilityCustom.ConvertReaderToObject<LotserialinventoryModel>(reader);
    }
}

