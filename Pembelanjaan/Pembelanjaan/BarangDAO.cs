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

        public BarangDAO(string connectionString)
        {
            try
            {
                _conn = new SqlConnection(connectionString);
                _conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Barang> GetAllDataBarang()
        {
            List<Barang> listData = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = @"select * from barang order by no";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            listData = new List<Barang>();
                            while (reader.Read())
                            {
                                listData.Add(new Barang
                                {
                                    No = reader["No"].ToString(),
                                    Nama = reader["Nama"].ToString(),
                                    Harga = Convert.ToDecimal(reader["Harga"].ToString()),
                                    Pajak = reader["Pajak"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listData;
        }

        public void Dispose()
        {
            if (_conn != null) _conn.Close();
        }
    }

}
