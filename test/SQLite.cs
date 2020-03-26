using System.Data;
using System.Net.NetworkInformation;
using System;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace test
{
#nullable enable
    /// <summary>
    /// sqlite 操作类
    /// </summary>
    public class SQLite:IDisposable
    {
        /// <summary>
        /// 初始化sqlite连接
        /// </summary>
        /// <param name="_connectionString">连接字符串</param>
        public SQLite(string _connectionString)
        {
            lock (lockThis)
            {
                sqlite = new SQLiteConnection(connectionString = _connectionString);
            }
        }
        SQLiteConnection sqlite;

        private string connectionString;
        private static object lockThis = new object();
        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <value>设置连接字符串的值</value>
        public string ConnectionString
        {
            get => connectionString;
            set
            {
                lock (lockThis)
                {
                    sqlite = new SQLiteConnection(connectionString = value);
                }
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns>成功：true<br/>失败：false</returns>
        public bool Open()
        {
            try
            {
                sqlite.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭sqlite连接
        /// </summary>
        public void Close() => sqlite.Close();

        /// <summary>
        /// 执行sqlcommand语句，返回影响的行数
        /// </summary>
        /// <param name="sqliteCommandString">SQL command语句</param>
        /// <returns> 返回影响的行数</returns>
        public int ExecuteSql(string sqliteCommandString) => (int)ExecuteCommandThenDisposed(x => x.ExecuteNonQuery(), sqliteCommandString);

        /// <summary>
        /// 执行SQL command语句，返回object类型值<br/>
        /// 【使用时请尽量返回struct类型的值】
        /// </summary>
        /// <param name="command">对SQLitecommand对象进行操作的委托</param>
        /// <param name="sqliteCommandString">sqlcommand语句</param>
        /// <returns>返回执行结果</returns>
        public object ExecuteCommandThenDisposed(Func<SQLiteCommand, object> command, string sqliteCommandString)
        {
            using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqliteCommandString, sqlite))
            {
                return command(sqliteCommand);
            }
        }

        /// <summary>
        /// 执行批量的sqlite数据库操作,第二个参数为事务对象
        /// </summary>
        /// <param name="command">执行操作的方法</param>
        /// <param name="transaction">是否开启事务。<br/>[默认null没有可开启的事务]</param>
        public void ExecuteAllCommandThenDisposed(Action<SQLiteCommand> command, SQLiteTransaction? transaction = null)
        {
            try
            {
                using (SQLiteCommand sQLiteCommand = new SQLiteCommand())
                {
                    command?.Invoke(sQLiteCommand);
                }
                transaction?.Commit();
            }
            catch (System.Exception ex)
            {
                transaction?.Rollback();
                throw new Exception("事务提交失败，" + ex.Message);
            }
        }

        SQLiteTransaction? transaction;

        /// <summary>
        /// 开启事务
        /// </summary>
        public SQLiteTransaction BeginTranscation() => transaction = sqlite.BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        public void TranscationCommit() => transaction?.Commit();

        /// <summary>
        /// 获取和操作sqltie数据
        /// </summary>
        /// <param name="OperateDate">操作DataTable的委托</param>
        /// <param name="sqliteCommandString">sqlcommand语句</param>
        /// <returns>DataTable数据</returns>
        public DataTable GetAndOperateSQLite(Action<DataTable> OperateDate, string sqliteCommandString)
        {
            try
            {
                using (SQLiteDataAdapter sqliteData = new SQLiteDataAdapter(sqliteCommandString, sqlite))
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        sqliteData.Fill(dataTable);
                        OperateDate?.Invoke(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("获取和操作sqltie数据失败，" + ex.Message);
            }
        }

        /// <summary>
        /// 释放SQLite所有资源
        /// </summary>
        public void Dispose() => sqlite.Dispose();
    }
}