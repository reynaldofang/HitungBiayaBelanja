using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Pembelanjaan
{
    public class BarangDAO : IDisposable
    {
        SqlConnection _conn = null;
        

        public BarangDAO()
        {
            try
            {
                _conn = new SqlConnection(Setting.GetConnectionString());
                _conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Barang> GetAllDataBarang()
        {
            List<Barang> listData = new List<Barang>();

            try
            {
                string sqlstring = @"select * from Barang";

                SqlCommand cmd = new SqlCommand(sqlstring, _conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                
                    while (reader.Read())
                    {
                        string kode = reader["Kode"].ToString();
                        string nama = reader["Nama"].ToString();
                        decimal harga = Decimal.Parse(reader["Harga"].ToString());
                        int quantity = int.Parse(reader["Quantity"].ToString());
                        string pjk = reader["Pajak"].ToString();

                        listData.Add(new Barang
                        {
                            Kode = kode,
                            Nama = nama,
                            Quantity = quantity,
                            Harga = harga,
                            Pajak = pjk
                        });

                    }
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return listData;
        }

        public int Insert(Barang barang)
        {
            int result = 0;
            try
            {
                string sqlString = @"insert into barang values (@kode, @nama, @harga , @quantity, @pajak)";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = sqlString;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@kode", barang.Kode);
                    cmd.Parameters.AddWithValue("@nama", barang.Nama);                   
                    cmd.Parameters.AddWithValue("@harga", barang.Harga);
                    cmd.Parameters.AddWithValue("@quantity", barang.Quantity);
                    cmd.Parameters.AddWithValue("@pajak", barang.Pajak);
                    result = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int Update(Barang barang)
        {
            int result = 0;
            SqlTransaction trans = null;
            try
            {
                trans = _conn.BeginTransaction();
                string sqlString =
                    @"update barang set kode = kode, nama = @nama, harga = @harga, quantity = @quantity , pajak = @pajak where kode = @kode";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = sqlString;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@kode", barang.Kode);
                    cmd.Parameters.AddWithValue("@nama", barang.Nama);
                    cmd.Parameters.AddWithValue("@harga", barang.Harga);
                    cmd.Parameters.AddWithValue("@quantity", barang.Quantity);
                    cmd.Parameters.AddWithValue("@pajak", barang.Pajak);
                    result = cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw ex;
            }
            finally
            {
                if (trans != null) trans.Dispose();
            }
            return result;
        }

        public int Delete(string kode)
        {
            int result = 0;
            try
            {
                string sqlString =
                    @"delete barang where kode = @kode";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = sqlString;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@kode", kode);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<Barang> QueryData(Barang barang)
        {
            List<Barang> listData = null;
            try
            {
                string sqlString = "select * from barang where kode like @kode, " +
                    "nama like @nama, harga like @harga," +
                    "quantity like @quantity";

                using (SqlCommand cmd = new SqlCommand(sqlString, _conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@kode", $"%{barang.Kode}%");
                    cmd.Parameters.AddWithValue("@nama", $"%{barang.Nama}%");                   
                    cmd.Parameters.AddWithValue("@harga", $"%{barang.Harga}%");
                    cmd.Parameters.AddWithValue("@quantity", $"%{barang.Quantity}%");
                    cmd.Parameters.AddWithValue("@pajak", $"%{barang.Pajak}%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listData = new List<Barang>();
                            while (reader.Read())
                            {
                                listData.Add(
                                    new Barang
                                    {
                                        Kode = reader["No"].ToString(),
                                        Nama = reader["nama"].ToString(),                                       
                                        Harga = Convert.ToDecimal(reader["harga"]),
                                        Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                                        Pajak = reader["pajak"].ToString()
                                    });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listData;
        }

        public Barang GetDataBarangByKode(string kode)
        {
            Barang barang = null;
            try
            {
                string sqlString = @"select * from barang where kode = @kode";
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@kode", kode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                     //   if (reader.HasRows)
                     //   {
                            if (reader.Read())
                            {
                                barang = new Barang
                                {
                                    Kode = reader["No"].ToString(),
                                    Nama = reader["nama"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                                    Harga = Convert.ToDecimal(reader["harga"].ToString()),
                                    Pajak = reader["pajak"].ToString(),
                                };
                     //  }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return barang;
        }

        public void Dispose()
        {
            if (_conn != null) _conn.Close();
        }
    }

}

