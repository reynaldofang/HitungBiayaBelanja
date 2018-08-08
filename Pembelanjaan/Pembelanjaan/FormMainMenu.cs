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
    public partial class FormMainMenu : Form
    {
        List<Barang> listBrg = null;
        List<Belanja> listBelanja = null;
        public FormMainMenu()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
        }

        private void QueryData(Barang barang = null)
        {
            
            try
            {
                this.dataGridView1.DataSource = null;
                List<Barang> listData = null;
                using (var daoBarang = new BarangDAO())
                {
                    if (barang == null)
                    {
                        listData = daoBarang.GetAllDataBarang();
                    }
                    else
                    {
                        listData = daoBarang.QueryData(barang);
                    }
                }
                if (listData != null)
                {
                    this.dataGridView1.DataSource = listData;
                    this.dataGridView1.Columns[0].DataPropertyName = nameof(Barang.Kode);
                    this.dataGridView1.Columns[1].DataPropertyName = nameof(Barang.Nama);
                    this.dataGridView1.Columns[2].DataPropertyName = nameof(Barang.Quantity);
                    this.dataGridView1.Columns[3].DataPropertyName = nameof(Barang.Harga);
                    this.dataGridView1.Columns[4].DataPropertyName = nameof(Barang.Pajak);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            
            listBelanja = new List<Belanja>();
            
            try
            {
                using (var barangdao = new BarangDAO())
                {
                    listBrg = barangdao.GetAllDataBarang();
                }

                dataGridView1.DataSource = listBrg;
                dataGridView1.Columns[0].DataPropertyName = "Kode";
                dataGridView1.Columns[1].DataPropertyName = "Nama";
                dataGridView1.Columns[2].DataPropertyName = "Quantity";
                dataGridView1.Columns[3].DataPropertyName = "Harga";
                dataGridView1.Columns[4].DataPropertyName = "Pajak";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnhitung_Click(object sender, EventArgs e)
        {
            FormHitung form = new FormHitung();
            form.Show();
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            
            FormTambah form = new FormTambah();
            form.Show();
        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Apa Anda Yakin Ingin Menghapus Data Tersebut?", "Hapus Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Yes:
                        using (var barangdao = new BarangDAO())
                        {
                            barangdao.Delete(dataGridView1.CurrentCell.Value.ToString());
                        }
                        MessageBox.Show("Barang berhasil dihapus!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FormMainMenu_Load(null, null);
        }
    }
}
