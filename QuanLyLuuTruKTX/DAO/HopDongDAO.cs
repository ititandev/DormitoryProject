﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class HopDongDAO
    {
        public static DataTable ViewAll()
        {
            return Data.ExecuteQuery(@"SELECT [MaSo]
                                          ,[MSSV]
                                          ,[MaNhanVien]
                                          ,[NgayLap]
                                          ,[NgayBatDau]
                                          ,[NgayKetThuc]
                                          , case [TinhTrang]
	                                      WHEN 0 THEN N'Chưa tới thời hạn'
	                                      WHEN 1 THEN N'Trong thời hạn'
	                                      WHEN 2 THEN N'Hết thời hạn'
	                                      END AS [ThoiHan]
                                          ,[IDPhong]
                                          ,[GiaTienTongCong]
                                          ,[GiaTienDaNop]
                                    FROM [dbo].[HopDong]");
        }

        public static HopDongDTO GetHopDongDTO(string MaSo)
        {
            SqlDataReader reader = Data.ExecuteReader("SELECT * FROM HopDong WHERE MaSo = @MaSo",new string[]{ MaSo});
            if (reader.Read())
            {
                HopDongDTO hopDongDTO = new HopDongDTO();

                hopDongDTO.MaSo = Convert.ToInt32(reader["MaSo"]);
                hopDongDTO.MSSV = Convert.ToString(reader["MSSV"]);
                hopDongDTO.MaNhanVien = Convert.ToString(reader["MaNhanVien"]);
                hopDongDTO.NgayLap = Convert.ToDateTime(reader["NgayLap"]);
                hopDongDTO.NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]);
                hopDongDTO.NgayKetThuc = Convert.ToDateTime(reader["NgayKetThuc"]);
                hopDongDTO.TinhTrang = HopDongDTO.TinhTrangHopDongString[Convert.ToSByte(reader["TinhTrang"])];
                hopDongDTO.IDPhong = Convert.ToString(reader["IDPhong"]);
                hopDongDTO.GiaTienTongCong = Convert.ToInt32(reader["GiaTienTongCong"]);
                hopDongDTO.GiaTienDaNop = Convert.ToInt32(reader["GiaTienDaNop"]);
                hopDongDTO.ChuThich = Convert.ToString(reader["ChuThich"]);
                return hopDongDTO;
            }
            else
                return null;
        }

        public static bool AddHopDongDTO(HopDongDTO hopDongDTO)
        {
            string[] param = new string[] { "@MSSV", "@MaNhanVien", "@NgayLap", "@NgayBatDau", "@NgayKetThuc", "@IDPhong", "@GiaTienTongCong", "@GiaTienDaNop", "@ChuThich" };
            object[] value = new object[] { hopDongDTO.MSSV, hopDongDTO.MaNhanVien, hopDongDTO.NgayLap.ToString("yyyy-MM-dd"),
                hopDongDTO.NgayBatDau.ToString("yyyy-MM-dd"), hopDongDTO.NgayKetThuc.ToString("yyyy-MM-dd"),
                hopDongDTO.IDPhong, hopDongDTO.GiaTienTongCong, hopDongDTO.GiaTienDaNop, hopDongDTO.ChuThich };
            //string[] value = new string[] { "32233", "54534", "2017-03-01", "2017-03-01", "2017-03-01", "0", "52318", "fofd3", "4324", "3432", "343432" };
            //int result =  Data.ExecuteNonQuery("INSERT INTO HopDong (MSSV, MaNhanVien, NgayLap, NgayBatDau, NgayKetThuc, TinhTrang, RFID, IDPhong, GiaTienTongCong, GiaTienDaNop, ChuThich) VALUES ( N@MSSV , N@MaNhanVien , @NgayLap , @NgayBatDau , @NgayKetThuc , @TinhTrang , N@RFID , N@IDPhong , @GiaTienTongCong , @GiaTienDaNop , @ChuThich )", param, value);
            SqlCommand sqlCommand = new SqlCommand(
                @"INSERT INTO [dbo].[HopDong]
                   ([MSSV]
                   ,[MaNhanVien]
                   ,[NgayLap]
                   ,[NgayBatDau]
                   ,[NgayKetThuc]
                   ,[TinhTrang]
                   ,[IDPhong]
                   ,[GiaTienTongCong]
                   ,[GiaTienDaNop]
                   ,[ChuThich])
                VALUES
                    ( @MSSV , @MaNhanVien , @NgayLap , @NgayBatDau , @NgayKetThuc , @TrangThai, @IDPhong , @GiaTienTongCong , @GiaTienDaNop , @ChuThich )");
            sqlCommand.Parameters.Add("@MSSV", SqlDbType.NVarChar).Value = hopDongDTO.MSSV;
            sqlCommand.Parameters.Add("@MaNhanVien", SqlDbType.NVarChar).Value = hopDongDTO.MaNhanVien;
            sqlCommand.Parameters.Add("@NgayLap", SqlDbType.Date).Value = hopDongDTO.NgayLap;
            sqlCommand.Parameters.Add("@NgayBatDau", SqlDbType.Date).Value = hopDongDTO.NgayBatDau;
            sqlCommand.Parameters.Add("@NgayKetThuc", SqlDbType.Date).Value = hopDongDTO.NgayKetThuc;
            sqlCommand.Parameters.Add("@TrangThai", SqlDbType.Int).Value = 0;
            sqlCommand.Parameters.Add("@IDPhong", SqlDbType.NVarChar).Value = hopDongDTO.IDPhong;
            sqlCommand.Parameters.Add("@GiaTienTongCong", SqlDbType.Int).Value = hopDongDTO.GiaTienTongCong;
            sqlCommand.Parameters.Add("@GiaTienDaNop", SqlDbType.Int).Value = hopDongDTO.GiaTienDaNop;
            sqlCommand.Parameters.Add("@ChuThich", SqlDbType.NVarChar).Value = hopDongDTO.ChuThich;
            int result = Data.ExecuteNonQueryParam(sqlCommand);
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
