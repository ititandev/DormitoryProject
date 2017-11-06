﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace DAO
{
    public static class Data
    {
        /// <summary>
        /// Thực thi query và lấy dữ liệu trả về dạng DataTable
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string query, object[] param = null)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectString);
                SqlCommand sqlCommand = new SqlCommand(query, con);
                if (param != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> listParam = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@')
                            listParam.Add(item);
                    }
                    for (int i = 0; i < param.Length; i++)
                    {
                        sqlCommand.Parameters.AddWithValue(listParam[i], param[i]);
                    }
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// Thực thi query mà không lấy dữ liệu trả về. 
        /// Hàm trả về số hàng bị thay đổi trong cơ sở dữ liệu
        /// </summary>
        /// <param name="query"></param>
        public static int ExecuteNonQuery(string query, object[] param = null)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectString);
                con.Open();
                SqlCommand sqlCommand = new SqlCommand(query, con);
                if (param != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> listParam = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@')
                            listParam.Add(item);
                    }
                    for (int i = 0; i < param.Length; i++)
                    {
                        sqlCommand.Parameters.AddWithValue(listParam[i], param[i]);
                    }
                }
                int rowAffected = sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                con.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

        }
        /// <summary>
        /// Thực thi query chứa trong SqlCommand, kèm tham số đi kèm trong SqlCommand.
        /// Hàm trả về số hàng bị thay đổi trong cơ sở dữ liệu
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryParam(SqlCommand sqlCommand)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectString);
                con.Open();
                sqlCommand.Connection = con;
                int rowAffected = sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                con.Close();
                return rowAffected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

        }
        /// <summary>
        /// Thực thi query và lấy dữ liệu trả về dạng SqlDataReader
        /// Sử dụng phương thức Read() khi sử dụng
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(String query, object[] param = null)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConnectString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                if (param != null)
                {
                    string[] temp = query.Split(' ');
                    List<string> listParam = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@')
                            listParam.Add(item);
                    }
                    for (int i = 0; i < param.Length; i++)
                    {
                        sqlCommand.Parameters.AddWithValue(listParam[i], param[i]);
                    }
                }
                SqlDataReader reader = sqlCommand.ExecuteReader();
                return reader;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Get, Set ConnectString
        /// </summary>
        public static string ConnectString
        {
            get
            {
                return DAO.Properties.Settings.Default.connectString;
            }
            set
            {
                DAO.Properties.Settings.Default.connectString = value;
                DAO.Properties.Settings.Default.Save();
            }
        }
    }

}
