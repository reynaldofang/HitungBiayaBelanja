using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pembelanjaan
{
    public class BarangDAOList
    {
        //public List<Barang> listData = new List<Barang>();
        Barang barang = new Barang();
        public List<Barang> listBarang = new List<Barang>();

        public bool TambahData(Barang barang)
        {
            bool result = false;
            try
            {
                if (Convert.ToDecimal(barang.Harga) > 0 && Convert.ToInt32(barang.Pajak) >= 0 && barang.Quantity > 0)
                {
                    listBarang.Add(barang);
                    result = true;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
            return result;
        }



        public bool DeleteData(Barang barang)
        {
            bool result = false;
            try
            {
                if (ItemIsExist(barang))
                {
                    Barang barangToDelete = null;
                    for (int i = 0; i < listBarang.Count; i++)
                    {
                        barangToDelete = listBarang[i];
                        if (barangToDelete.Nama.ToLower().Trim().Equals(barang.Nama.ToLower().Trim()))
                        {
                            break;
                        }
                    }
                    if (barangToDelete != null)
                    {
                        listBarang.Remove(barangToDelete);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdateData(Barang lama, Barang baru)
        {
            bool result = false;
            try
            {
                if (Convert.ToDecimal(baru.Harga) > 0 && Convert.ToInt32(baru.Pajak) >= 0 && baru.Quantity > 0)
                {
                    if (ItemIsExist(lama))
                    {
                        for (int i = 0; i < listBarang.Count; i++)
                        {
                            Barang item = listBarang[i];
                            if (item.Nama.ToLower().Trim().Equals(lama.Nama.ToLower().Trim()))
                            {
                                listBarang[i] = baru;
                                result = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException();
            }
            return result;
        }

        public bool ItemIsExist(Barang barang)
        {
            if (listBarang?.Count > 0)
            {
                foreach (Barang item in listBarang)
                {
                    if (item.Nama.ToLower().Trim().Equals(barang.Nama.ToLower().Trim()))
                        return true;
                }
            }
            return false;
        }

    }
}
