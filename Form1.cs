using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace mikro
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        DataTable tablo;
        
        public Form1()
        {
            InitializeComponent();
        }
        void VeriIzleme()
            //server ile baglanti sagladik
        {   baglanti = new SqlConnection("SERVER=DESKTOP-8LLIB44\\USERB;Initial Catalog=MikroDB_V15_TEST;Integrated Security =SSPI");
            baglanti.Open();
            //sutun basliklarini select ile from arasinda cagirdik/
            da = new SqlDataAdapter("SELECT sth_RECno,sth_RECid_DBCno, sth_RECid_RECno,sth_special1,sth_special2,sth_special3, sth_evraktip, sth_evrakno_seri, sth_evrakno_sira,sth_satirno FROM STOK_HAREKETLERI", baglanti);    
            tablo = new DataTable();  
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VeriIzleme();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //secili satirdaki degerleri textboxlarin icerisine atar bu sayede guncelleme ve silme islemleri daha rahat halledilir.
                txtGirisAlti.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtGirisDBCno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtGirisRECno.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtGirisSpecial1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtGirisSpecial2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtGirisSpecial3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtGirisEvrakTip.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtGirisEvraknoSeri.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                txtGirisEvraknoSira.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtGirisSatirno.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO STOK_HAREKETLERI (sth_RECid_DBCno, sth_RECid_RECno, sth_special1, sth_special2, sth_special3, sth_evraktip, sth_evrakno_seri, sth_evrakno_sira, sth_satirno) VALUES (@sth_RECid_DBCno, @sth_RECid_RECno, @sth_special1, @sth_special2, @sth_special3, @sth_evraktip, @sth_evrakno_seri, @sth_evrakno_sira, @sth_satirno)";
            
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@sth_RECid_DBCno", txtGirisDBCno.Text);
            komut.Parameters.AddWithValue("@sth_RECid_RECno", txtGirisRECno.Text);
            komut.Parameters.AddWithValue("@sth_special1", txtGirisSpecial1.Text);
            komut.Parameters.AddWithValue("@sth_special2", txtGirisSpecial2.Text);
            komut.Parameters.AddWithValue("@sth_special3", txtGirisSpecial3.Text);
            komut.Parameters.AddWithValue("@sth_evraktip",txtGirisEvrakTip.Text);
            komut.Parameters.AddWithValue("@sth_evrakno_seri", txtGirisEvraknoSeri.Text);
            komut.Parameters.AddWithValue("@sth_evrakno_sira", txtGirisEvraknoSira.Text);
            komut.Parameters.AddWithValue("@sth_satirno", txtGirisSatirno.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriIzleme();
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM STOK_HAREKETLERI where sth_RECno=@sth_RECno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@sth_RECno",Convert.ToInt32(txtGirisAlti.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriIzleme();
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE STOK_HAREKETLERI SET sth_RECid_DBCno=@sth_RECid_DBCno, sth_RECid_RECno=@sth_RECid_RECno, sth_special1=@sth_special1, sth_special2=@sth_special2, sth_special3=@sth_special3, sth_evraktip=@sth_evraktip, sth_evrakno_seri=@sth_evrakno_seri, sth_evrakno_sira=@sth_evrakno_sira, sth_satirno=@sth_satirno  WHERE sth_RECno=@sth_RECno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@sth_RECno", txtGirisAlti.Text);
            komut.Parameters.AddWithValue("@sth_RECid_DBCno", txtGirisDBCno.Text);
            komut.Parameters.AddWithValue("@sth_RECid_RECno", txtGirisRECno.Text);
            komut.Parameters.AddWithValue("@sth_special1", txtGirisSpecial1.Text);
            komut.Parameters.AddWithValue("@sth_special2", txtGirisSpecial2.Text);
            komut.Parameters.AddWithValue("@sth_special3", txtGirisSpecial3.Text);
            komut.Parameters.AddWithValue("@sth_evraktip", txtGirisEvrakTip.Text);
            komut.Parameters.AddWithValue("@sth_evrakno_seri", txtGirisEvraknoSeri.Text);
            komut.Parameters.AddWithValue("@sth_evrakno_sira", txtGirisEvraknoSira.Text);
            komut.Parameters.AddWithValue("@sth_satirno", txtGirisSatirno.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriIzleme();
        }
    }
}
