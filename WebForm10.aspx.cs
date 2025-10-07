using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web;

namespace petshopint2
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string urunID = Request.QueryString["UrunID"];
                if (string.IsNullOrEmpty(urunID))
                {
                    lblMesaj.Text = "Hata: Ürün ID'si belirtilmemiş.";
                    return;
                }

                LoadProductDetails(urunID);
                RenkleriDoldur(urunID);
            }
        }

        private void LoadProductDetails(string urunID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT u.UrunID, u.UrunAdi, u.Fiyat, u.ResimYolu, u.Aciklama, u.Stok, k.KategoriAdi, m.MarkaAdi, u.UrunTuru, u.KategoriID, u.MarkaID " +
                                   "FROM urunler u " +
                                   "INNER JOIN kategoriler k ON u.KategoriID = k.KategoriID " +
                                   "INNER JOIN markalar m ON u.MarkaID = m.MarkaID " +
                                   "WHERE u.UrunID = @UrunID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", urunID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblUrunAdi.Text = reader["UrunAdi"].ToString();
                                lblFiyat.Text = Convert.ToDecimal(reader["Fiyat"]).ToString("C");
                                lblAciklama.Text = reader["Aciklama"].ToString();
                                lblStok.Text = reader["Stok"].ToString();
                                lblKategori.Text = reader["KategoriAdi"].ToString();
                                lblMarka.Text = reader["MarkaAdi"].ToString();
                                lblUrunTuru.Text = reader["UrunTuru"].ToString();
                                imgUrun.ImageUrl = reader["ResimYolu"].ToString();
                                btnSepeteEkle.CommandArgument = reader["UrunID"].ToString();
                                ViewState["UrunAdi"] = reader["UrunAdi"].ToString();
                                ViewState["KategoriID"] = reader["KategoriID"].ToString();
                                ViewState["MarkaID"] = reader["MarkaID"].ToString();
                                ViewState["UrunTuru"] = reader["UrunTuru"].ToString();
                            }
                            else
                            {
                                lblMesaj.Text = "Hata: Ürün bulunamadı.";
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Hata: " + ex.Message;
            }
        }

        private void RenkleriDoldur(string urunID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Renk FROM urunler WHERE UrunAdi = (SELECT UrunAdi FROM urunler WHERE UrunID = @UrunID) " +
                                   "AND KategoriID = (SELECT KategoriID FROM urunler WHERE UrunID = @UrunID) " +
                                   "AND MarkaID = (SELECT MarkaID FROM urunler WHERE UrunID = @UrunID) " +
                                   "AND UrunTuru = (SELECT UrunTuru FROM urunler WHERE UrunID = @UrunID) " +
                                   "AND Stok > 0";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", urunID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dtRenkler = new DataTable();
                            dtRenkler.Columns.Add("Renk", typeof(string));
                            dtRenkler.Rows.Add("Renk Seçin");

                            while (reader.Read())
                            {
                                string renk = reader["Renk"].ToString();
                                if (!string.IsNullOrEmpty(renk))
                                {
                                    dtRenkler.Rows.Add(renk);
                                }
                            }

                            ddlRenk.DataSource = dtRenkler;
                            ddlRenk.DataTextField = "Renk";
                            ddlRenk.DataValueField = "Renk";
                            ddlRenk.DataBind();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Renkler yüklenirken hata: {ex.Message}";
            }
        }

        protected void ddlRenk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRenk.SelectedValue == "Renk Seçin")
            {
                lblMesaj.Text = "Lütfen bir renk seçin.";
                return;
            }

            string urunAdi = ViewState["UrunAdi"]?.ToString();
            string kategoriID = ViewState["KategoriID"]?.ToString();
            string markaID = ViewState["MarkaID"]?.ToString();
            string urunTuru = ViewState["UrunTuru"]?.ToString();
            string selectedRenk = ddlRenk.SelectedValue;

            if (string.IsNullOrEmpty(urunAdi) || string.IsNullOrEmpty(kategoriID) || string.IsNullOrEmpty(markaID) || string.IsNullOrEmpty(urunTuru))
            {
                lblMesaj.Text = "Hata: Ürün bilgileri eksik.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT u.UrunID, u.Fiyat, u.ResimYolu, u.Aciklama, u.Stok, k.KategoriAdi, m.MarkaAdi, u.UrunTuru " +
                                   "FROM urunler u " +
                                   "INNER JOIN kategoriler k ON u.KategoriID = k.KategoriID " +
                                   "INNER JOIN markalar m ON u.MarkaID = m.MarkaID " +
                                   "WHERE u.UrunAdi = @UrunAdi AND u.KategoriID = @KategoriID AND u.MarkaID = @MarkaID AND u.UrunTuru = @UrunTuru AND u.Renk = @Renk";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                        cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                        cmd.Parameters.AddWithValue("@MarkaID", markaID);
                        cmd.Parameters.AddWithValue("@UrunTuru", urunTuru);
                        cmd.Parameters.AddWithValue("@Renk", selectedRenk);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblUrunAdi.Text = urunAdi; // Same product name
                                lblFiyat.Text = Convert.ToDecimal(reader["Fiyat"]).ToString("C");
                                lblAciklama.Text = reader["Aciklama"].ToString();
                                lblStok.Text = reader["Stok"].ToString();
                                lblKategori.Text = reader["KategoriAdi"].ToString();
                                lblMarka.Text = reader["MarkaAdi"].ToString();
                                lblUrunTuru.Text = reader["UrunTuru"].ToString();
                                imgUrun.ImageUrl = reader["ResimYolu"].ToString();
                                btnSepeteEkle.CommandArgument = reader["UrunID"].ToString();
                                lblMesaj.Text = "";
                            }
                            else
                            {
                                lblMesaj.Text = $"Seçilen renkte ({selectedRenk}) ürün bulunamadı.";
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Hata: {ex.Message}";
            }
        }

        protected void btnSepeteEkle_Click(object sender, EventArgs e)
        {
            if (ddlRenk.SelectedValue == "Renk Seçin")
            {
                lblMesaj.Text = "Lütfen bir renk seçin.";
                return;
            }

            string urunID = btnSepeteEkle.CommandArgument;
            string selectedRenk = ddlRenk.SelectedValue;

            // Oturum kontrolü
            if (Session["TelefonNo"] == null)
            {
                // Kullanıcı oturum açmamış, ürünü ve renk bilgisini geçici olarak sakla
                Session["PendingUrunID"] = urunID;
                Session["PendingRenk"] = selectedRenk;
                Response.Redirect("WebForm3.aspx?returnUrl=WebForm5.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }

            // Stok kontrolü
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Stok FROM urunler WHERE UrunID = @UrunID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", urunID);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int stock = Convert.ToInt32(result);
                            if (stock <= 0)
                            {
                                lblMesaj.Text = $"Seçilen renkte ({selectedRenk}) ürün stokta yok!";
                                return;
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Hata: {ex.Message}";
                return;
            }

            // Ürünü sepete ekle
            List<string> sepet = Session["Sepet"] as List<string>;
            if (sepet == null)
                sepet = new List<string>();
            sepet.Add(urunID);
            Session["Sepet"] = sepet;

            // Sepet sayfasına yönlendir
            Response.Redirect("WebForm5.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void lnkCikis_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("WebForm1.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}