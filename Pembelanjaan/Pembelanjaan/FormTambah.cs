using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pembelanjaan
{
    public partial class FormTambah : Form
    {
        bool _result = false;

        public FormTambah()
        {
            InitializeComponent();
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            if (this.txtkode.Text.Trim() == "" || txtnama.Text.Trim() == "" || txtjumlah.Text.Trim() == "" || txtharga.Text.Trim() == "" || txtpajak.Text.Trim() == "")
            {
                MessageBox.Show("Sorry, isi semua terlebih dahulu ...", "Kosong" , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtkode.Text.Length > 4 )
            {
                MessageBox.Show("Sorry, Kode Barang tidak boleh melebihi 4 karakter ...", "Kode Barang", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txtnama.Text.Length > 50)
            {
                MessageBox.Show("Sorry, Nama tidak boleh lewat 50 karakter ...", "Nama Terlalu Panjang", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Decimal.Parse(txtpajak.Text) > 100)
            {
                MessageBox.Show("Sorry, Persen tidak boleh melebihi 100% ...", "Pajak Melebihan Batas!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                try
                {
                    using (var dao = new BarangDAO())
                    {
                        _result = dao.Insert(
                            new Barang
                            {
                                Kode = this.txtkode.Text.Trim(),
                                Nama = this.txtnama.Text.Trim(),
                                Harga = Convert.ToDecimal(this.txtharga.Text.Trim()),
                                Quantity = Convert.ToInt32(this.txtjumlah.Text.Trim()),                             
                                Pajak = (Convert.ToDouble(this.txtpajak.Text.Trim().Split('%')[0]) / 100).ToString()
                            }) > 0;
                    }
                    MessageBox.Show("Success Tambah Barang!", this.Text, MessageBoxButtons.OK , MessageBoxIcon.Information);
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
