using Breadcrumb.Utility;
using Breadcrumb.DataAccess.SQLServer.Interface;
using Breadcrumb.DataAccess.SqlClient;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using Breadcrumb.Model.vTvShowsModels;
using Breadcrumb.Model.tbSeasonsModel;
using Breadcrumb.Model.tbEpisodesModels;
using Breadcrumb.Model.tbCoversModels;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace Breadcrumb.DataAccess.SQLServer.Impl
{
    public class CoversDataAccess : ICoversDataAccess
    {
        private String ConnectionString { get; set; }
        private CommonFunctions CommonFunctions { get; set; }

        public CoversDataAccess(String connectionString, CommonFunctions commonFunctions)
        {
            CommonFunctions = commonFunctions;
            ConnectionString = connectionString;
        }

        public List<tbCoversModel> GetCoverByBreadId(Guid BreadId)
        {
            var ret = new List<tbCoversModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM tbCovers t WHERE BreadId = @BreadId";
                SqlCommand cmd = new SqlCommand(CommandText, connection);
                cmd.Parameters.AddWithValue("@BreadId", BreadId);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbCoversModel>();
                return ret;
            }
        }

        public List<tbCoversModel> GetCoverByBreadIds(string BreadIds)
        {
            var ret = new List<tbCoversModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"SELECT  t.* FROM tbCovers t WHERE BreadId IN (" + BreadIds + ")";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                var dt = UtilityCustom.GetDataTableFromCommand(cmd);
                ret = dt.DataTableToObjectList<tbCoversModel>();
                return ret;
            }
        }

        public bool InsertUpdateDeleteCoversForSingleBread(List<string> CoversToInsertQueries, List<string> CoversToUpdateQueries, string CoversToDeleteQuery)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string CommandText = @"";
                SqlCommand cmd = new SqlCommand(CommandText, connection);

                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction("");
                cmd.Transaction = transaction;

                try
                {
                    foreach (var commandString in CoversToInsertQueries)
                    {
                        cmd.CommandText = commandString;
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var commandString in CoversToUpdateQueries)
                    {
                        cmd.CommandText = commandString;
                        cmd.ExecuteNonQuery();
                    }

                    if (!String.IsNullOrEmpty(CoversToDeleteQuery))
                    {
                        cmd.CommandText = CoversToDeleteQuery;
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    throw;
                }
                finally
                {
                    connection.Close();
                }

                return false;
            }
        }
    }
}

