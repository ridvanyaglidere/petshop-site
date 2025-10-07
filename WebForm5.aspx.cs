using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace petshopint2
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";
        private string selectedUrunID; // To store the selected product ID for the detail section
        private string kullaniciAdi; // To store the user's name persistently

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["TelefonNo"] != null)
                {
                    string telefonno = Session["TelefonNo"].ToString();
                    lblMesaj.Text = $"Debug: TelefonNo = {telefonno}";
                    lblMesaj.Visible = true;
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "SELECT id, isimsoyisim FROM [dbo].[kullanicilar] WHERE telefonno = @telefonno";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@telefonno", telefonno);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        kullaniciAdi = reader["isimsoyisim"].ToString();
                                        lblMesaj.Text += $"; isimsoyisim = {kullaniciAdi}"; // Debug
                                        // Handle short or invalid isimsoyisim
                                        if (string.IsNullOrWhiteSpace(kullaniciAdi) || kullaniciAdi.Length < 2)
                                        {
                                            kullaniciAdi = "Kullanıcı";
                                            lblKullaniciAdi.Text = "Hoş Geldiniz, Kullanıcı!";
                                        }
                                        else
                                        {
                                            lblKullaniciAdi.Text = $"Hoş Geldiniz, {kullaniciAdi}!";
                                        }
                                    }
                                    else
                                    {
                                        lblKullaniciAdi.Text = "Hoş Geldiniz! Kullanıcı adı bulunamadı.";
                                        lblMesaj.Text = $"Debug: TelefonNo {telefonno} için kullanıcı bulunamadı.";
                                        lblMesaj.Visible = true;
                                        kullaniciAdi = "Bilinmeyen Kullanıcı";
                                    }
                                }
                            }
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblKullaniciAdi.Text = "Hata: " + ex.Message;
                        lblMesaj.Text = $"Debug: Page_Load hata - {ex.Message}";
                        lblMesaj.Visible = true;
                        kullaniciAdi = "Bilinmeyen Kullanıcı";
                    }

                    DisplayCart();
                    LoadPreviousOrders(); // Load previous orders on page load
                }
                else
                {
                    lblKullaniciAdi.Text = "Hoş Geldiniz! Oturum bilgisi bulunamadı.";
                    lblMesaj.Text = "Debug: Session[TelefonNo] null. Giriş yapın.";
                    lblMesaj.Visible = true;
                    Response.Redirect("WebForm3.aspx?returnUrl=WebForm5.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        private void LoadPreviousOrders()
        {
            if (Session["TelefonNo"] == null)
            {
                lblMesaj.Text = "Debug: Session[TelefonNo] null. Giriş yapın.";
                lblMesaj.Visible = true;
                return;
            }

            string telefonno = Session["TelefonNo"].ToString();
            int musteriID = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Get MusteriID based on TelefonNo
                    string musteriQuery = "SELECT id FROM kullanicilar WHERE telefonno = @telefonno";
                    using (SqlCommand cmd = new SqlCommand(musteriQuery, conn))
                    {
                        cmd.Parameters.Clear(); // Ensure no stale parameters
                        cmd.Parameters.AddWithValue("@telefonno", telefonno);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            musteriID = Convert.ToInt32(result);
                            lblMesaj.Text = $"Debug: MusteriID = {musteriID} retrieved for TelefonNo = {telefonno}";
                        }
                        else
                        {
                            lblMesaj.Text = $"Debug: Kullanıcı bulunamadı (Telefon: {telefonno}).";
                            lblMesaj.Visible = true;
                            lblModalDebug.Text = $"Debug: Kullanıcı bulunamadı (Telefon: {telefonno}).";
                            lblModalDebug.Visible = true;
                            return;
                        }
                    }

                    // Fetch previous orders from gecmis_siparisler
                    string ordersQuery = @"
                        SELECT SiparisID, UrunAdi, Adet, ToplamFiyat, SiparisTarihi, SiparisNo
                        FROM gecmis_siparisler
                        WHERE MusteriID = @MusteriID
                        ORDER BY SiparisTarihi DESC";
                    using (SqlCommand cmd = new SqlCommand(ordersQuery, conn))
                    {
                        cmd.Parameters.Clear(); // Clear parameters to avoid conflicts
                        cmd.Parameters.AddWithValue("@MusteriID", musteriID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Debug: Log DataTable content
                            StringBuilder debugData = new StringBuilder();
                            debugData.Append($"Debug: {dt.Rows.Count} orders loaded for MusteriID = {musteriID}");
                            if (dt.Rows.Count > 0)
                            {
                                debugData.Append("; Rows: ");
                                foreach (DataRow row in dt.Rows)
                                {
                                    debugData.Append($"[SiparisID={row["SiparisID"]}, UrunAdi={row["UrunAdi"]}, Adet={row["Adet"]}, ToplamFiyat={row["ToplamFiyat"]}, SiparisTarihi={row["SiparisTarihi"]}, SiparisNo={row["SiparisNo"]}] ");
                                }
                            }

                            lblMesaj.Text += $"; Önceki sipariş sayısı = {dt.Rows.Count}";
                            lblMesaj.Visible = true;

                            if (dt.Rows.Count > 0)
                            {
                                ViewState["PastOrders"] = dt; // Store in ViewState
                                gvPastOrders.DataSource = dt;
                                gvPastOrders.DataBind();
                                gvPastOrders.Visible = true;
                                lblNoOrders.Visible = false;
                                lblModalDebug.Visible = false; // Hide debug label
                                lblMesaj.Text += $"; Data bound to gvPastOrders; GridView rows = {gvPastOrders.Rows.Count}";
                            }
                            else
                            {
                                ViewState["PastOrders"] = null;
                                gvPastOrders.DataSource = null;
                                gvPastOrders.DataBind();
                                gvPastOrders.Visible = false;
                                lblNoOrders.Visible = true;
                                lblModalDebug.Text = "Debug: No orders found.";
                                lblModalDebug.Visible = true;
                                lblMesaj.Text += "; No orders found, showing lblNoOrders";
                            }
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Debug: LoadPreviousOrders hata - {ex.Message} (MusteriID: {musteriID})";
                lblMesaj.Visible = true;
                lblModalDebug.Text = $"Error: {ex.Message}";
                lblModalDebug.Visible = true;
            }
        }

        protected void gvPastOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPastOrders.PageIndex = e.NewPageIndex;
            if (ViewState["PastOrders"] != null)
            {
                DataTable dt = (DataTable)ViewState["PastOrders"];
                gvPastOrders.DataSource = dt;
                gvPastOrders.DataBind();
                lblMesaj.Text += $"; PageIndex changed; GridView rows = {gvPastOrders.Rows.Count}";
                lblMesaj.Visible = true;
                lblNoOrders.Visible = false;
                lblModalDebug.Visible = false;
            }
            else
            {
                LoadPreviousOrders();
            }
        }

        private void DisplayCart()
        {
            List<string> sepet = Session["Sepet"] as List<string>;
            if (sepet == null || sepet.Count == 0)
            {
                lblKullaniciAdi.Text = $"Hoş Geldiniz, {kullaniciAdi}!";
                lblToplamFiyat.Text = "Toplam: 0,00 TL";
                gvSepet.DataSource = null;
                gvSepet.DataBind();
                lblMesaj.Text = "Sepetiniz boş!";
                lblMesaj.Visible = true;
                return;
            }

            var cartItems = sepet.GroupBy(id => id)
                                 .Select(g => new { UrunID = g.Key, Adet = g.Count() })
                                 .ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("UrunID", typeof(string));
            dt.Columns.Add("UrunAdi", typeof(string));
            dt.Columns.Add("Fiyat", typeof(decimal));
            dt.Columns.Add("ResimYolu", typeof(string));
            dt.Columns.Add("Aciklama", typeof(string));
            dt.Columns.Add("Adet", typeof(int));
            dt.Columns.Add("ToplamFiyat", typeof(decimal));

            decimal toplamFiyat = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (var item in cartItems)
                    {
                        if (!int.TryParse(item.UrunID, out int urunID))
                        {
                            lblMesaj.Text = $"Debug: Geçersiz UrunID: {item.UrunID}";
                            lblMesaj.Visible = true;
                            continue;
                        }

                        string query = "SELECT UrunAdi, Fiyat, ResimYolu, Aciklama FROM urunler WHERE UrunID = @UrunID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UrunID", urunID);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    DataRow row = dt.NewRow();
                                    row["UrunID"] = item.UrunID;
                                    row["UrunAdi"] = reader["UrunAdi"].ToString();
                                    row["Fiyat"] = Convert.ToDecimal(reader["Fiyat"]);
                                    row["ResimYolu"] = reader["ResimYolu"].ToString();
                                    row["Aciklama"] = reader["Aciklama"].ToString();
                                    row["Adet"] = item.Adet;
                                    row["ToplamFiyat"] = item.Adet * Convert.ToDecimal(reader["Fiyat"]);
                                    toplamFiyat += item.Adet * Convert.ToDecimal(reader["Fiyat"]);
                                    dt.Rows.Add(row);
                                }
                            }
                        }
                    }
                    conn.Close();
                }

                if (gvSepet != null)
                {
                    gvSepet.DataSource = dt;
                    gvSepet.DataBind();
                }
                if (lblToplamFiyat != null)
                {
                    lblToplamFiyat.Text = $"Toplam: {toplamFiyat:C}";
                }
                lblMesaj.Visible = false;
            }
            catch (Exception ex)
            {
                lblKullaniciAdi.Text = $"Hoş Geldiniz, {kullaniciAdi}!";
                lblMesaj.Text = $"Debug: DisplayCart hata - {ex.Message}";
                lblMesaj.Visible = true;
            }
        }

        protected void gvSepet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string urunID = e.CommandArgument.ToString();
            selectedUrunID = urunID;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UrunAdi, Fiyat, ResimYolu, Aciklama FROM urunler WHERE UrunID = @UrunID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!int.TryParse(urunID, out int parsedUrunID))
                        {
                            lblMesaj.Text = $"Debug: Geçersiz UrunID: {urunID}";
                            lblMesaj.Visible = true;
                            return;
                        }
                        cmd.Parameters.AddWithValue("@UrunID", parsedUrunID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblProductDetail.Text = $"Ürün: {reader["UrunAdi"]}<br />Fiyat: {Convert.ToDecimal(reader["Fiyat"]):C}<br />Açıklama: {reader["Aciklama"]}";
                                imgSelectedProduct.ImageUrl = reader["ResimYolu"].ToString();
                                imgSelectedProduct.Visible = true;
                                btnAddToCartDetail.Visible = true;
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Debug: gvSepet_RowCommand hata - {ex.Message}";
                lblMesaj.Visible = true;
                return;
            }

            if (e.CommandName == "Ekle")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string stockQuery = "SELECT Stok FROM urunler WHERE UrunID = @UrunID";
                        using (SqlCommand cmd = new SqlCommand(stockQuery, conn))
                        {
                            if (!int.TryParse(urunID, out int parsedUrunID))
                            {
                                lblMesaj.Text = $"Debug: Geçersiz UrunID: {urunID}";
                                lblMesaj.Visible = true;
                                return;
                            }
                            cmd.Parameters.AddWithValue("@UrunID", parsedUrunID);
                            object result = cmd.ExecuteScalar();
                            if (result != null)
                            {
                                int stock = Convert.ToInt32(result);
                                List<string> sepet = Session["Sepet"] as List<string>;
                                int currentCount = sepet?.Count(id => id == urunID) ?? 0;
                                if (stock <= currentCount)
                                {
                                    lblMesaj.Text = "Ürün stokta yok!";
                                    lblMesaj.Visible = true;
                                    return;
                                }
                            }
                        }
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = $"Debug: Ekle hata - {ex.Message}";
                    lblMesaj.Visible = true;
                    return;
                }

                List<string> sepetAdd = Session["Sepet"] as List<string>;
                if (sepetAdd == null)
                {
                    sepetAdd = new List<string>();
                }
                sepetAdd.Add(urunID);
                Session["Sepet"] = sepetAdd;
                DisplayCart();
                lblMesaj.Text = "Ürün sepete eklendi!";
                lblMesaj.Visible = true;
            }
            else if (e.CommandName == "Kaldir")
            {
                List<string> sepet = Session["Sepet"] as List<string>;
                if (sepet != null && sepet.Contains(urunID))
                {
                    sepet.Remove(urunID);
                    Session["Sepet"] = sepet;
                    DisplayCart();
                    lblMesaj.Text = "Ürün sepetten kaldırıldı!";
                    lblMesaj.Visible = true;
                }
                else
                {
                    lblMesaj.Text = "Ürün sepetinizde bulunamadı!";
                    lblMesaj.Visible = true;
                }
            }
        }

        protected void btnAddToCartDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedUrunID))
            {
                lblMesaj.Text = "Lütfen bir ürün seçin!";
                lblMesaj.Visible = true;
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string stockQuery = "SELECT Stok FROM urunler WHERE UrunID = @UrunID";
                    using (SqlCommand cmd = new SqlCommand(stockQuery, conn))
                    {
                        if (!int.TryParse(selectedUrunID, out int parsedUrunID))
                        {
                            lblMesaj.Text = $"Debug: Geçersiz UrunID: {selectedUrunID}";
                            lblMesaj.Visible = true;
                            return;
                        }
                        cmd.Parameters.AddWithValue("@UrunID", parsedUrunID);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int stock = Convert.ToInt32(result);
                            List<string> sepet = Session["Sepet"] as List<string>;
                            int currentCount = sepet?.Count(id => id == selectedUrunID) ?? 0;
                            if (stock <= currentCount)
                            {
                                lblMesaj.Text = "Ürün stokta yok!";
                                lblMesaj.Visible = true;
                                return;
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Debug: btnAddToCartDetail hata - {ex.Message}";
                lblMesaj.Visible = true;
                return;
            }

            List<string> sepetAdd = Session["Sepet"] as List<string>;
            if (sepetAdd == null)
            {
                sepetAdd = new List<string>();
            }
            sepetAdd.Add(selectedUrunID);
            Session["Sepet"] = sepetAdd;
            DisplayCart();
            lblMesaj.Text = "Ürün sepete eklendi!";
            lblMesaj.Visible = true;
        }

        protected void btnSepetiOnayla_Click(object sender, EventArgs e)
        {
            List<string> sepet = Session["Sepet"] as List<string>;
            if (sepet == null || sepet.Count == 0)
            {
                lblMesaj.Text = "Sepetiniz boş!";
                lblMesaj.Visible = true;
                return;
            }

            var cartItems = sepet.GroupBy(id => id)
                                 .Select(g => new { UrunID = g.Key, Adet = g.Count() })
                                 .ToList();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Stok kontrolü
                            foreach (var item in cartItems)
                            {
                                if (!int.TryParse(item.UrunID, out int urunID))
                                {
                                    lblMesaj.Text = $"Debug: Geçersiz UrunID: {item.UrunID}";
                                    lblMesaj.Visible = true;
                                    transaction.Rollback();
                                    return;
                                }

                                string stockQuery = "SELECT Stok FROM urunler WHERE UrunID = @UrunID";
                                using (SqlCommand cmd = new SqlCommand(stockQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@UrunID", urunID);
                                    object result = cmd.ExecuteScalar();
                                    if (result != null)
                                    {
                                        int stock = Convert.ToInt32(result);
                                        if (stock < item.Adet)
                                        {
                                            lblMesaj.Text = $"Ürün ID {item.UrunID} için yeterli stok yok!";
                                            lblMesaj.Visible = true;
                                            transaction.Rollback();
                                            return;
                                        }
                                    }
                                }
                            }

                            // Kullanıcı bilgileri
                            string telefonno = Session["TelefonNo"].ToString();
                            int musteriID = 0;
                            string musteriQuery = "SELECT id, isimsoyisim FROM kullanicilar WHERE telefonno = @telefonno";
                            using (SqlCommand cmd = new SqlCommand(musteriQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@telefonno", telefonno);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        musteriID = Convert.ToInt32(reader["id"]);
                                        kullaniciAdi = reader["isimsoyisim"].ToString();
                                    }
                                    else
                                    {
                                        lblMesaj.Text = $"Debug: Kullanıcı bulunamadı (Telefon: {telefonno}).";
                                        lblMesaj.Visible = true;
                                        transaction.Rollback();
                                        return;
                                    }
                                }
                            }

                            // Rastgele sipariş numarası üret
                            string siparisNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

                            // Siparişleri kaydet (gecmis_siparisler)
                            foreach (var item in cartItems)
                            {
                                if (!int.TryParse(item.UrunID, out int urunID))
                                {
                                    lblMesaj.Text = $"Debug: Geçersiz UrunID: {item.UrunID}";
                                    lblMesaj.Visible = true;
                                    transaction.Rollback();
                                    return;
                                }

                                // Get UrunAdi and Fiyat
                                string productQuery = "SELECT UrunAdi, Fiyat FROM urunler WHERE UrunID = @UrunID";
                                string urunAdi = "";
                                decimal fiyat = 0;
                                using (SqlCommand cmd = new SqlCommand(productQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@UrunID", urunID);
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            urunAdi = reader["UrunAdi"].ToString();
                                            fiyat = Convert.ToDecimal(reader["Fiyat"]);
                                        }
                                        else
                                        {
                                            lblMesaj.Text = $"Debug: Ürün ID {item.UrunID} bulunamadı.";
                                            lblMesaj.Visible = true;
                                            transaction.Rollback();
                                            return;
                                        }
                                    }
                                }

                                // Insert into gecmis_siparisler
                                string orderQuery = @"
                                    INSERT INTO gecmis_siparisler (MusteriID, UrunID, UrunAdi, Adet, ToplamFiyat, SiparisTarihi, KullaniciAdi, SiparisNo)
                                    VALUES (@MusteriID, @UrunID, @UrunAdi, @Adet, @ToplamFiyat, @SiparisTarihi, @KullaniciAdi, @SiparisNo)";
                                using (SqlCommand cmd = new SqlCommand(orderQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@MusteriID", musteriID);
                                    cmd.Parameters.AddWithValue("@UrunID", urunID);
                                    cmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                                    cmd.Parameters.AddWithValue("@Adet", item.Adet);
                                    cmd.Parameters.AddWithValue("@ToplamFiyat", item.Adet * fiyat);
                                    cmd.Parameters.AddWithValue("@SiparisTarihi", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                                    cmd.Parameters.AddWithValue("@SiparisNo", siparisNo);
                                    cmd.ExecuteNonQuery();
                                }

                                // Update stock
                                string updateStockQuery = "UPDATE urunler SET Stok = Stok - @Adet WHERE UrunID = @UrunID";
                                using (SqlCommand cmd = new SqlCommand(updateStockQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Adet", item.Adet);
                                    cmd.Parameters.AddWithValue("@UrunID", urunID);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            // İşlemi onayla
                            transaction.Commit();

                            // Sepeti sıfırla
                            Session["Sepet"] = null;
                            DisplayCart();
                            LoadPreviousOrders(); // Reload previous orders after confirming new order

                            // Onay mesajını hazırla
                            lblPurchaseMessage.Text = $"Sipariş Onaylandı! 🐾<br />Alıcı: {kullaniciAdi}<br />Sipariş No: {siparisNo}";

                            // Modal'ı göster
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showPurchaseConfirmation", "showPurchaseConfirmation();", true);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            lblMesaj.Text = $"Debug: btnSepetiOnayla hata - {ex.Message}";
                            lblMesaj.Visible = true;
                            return;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Debug: btnSepetiOnayla dış hata - {ex.Message}";
                lblMesaj.Visible = true;
            }
        }

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm4.aspx", false);
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