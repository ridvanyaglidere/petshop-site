using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace petshopint2
{
    public partial class Hakkimizda : Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadHakkimizdaContent();
            }
        }

        private void LoadHakkimizdaContent()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Bolum, Icerik FROM Hakkimizda";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string bolum = reader["Bolum"].ToString();
                                string icerik = reader["Icerik"].ToString();
                                switch (bolum)
                                {
                                    case "Giris":
                                        litGiris.Text = icerik;
                                        break;
                                    case "BizKimiz":
                                        litBizKimiz.Text = icerik;
                                        break;
                                    case "Misyon":
                                        litMisyon.Text = icerik;
                                        break;
                                    case "Hizmetler":
                                        litHizmetler.Text = icerik;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                litError.Text = $"Hakkımızda yüklenirken hata: {ex.Message}";
            }
        }
    }
}