using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace petshopint2
{
    public partial class WebForm4 : Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optional: Check if user is logged in
                if (Session["KullaniciID"] == null)
                {
                    lnkCikis.Visible = false; // Hide logout button if not logged in
                }
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
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "Ürünler yüklenirken hata: " + ex.Message;
            }
        }

        protected void btnDetay_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string urunID = btn.CommandArgument;
            Response.Redirect($"WebForm10.aspx?UrunID={urunID}");
        }

        protected void lnkCikis_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("WebForm3.aspx");
        }
    }
}