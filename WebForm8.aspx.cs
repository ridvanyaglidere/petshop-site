using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace petshopint2
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";
        protected Label lblMesaj;
        protected Repeater rptUrunler;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UrunleriListele();
            }
        }

        private void UrunleriListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT u.UrunID, u.UrunAdi, u.Fiyat, u.ResimYolu, u.Aciklama, u.Stok, k.KategoriAdi, m.MarkaAdi, u.UrunTuru " +
                                   "FROM urunler u " +
                                   "INNER JOIN kategoriler k ON u.KategoriID = k.KategoriID " +
                                   "INNER JOIN markalar m ON u.MarkaID = m.MarkaID";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        rptUrunler.DataSource = dt;
                        rptUrunler.DataBind();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Ürünler yüklenirken hata: " + ex.Message;
            }
        }

        protected void btnDetay_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string urunID = btn.CommandArgument;
            Response.Redirect($"WebForm10.aspx?UrunID={urunID}");
        }
    }
}