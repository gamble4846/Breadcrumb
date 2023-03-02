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
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Breadcrumb.Utility
{
    public static class UtilityCustom
    {
        private static readonly HttpClient _httpClient = new HttpClient();

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

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static async Task<dynamic> RestCall(string ApiLink)
        {
            dynamic data = await _httpClient.GetStringAsync(ApiLink);

            JObject json = JObject.Parse(data);
            var emptyArrays = json.Properties()
                .Where(p => p.Value.Type == JTokenType.Array && !p.Value.HasValues)
                .ToList();

            foreach (var property in emptyArrays)
            {
                property.Remove();
            }

            var CleanedJSON = json.ToString();

            dynamic ToReturn = JsonConvert.DeserializeObject<dynamic>(CleanedJSON);
            return ToReturn;
        }
    }
}

