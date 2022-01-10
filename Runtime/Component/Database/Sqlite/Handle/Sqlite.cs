using Cat.Core;
using Cat.Extend;
using Cat.Logger;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace Cat.Data
{
    public static class ConstSqlite
    {
        public const int CONFIGER = 0;
        public const int RUNTIME = 1;
    }
    /// <summary>
    /// Sqlite数据库(默认单(unity)链接)
    /// </summary>
    public class Sqlite : IDisposable
    {
        public ConnectionState State => sqliteApiCon.State;
        private static readonly string[] paths;
        private SqliteConnection sqliteApiCon;
        private SqliteTransaction trs;//单对象只开一个
        static Sqlite()
        {
            var settingPaths = CatSeting.Instance.SqlitePaths;
            paths = new string[settingPaths.Length];
            for (var i = 0; i < paths.Length; i++)
            {
                paths[i] = "Data Source = " + Application.streamingAssetsPath + "/" + settingPaths[i] + ";";
            }
            //Debug.Log("Sqlite try inited, DB path = " + _path);
        }
        public Sqlite(int sqliteIndex = ConstSqlite.CONFIGER)
        {
            try
            {
                //Debug.Log("Sqlite try open");
                string connectionString = paths[sqliteIndex];
                sqliteApiCon = new SqliteConnection(connectionString);
                sqliteApiCon.Open();
                //Debug.Log("Sqlite open Successful!");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Sqlite is error ! : " + e.ToString());
                throw;
            }
        }
        //public void Close()
        //{
        //    if (sqliteApiCon.State == ConnectionState.Open)
        //        sqliteApiCon.Close();
        //    //Debug.Log("Sqlite Disposed manual");
        //}
        public void Dispose()
        {
            try
            {
                if (sqliteApiCon != null)
                {
                    if (sqliteApiCon.State == ConnectionState.Open)
                        sqliteApiCon.Close();
                    if (trs != null)
                        trs.Dispose();
                    sqliteApiCon.Dispose();
                    sqliteApiCon = null;
                    trs = null;
                    //Debug.Log("Sqlite Disposed");
                }
            }
            catch (Exception e)
            {
                Debug.LogError("释放Sqlite时出错" + e);
            }
            
        }
        public DataTable Query(string sqlString)
        {
            try
            {
                Check(sqlString);
                SqliteCommand cmd = new SqliteCommand(sqlString, sqliteApiCon);
                SqliteDataReader reader = cmd.ExecuteReader();
                return reader.GetTable();
            }
            catch (SqliteException e)
            {
                Debug.LogError("sqlite query error!sql:[" + sqlString + "]" + e.ToString());
            }
            return null;
        }
        public async Task<DataTable> QueryAsync(string sqlString)
        {
            try
            {
                Check(sqlString);
                SqliteCommand cmd = new SqliteCommand(sqlString, sqliteApiCon);
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                return reader.GetTable();
            }
            catch (SqliteException e)
            {
                Debug.LogError("sqlite query error!sql:[" + sqlString + "]" + e.ToString());
            }
            return null;
        }
        public int ExecuteNonQuery(string sqlString)
        {
            try
            {
                Check(sqlString);
                SqliteCommand cmd = new SqliteCommand(sqlString, sqliteApiCon);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.LogError("sqlite execute error!sql:[" + sqlString + "]" + e.ToString());
            }
            return -1;
        }
        public async Task<int> ExecuteNonQueryAsync(string sqlString)
        {
            try
            {
                Check(sqlString);
                var cmd = new SqliteCommand(sqlString, sqliteApiCon);
                return await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Debug.LogError("sqlite execute error!sql:[" + sqlString + "]" + e.ToString());
            }
            return -1;
        }
        public void BeginTrans()
        {
            trs = sqliteApiCon.BeginTransaction();//在多线程连接池锁处理之前，单一事务处理
        }
        public void CommitTrans()
        {
            trs.Commit();
        }
        private void Check(string sqlString)
        {
            if (sqliteApiCon.State != ConnectionState.Open)
            {
                throw new Exception("确保连接打开后重试");
            }
            if (string.IsNullOrEmpty(sqlString))
            {
                throw new Exception("转入的Sql字符串无效");
            }
        }
    }
}

