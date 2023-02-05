using Breadcrumb.DataAccess.Interface;
using Breadcrumb.Model;
using Breadcrumb.Utility;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Breadcrumb.DataAccess.Impl
{
    public class AccessDataAccess : IAccessDataAccess
    {
        private MSSqlDatabase MSSqlDatabase { get; set; }
        public AccessDataAccess(MSSqlDatabase msSqlDatabase)
        {
            MSSqlDatabase = msSqlDatabase;
        }
        public List<AccessModel> GetAllAccess(int page=1,int itemsPerPage=100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<AccessModel>();
			int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Access t  Order by t.EmailType OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
			cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new AccessModel()
                    {
                       GUIDAccess= reader.GetValue<String>("GUIDAccess"),
ID= reader.GetValue<String>("ID"),
Password= reader.GetValue<String>("Password"),
Name= reader.GetValue<String>("Name"),
EMail= reader.GetValue<String>("EMail"),
EmailSignature= reader.GetValue<String>("EmailSignature"),
BCCAddress= reader.GetValue<String>("BCCAddress"),
MessageAddr= reader.GetValue<String>("MessageAddr"),
Active= reader.GetBoolean("Active"),
GUIDSalesperson= reader.GetValue<String>("GUIDSalesperson"),
AutoOpenAlerts= reader.GetBoolean("AutoOpenAlerts"),
AutoOpenActivities= reader.GetBoolean("AutoOpenActivities"),
AutoOpenSchedule= reader.GetBoolean("AutoOpenSchedule"),
DefaultActivityType= reader.GetValue<String>("DefaultActivityType"),
DefaultScheduleClass= reader.GetValue<String>("DefaultScheduleClass"),
AutoOpenDashboard= reader.GetBoolean("AutoOpenDashboard"),
AutoOpenOrderManager= reader.GetBoolean("AutoOpenOrderManager"),
RestrictBySalesperson= reader.GetBoolean("RestrictBySalesperson"),
EmailSettingsOverride= reader.GetBoolean("EmailSettingsOverride"),
SMTPServer= reader.GetValue<String>("SMTPServer"),
SMTPUserName= reader.GetValue<String>("SMTPUserName"),
SMTPPassword= reader.GetValue<String>("SMTPPassword"),
EmailType= reader.IsDBNull(Helper.GetColumnOrder(reader,"EmailType")) ? (Int32?)null : reader.GetInt32("EmailType"),
SMTPEncryption= reader.GetValue<Int32>("SMTPEncryption"),
SMTPAuthRequired= reader.GetBoolean("SMTPAuthRequired"),
PrevWhatsNew= reader.IsDBNull(Helper.GetColumnOrder(reader,"PrevWhatsNew")) ? (Int32?)null : reader.GetInt32("PrevWhatsNew"),
LastWhatsNew= reader.IsDBNull(Helper.GetColumnOrder(reader,"LastWhatsNew")) ? (Int32?)null : reader.GetInt32("LastWhatsNew"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public List<AccessModel> SearchAccess(string searchKey, int page = 1, int itemsPerPage = 100,List<OrderByModel> orderBy=null)
        {
            var ret = new List<AccessModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Access t  WHERE  t.GUIDAccess LIKE CONCAT('%',@SearchKey,'%') OR t.ID LIKE CONCAT('%',@SearchKey,'%') OR t.Password LIKE CONCAT('%',@SearchKey,'%') OR t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.EMail LIKE CONCAT('%',@SearchKey,'%') OR t.EmailSignature LIKE CONCAT('%',@SearchKey,'%') OR t.BCCAddress LIKE CONCAT('%',@SearchKey,'%') OR t.MessageAddr LIKE CONCAT('%',@SearchKey,'%') OR t.GUIDSalesperson LIKE CONCAT('%',@SearchKey,'%') OR t.DefaultActivityType LIKE CONCAT('%',@SearchKey,'%') OR t.DefaultScheduleClass LIKE CONCAT('%',@SearchKey,'%') OR t.SMTPServer LIKE CONCAT('%',@SearchKey,'%') OR t.SMTPUserName LIKE CONCAT('%',@SearchKey,'%') OR t.SMTPPassword LIKE CONCAT('%',@SearchKey,'%') Order by t.EmailType OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
             if(orderBy!=null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText,orderBy);
            }
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var t = new AccessModel()
                    {
                       GUIDAccess= reader.GetValue<String>("GUIDAccess"),
ID= reader.GetValue<String>("ID"),
Password= reader.GetValue<String>("Password"),
Name= reader.GetValue<String>("Name"),
EMail= reader.GetValue<String>("EMail"),
EmailSignature= reader.GetValue<String>("EmailSignature"),
BCCAddress= reader.GetValue<String>("BCCAddress"),
MessageAddr= reader.GetValue<String>("MessageAddr"),
Active= reader.GetBoolean("Active"),
GUIDSalesperson= reader.GetValue<String>("GUIDSalesperson"),
AutoOpenAlerts= reader.GetBoolean("AutoOpenAlerts"),
AutoOpenActivities= reader.GetBoolean("AutoOpenActivities"),
AutoOpenSchedule= reader.GetBoolean("AutoOpenSchedule"),
DefaultActivityType= reader.GetValue<String>("DefaultActivityType"),
DefaultScheduleClass= reader.GetValue<String>("DefaultScheduleClass"),
AutoOpenDashboard= reader.GetBoolean("AutoOpenDashboard"),
AutoOpenOrderManager= reader.GetBoolean("AutoOpenOrderManager"),
RestrictBySalesperson= reader.GetBoolean("RestrictBySalesperson"),
EmailSettingsOverride= reader.GetBoolean("EmailSettingsOverride"),
SMTPServer= reader.GetValue<String>("SMTPServer"),
SMTPUserName= reader.GetValue<String>("SMTPUserName"),
SMTPPassword= reader.GetValue<String>("SMTPPassword"),
EmailType= reader.IsDBNull(Helper.GetColumnOrder(reader,"EmailType")) ? (Int32?)null : reader.GetInt32("EmailType"),
SMTPEncryption= reader.GetValue<Int32>("SMTPEncryption"),
SMTPAuthRequired= reader.GetBoolean("SMTPAuthRequired"),
PrevWhatsNew= reader.IsDBNull(Helper.GetColumnOrder(reader,"PrevWhatsNew")) ? (Int32?)null : reader.GetInt32("PrevWhatsNew"),
LastWhatsNew= reader.IsDBNull(Helper.GetColumnOrder(reader,"LastWhatsNew")) ? (Int32?)null : reader.GetInt32("LastWhatsNew"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public  int GetAllTotalRecordAccess()
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Access t";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        public int GetSearchTotalRecordAccess(string searchKey)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Access t  WHERE  t.GUIDAccess LIKE CONCAT('%',@SearchKey,'%') OR t.ID LIKE CONCAT('%',@SearchKey,'%') OR t.Password LIKE CONCAT('%',@SearchKey,'%') OR t.Name LIKE CONCAT('%',@SearchKey,'%') OR t.EMail LIKE CONCAT('%',@SearchKey,'%') OR t.EmailSignature LIKE CONCAT('%',@SearchKey,'%') OR t.BCCAddress LIKE CONCAT('%',@SearchKey,'%') OR t.MessageAddr LIKE CONCAT('%',@SearchKey,'%') OR t.GUIDSalesperson LIKE CONCAT('%',@SearchKey,'%') OR t.DefaultActivityType LIKE CONCAT('%',@SearchKey,'%') OR t.DefaultScheduleClass LIKE CONCAT('%',@SearchKey,'%') OR t.SMTPServer LIKE CONCAT('%',@SearchKey,'%') OR t.SMTPUserName LIKE CONCAT('%',@SearchKey,'%') OR t.SMTPPassword LIKE CONCAT('%',@SearchKey,'%')";
            cmd.Parameters.AddWithValue("@SearchKey", searchKey);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }

		

        public List<AccessModel> FilterAccess(List<FilterModel> filterBy,string andOr, int page, int itemsPerPage, List<OrderByModel> orderBy)
        {
            var ret = new List<AccessModel>();
            int offset = (page - 1) * itemsPerPage;
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT  t.* FROM Access t  {filterColumns} Order by t.EmailType OFFSET @Offset ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
            if(filterBy!=null && filterBy.Count > 0)
            {
            int paramCount=0;
                var whereClause = string.Empty;
                foreach (var r in filterBy)
                {
                    if (!string.IsNullOrEmpty(r.ColumnName))
                    {
                    paramCount++;
                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            whereClause=whereClause + " " + andOr + " ";
                        }
                        whereClause = whereClause + "t." + r.ColumnName + "=@" + r.ColumnName+paramCount;
                        cmd.Parameters.AddWithValue("@"+ r.ColumnName+paramCount, r.ColumnValue);
                    }
                }
                whereClause = whereClause.Trim();
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where " + whereClause);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "");
            }
            if (orderBy != null && orderBy.Count > 0)
            {
                cmd.CommandText = Helper.ConverOrderListToSQL(cmd.CommandText, orderBy);
            }
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@ItemsPerPage", itemsPerPage);
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                { 
                var t = new AccessModel()
                    {
                       GUIDAccess= reader.GetValue<String>("GUIDAccess"),
ID= reader.GetValue<String>("ID"),
Password= reader.GetValue<String>("Password"),
Name= reader.GetValue<String>("Name"),
EMail= reader.GetValue<String>("EMail"),
EmailSignature= reader.GetValue<String>("EmailSignature"),
BCCAddress= reader.GetValue<String>("BCCAddress"),
MessageAddr= reader.GetValue<String>("MessageAddr"),
Active= reader.GetBoolean("Active"),
GUIDSalesperson= reader.GetValue<String>("GUIDSalesperson"),
AutoOpenAlerts= reader.GetBoolean("AutoOpenAlerts"),
AutoOpenActivities= reader.GetBoolean("AutoOpenActivities"),
AutoOpenSchedule= reader.GetBoolean("AutoOpenSchedule"),
DefaultActivityType= reader.GetValue<String>("DefaultActivityType"),
DefaultScheduleClass= reader.GetValue<String>("DefaultScheduleClass"),
AutoOpenDashboard= reader.GetBoolean("AutoOpenDashboard"),
AutoOpenOrderManager= reader.GetBoolean("AutoOpenOrderManager"),
RestrictBySalesperson= reader.GetBoolean("RestrictBySalesperson"),
EmailSettingsOverride= reader.GetBoolean("EmailSettingsOverride"),
SMTPServer= reader.GetValue<String>("SMTPServer"),
SMTPUserName= reader.GetValue<String>("SMTPUserName"),
SMTPPassword= reader.GetValue<String>("SMTPPassword"),
EmailType= reader.IsDBNull(Helper.GetColumnOrder(reader,"EmailType")) ? (Int32?)null : reader.GetInt32("EmailType"),
SMTPEncryption= reader.GetValue<Int32>("SMTPEncryption"),
SMTPAuthRequired= reader.GetBoolean("SMTPAuthRequired"),
PrevWhatsNew= reader.IsDBNull(Helper.GetColumnOrder(reader,"PrevWhatsNew")) ? (Int32?)null : reader.GetInt32("PrevWhatsNew"),
LastWhatsNew= reader.IsDBNull(Helper.GetColumnOrder(reader,"LastWhatsNew")) ? (Int32?)null : reader.GetInt32("LastWhatsNew"),
                    };

                    ret.Add(t);
                }
            return ret;
        }

        public int GetFilterTotalRecordAccess(List<FilterModel> filterBy,string andOr)
        {
            var cmd = this.MSSqlDatabase.Connection.CreateCommand() as SqlCommand;
            cmd.CommandText = @"SELECT Count(*) as TotalRecord FROM Access t {filterColumns}";
            if (filterBy != null && filterBy.Count > 0)
            {
            int paramCount=0;
                var whereClause = string.Empty;
                foreach (var r in filterBy)
                {
                    if (!string.IsNullOrEmpty(r.ColumnName))
                    {paramCount++;
                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            whereClause = whereClause + " " + andOr + " ";
                        }
                        whereClause = whereClause + "t." + r.ColumnName + "=@" + r.ColumnName+paramCount;
                        cmd.Parameters.AddWithValue("@" + r.ColumnName+paramCount, r.ColumnValue);
                    }
                }
                whereClause = whereClause.Trim();
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "Where " + whereClause);
            }
            else
            {
                cmd.CommandText = cmd.CommandText.Replace("{filterColumns}", "");
            }
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    return reader.GetInt32("TotalRecord");
                }
            return 0;
        }
        
    }
}

