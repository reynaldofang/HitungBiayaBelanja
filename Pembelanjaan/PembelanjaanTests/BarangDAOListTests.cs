using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pembelanjaan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pembelanjaan.Tests
{
    [TestClass()]
    public class BarangDAOListTests
    {
        BarangDAOList daoBarang;
        List<Barang> listData = new List<Barang>();

        public BarangDAOListTests()
        {
            daoBarang = new BarangDAOList();
        }

        //Membuat testing untuk tambah data dengan cara menghitung jumlah data sebelum dan sesudah penambahan
        [TestMethod()]
        public void TambahDataTest()
        {
            listData.Add(new Barang
            {
                Kode = "0001",
                Nama = "Buku",
                Quantity = 10,
                Harga = 12000,
                Pajak = "10"
            });
            daoBarang.TambahData(new Barang
            {
                Kode = "0001",
                Nama = "Buku",
                Quantity = 10,
                Harga = 12000,
                Pajak = "10"
            });
            Assert.AreEqual(listData.Count, daoBarang.listBarang.Count);
        }

        //Membuat testing untuk menambah data barang dengan angka yang minus
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TambahDataTestMinus()
        {
            daoBarang.TambahData(new Barang
            {
                Kode = "0002",
                Nama = "Pensil",
                Quantity = -5,
                Harga = 5000,
                Pajak = "2"
            });
        }

        //Testing untuk mengedit data pada nama barang
        [TestMethod()]
        public void UpdateDataTest()
        {
            Barang lama = new Barang
            {
                Kode = "0003",
                Nama = "Novel",
                Quantity = 5,
                Harga = 50000,
                Pajak = "5"
            };
            daoBarang.listBarang.Add(lama);

            Barang baru = new Barang
            {
                Kode = "0003",
                Nama = "Kamus",
                Quantity = 5,
                Harga = 50000,
                Pajak = "5"
            };
            daoBarang.UpdateData(lama, baru);

            listData.Add(new Barang
            {
                Kode = "0003",
                Nama = "Kamus",
                Quantity = 5,
                Harga = 50000,
                Pajak = "5"
            });
            Assert.AreEqual(daoBarang.listBarang.Count, listData.Count);
        }

        //Testing untuk mengedit data barang dengan quantity minus
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateDataTestMinus()
        {
            Barang lama = new Barang
            {
                Kode = "0003",
                Nama = "Novel",
                Quantity = 5,
                Harga = 50000,
                Pajak = "5"
            };
            daoBarang.listBarang.Add(lama);

            Barang baru = new Barang
            {
                Kode = "0003",
                Nama = "Novel",
                Quantity = -5,
                Harga = 50000,
                Pajak = "5"
            };
            daoBarang.UpdateData(lama, baru);
        }

        //Testing untuk menghapus data dengan menghitung jumlah data sebelum dan sesudah penghapusan
        [TestMethod()]
        public void DeleteDataTest()
        {
            Barang lama = new Barang
            {
                Kode = "0003",
                Nama = "Novel",
                Quantity = 5,
                Harga = 50000,
                Pajak = "5"
            };
            daoBarang.listBarang.Add(lama);
            daoBarang.DeleteData(lama);

            List<Barang> barang = new List<Barang>();
            Assert.AreEqual(barang.Count, daoBarang.listBarang.Count);
        }
    }
}