using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;


namespace MVCBase.Common
{
    public class Database
    {
        private string sConnectionString = "";
        private SqlConnection Conn;

        public Database() { }

        public Database(string strcon)
        {
            sConnectionString = strcon;
        }

        public string StrCon
        {
            get
            {
                return sConnectionString;
            }
            set
            {
                sConnectionString = value;
            }
        }

        public void Conncetion()
        {
            if (sConnectionString == "" || sConnectionString == null)
            {
                sConnectionString = "";
            }
        }
        private void Open()
        {
            Conncetion();
            Conn = new SqlConnection(sConnectionString);

            if (Conn.State == ConnectionState.Closed)
                Conn.Open();

        }

        public void Close()
        {
            if (Conn != null)
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                Conn.Dispose();
                Conn = null;
            }
        }

        private void Dispose()
        {
            if (Conn != null)
                Conn.Dispose();
            Conn = null;
        }

        public SqlDataReader ExecuteReader(string cmdText, CommandType cmdType)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Conn;
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            try
            {

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            catch
            {
                this.Close();
                return null;
            }
        }

        public SqlDataReader ExecuteReader(string cmdText, CommandType cmdType, params SqlParameter[] CmdParams)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Conn;
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (CmdParams != null)
            {
                foreach (SqlParameter Param in CmdParams)
                {
                    cmd.Parameters.Add(Param);
                }
            }

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();

                return reader;
            }

            catch
            {
                this.Close();
                return null;
            }
        }

        public DataSet GetDataSet(string cmdText, CommandType cmdType)
        {
            Conncetion();
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection Conn = new SqlConnection(sConnectionString))//调用完直接释放资源 适用与非连接环境
            {
                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;

                SqlDataAdapter ada = new SqlDataAdapter();
                ada.SelectCommand = cmd;

                DataSet ds = new DataSet();
                try
                {
                    ada.Fill(ds);
                }
                catch (SqlException ex)
                {
                }

                return ds;
            }
        }

        public DataSet GetDataSet(string cmdText, CommandType cmdType, SqlParameter[] adaParams)
        {
            Conncetion();
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection Conn = new SqlConnection(sConnectionString))
            {
                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;

                SqlDataAdapter ada = new SqlDataAdapter();

                if (adaParams != null)
                {
                    foreach (SqlParameter Param in adaParams)
                    {
                        cmd.Parameters.Add(Param);
                    }
                }

                ada.SelectCommand = cmd;

                DataSet ds = new DataSet();
                try
                {
                    ada.Fill(ds);
                }
                catch (SqlException ex)
                {
                }


                cmd.Parameters.Clear();

                return ds;
            }
        }

        public DataTable GetDataTable(string cmdText)
        {
            DataTable tempdt = new DataTable();
            try
            {
                tempdt = GetDataSet(cmdText, CommandType.Text).Tables[0];
            }
            catch (Exception)
            {
            }

            return tempdt;
        }

        public DataTable GetDataTable(string cmdText, CommandType cmdType)
        {
            DataTable tempdt = new DataTable();
            try
            {
                tempdt = GetDataSet(cmdText, cmdType).Tables[0];
            }
            catch (Exception)
            {
            }

            return tempdt;
        }

        public DataTable GetDataTable(string cmdText, CommandType cmdType, SqlParameter[] adaParams)
        {
            return GetDataSet(cmdText, cmdType, adaParams).Tables[0];
        }

        public int ExecuteNonQuery(string cmdText, CommandType cmdType)
        {
            int i;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;

                i = cmd.ExecuteNonQuery();
            }

            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.Close();
            }

            return i;

        }

        public int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            int i;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;

                foreach (SqlParameter Param in cmdParams)
                {
                    cmd.Parameters.Add(Param);
                }


                i = cmd.ExecuteNonQuery();
            }

            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.Close();
            }

            return i;
        }

        public int ExecuteReturnValue(string cmdText, CommandType cmdType)
        {
            SqlCommand cmd;
            try
            {
                this.Open();
                cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;

                cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));

                cmd.ExecuteNonQuery();
            }

            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.Close();
            }

            return (int)cmd.Parameters["ReturnValue"].Value;
        }

        public int ExecuteReturnValue(string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            SqlCommand cmd;
            try
            {
                this.Open();
                cmd = new SqlCommand();

                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;

                foreach (SqlParameter Param in cmdParams)
                {
                    cmd.Parameters.Add(Param);
                }

                cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                    ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));

                cmd.ExecuteNonQuery();
            }

            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.Close();
            }

            return (int)cmd.Parameters["ReturnValue"].Value;
        }

        public object ExecuteScalar(string cmdText)
        {
            object o;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdText;
                o = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.Close();
            }
            return o;
        }

        public object ExecuteScalar(string cmdText, CommandType cmdType)
        {
            object o;
            try
            {
                this.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                o = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                this.Close();
            }
            return o;
        }

        public object ExecuteScalar(string cmdText, CommandType cmdType, SqlParameter[] cmdParams)
        {
            object o;
            SqlCommand cmd = new SqlCommand();
            try
            {
                this.Open();
                cmd.Connection = Conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                foreach (SqlParameter Param in cmdParams)
                {
                    cmd.Parameters.Add(Param);
                }
                o = cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                cmd.Parameters.Clear();
                this.Close();
            }
            return o;
        }

        //------------------------------------------------------------------------------------


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns>是否执行成功</returns>
        public bool RunProc(string procName)
        {
            try
            {
                SqlCommand cmd = CreateCommand(procName, null, CommandType.StoredProcedure);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["ReturnValue"].Value) == 0 ? true : false;
            }
            catch { return false; }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">执行该存储过程所需要的 sql 参数数组 SqlParameter[]</param>
        /// <returns>是否执行成功</returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = CreateCommand(procName, prams, CommandType.StoredProcedure);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["ReturnValue"].Value);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return 0;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="dataReader">SqlDataReader 用来接受存储过程索取的数据集</param>
        /// <returns>是否执行成功</returns>
        public bool RunProc(string procName, out SqlDataReader dataReader)
        {
            try
            {
                SqlCommand cmd = CreateCommand(procName, null, CommandType.StoredProcedure);
                dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return true;
            }
            catch
            {
                dataReader = null;
                return false;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">执行该存储过程所需要的 sql 参数数组 SqlParameter[]</param>
        /// <param name="dataReader">SqlDataReader 用来接受存储过程索取的数据集</param>
        /// <returns>是否执行成功</returns>
        public bool RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            try
            {
                SqlCommand cmd = CreateCommand(procName, prams, CommandType.StoredProcedure);
                dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return true;
            }
            catch { dataReader = null; return false; }
            finally
            {
                Close();
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>	
        /// <param name="dataset">DataSet 用来接受存储过程索取的数据集</param>
        /// <returns>是否执行成功</returns>
        public bool RunProc(string procName, out DataSet dataset)
        {
            try
            {
                SqlCommand cmd = CreateCommand(procName, null, CommandType.StoredProcedure);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dataset = new DataSet();
                da.Fill(dataset);
                return true;
            }
            catch { dataset = null; return false; }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">执行该存储过程所需要的 sql 参数数组 SqlParameter[]</param>
        /// <param name="dataset">DataSet 用来接受存储过程索取的数据集</param>
        /// <returns>是否执行成功</returns>
        public bool RunProc(string procName, SqlParameter[] prams, out DataSet dataset)
        {
            try
            {
                SqlCommand cmd = CreateCommand(procName, prams, CommandType.StoredProcedure);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dataset = new DataSet();

                da.Fill(dataset);
                return true;
            }
            catch { dataset = null; return false; }
            finally
            {
                Close();
            }
        }


        /// <summary>
        /// 为存储过程创建 SqlCommand 对象
        /// </summary>
        /// <param name="procName_sSQL">要创建 SqlCommand 对象的存储过程名---///---或者是 普通的SQL 语句(含sql参数)</param>
        /// <param name="prams">存储过程所需的 sql 参数数组 SqlParameter[]</param>
        /// <returns>创建 SqlCommand 对象</returns>
        private SqlCommand CreateCommand(string procName_sSQL, SqlParameter[] prams, CommandType cmdType)
        {
            // 确认已经打开 数据连接对象 SqlConnection
            Open();

            SqlCommand cmd = new SqlCommand(procName_sSQL, Conn);
            cmd.CommandType = cmdType;

            // 向SqlCommand 对象 添加sql 参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }

            if (cmdType == CommandType.StoredProcedure)
            {
                // 向SqlCommand 对象 添加返回值 sql 参数 ---- ReturnValue
                cmd.Parameters.Add(
                    new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                    ParameterDirection.ReturnValue, false, 0, 0,
                    string.Empty, DataRowVersion.Default, null));
            }
            return cmd;
        }

        /// <summary>
        /// 执行存储过程(支持 事务 SqlTransaction)
        /// </summary>
        /// <param name="transaction">SqlTransaction 事务对象</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="prams">执行该存储过程所需要的 sql 参数数组 SqlParameter[]</param>
        /// <returns></returns>
        public bool RunProc(SqlTransaction transaction, string procName, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = CreateCommand(transaction, procName, prams, CommandType.StoredProcedure);
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(cmd.Parameters["ReturnValue"].Value) == 0 ? true : false;
            }
            catch { return false; }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 为存储过程创建 SqlCommand 对象 (支持 事务 SqlTransaction)
        /// </summary>
        /// <param name="transaction">SqlTransaction 事务对象</param>
        /// <param name="procName_sSQL">存储过程名---///---或者是 普通的SQL 语句(含sql参数)</param>
        /// <param name="prams">存储过程所需要的 sql 参数数组 SqlParameter[]</param>
        /// <returns></returns>
        private SqlCommand CreateCommand(SqlTransaction transaction, string procName_sSQL, SqlParameter[] prams, CommandType cmdType)
        {
            // 确认已经打开 数据连接对象 SqlConnection
            if (transaction.Connection.State != ConnectionState.Open)
            {
                transaction.Connection.ConnectionString = StrCon.ToString();
                transaction.Connection.Open();
            }

            SqlCommand cmd = new SqlCommand(procName_sSQL, transaction.Connection, transaction);
            cmd.CommandType = cmdType;

            //-- 向SqlCommand 对象 添加sql 参数
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }

            if (cmdType == CommandType.StoredProcedure)
            {
                // 向SqlCommand 对象 添加返回值 sql 参数 ---- ReturnValue
                cmd.Parameters.Add(
                    new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                    ParameterDirection.ReturnValue, false, 0, 0,
                    string.Empty, DataRowVersion.Default, null));
            }
            return cmd;
        }


        /// <summary>
        /// 创建存储过程/命令文本的 sql 输入参数(SqlParameter)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数数值范围</param>
        /// <param name="Value">参数值</param>
        /// <returns>构造出输入参数</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// 创建存储过程/命令文本的 sql 输出参数(SqlParameter)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数数值范围</param>
        /// <returns>构造出输出参数</returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 创建存储过程/命令文本的 sql 参数(SqlParameter)
        /// </summary>
        /// <param name="ParamName">参数名</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数数值范围</param>
        /// <param name="Direction">参数操作形式</param>
        /// <param name="Value">参数值</param>
        /// <returns>构造出 SqlParameter</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
                param = new SqlParameter(ParamName, DbType, Size);
            else
                param = new SqlParameter(ParamName, DbType);

            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;

            return param;
        }


        public DataSet ExecuteNonQuery(string SqlStr)
        {
            //sConncetion();
            SqlConnection SqlCon = new SqlConnection(sConnectionString);
            SqlCon.Open();
            SqlCommand SqlCom = new SqlCommand(SqlStr.ToString(), SqlCon);
            SqlCom.CommandType = CommandType.Text;
            //SqlCom.ExecuteNonQuery();
            DataSet ObjDs = new DataSet();
            SqlDataAdapter SqlDa = new SqlDataAdapter(SqlCom);
            SqlDa.Fill(ObjDs);


            SqlCon.Close();
            return ObjDs;
        }

    }
}