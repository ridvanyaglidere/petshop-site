using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace petshopint2
{
    public partial class WebForm6 : Page
    {
        protected Label lblMesaj;
        protected Label lblSiparisSayisi;
        protected Label lblToplamGelir;
        protected Label lblKullaniciSayisi;
        protected GridView gvSiparisler;
        protected GridView gvMesajlar;
        protected TextBox txtUrunAdi;
        protected TextBox txtAlisFiyati;
        protected TextBox txtFiyat;
        protected TextBox txtAciklama;
        protected TextBox txtStok;
        protected DropDownList ddlKategoriler;
        protected DropDownList ddlMarkalar;
        protected DropDownList ddlUrunTuru;
        protected FileUpload fuResim;
        protected Button btnUrunEkle;
        protected GridView gvUrunler;
        protected GridView gvKullanicilar;
        protected TextBox txtKategoriAdi;
        protected Button btnKategoriEkle;
        protected GridView gvKategoriler;
        protected TextBox txtMarkaAdi;
        protected Button btnMarkaEkle;
        protected GridView gvMarkalar;
        protected TextBox txtGiris;
        protected TextBox txtBizKimiz;
        protected TextBox txtMisyon;
        protected TextBox txtHizmetler;
        protected Button btnHakkimizdaGuncelle;

        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminID"] == null)
                {
                    Response.Redirect("WebForm1.aspx?redirected=true", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }

                SiparisleriListele();
                UrunleriListele();
                KategorileriListele();
                MarkalariListele();
                KullanicilariListele();
                MesajlariListele();
                IstatistikleriGoster();
                HakkimizdaIcerikleriniYukle();
                KategorileriDoldur();
                MarkalariDoldur();
                UrunTurleriniDoldur();
                RenkleriDoldur();
                LoadPriceChartData();
            }
        }

        private void RenkleriDoldur()
        {
            try
            {
                DataTable dtRenkler = new DataTable();
                dtRenkler.Columns.Add("Renk", typeof(string));
                dtRenkler.Rows.Add("Renk Seçin");
                dtRenkler.Rows.Add("Kırmızı");
                dtRenkler.Rows.Add("Mavi");
                dtRenkler.Rows.Add("Yeşil");
                dtRenkler.Rows.Add("Siyah");
                dtRenkler.Rows.Add("Beyaz");

                ddlRenk.DataSource = dtRenkler;
                ddlRenk.DataTextField = "Renk";
                ddlRenk.DataValueField = "Renk";
                ddlRenk.DataBind();
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Renkler yüklenirken hata: {ex.Message}";
            }
        }

        private void LoadPriceChartData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT AVG(AlisFiyati) AS AvgPurchasePrice, AVG(Fiyat) AS AvgSalePrice, AVG(Fiyat - AlisFiyati) AS AvgProfit FROM urunler";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal avgPurchasePrice = reader["AvgPurchasePrice"] != DBNull.Value ? Convert.ToDecimal(reader["AvgPurchasePrice"]) : 0;
                                decimal avgSalePrice = reader["AvgSalePrice"] != DBNull.Value ? Convert.ToDecimal(reader["AvgSalePrice"]) : 0;
                                decimal avgProfit = reader["AvgProfit"] != DBNull.Value ? Convert.ToDecimal(reader["AvgProfit"]) : 0;
                                ViewState["AvgPurchasePrice"] = avgPurchasePrice;
                                ViewState["AvgSalePrice"] = avgSalePrice;
                                ViewState["AvgProfit"] = avgProfit;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Fiyat grafiği verileri yüklenirken hata: {ex.Message}";
            }
        }

        private void HakkimizdaIcerikleriniYukle()
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
                            int rowCount = 0;
                            while (reader.Read())
                            {
                                rowCount++;
                                string bolum = reader["Bolum"].ToString();
                                string icerik = reader["Icerik"].ToString();
                                switch (bolum)
                                {
                                    case "Giris":
                                        txtGiris.Text = icerik;
                                        break;
                                    case "BizKimiz":
                                        txtBizKimiz.Text = icerik;
                                        break;
                                    case "Misyon":
                                        txtMisyon.Text = icerik;
                                        break;
                                    case "Hizmetler":
                                        txtHizmetler.Text = icerik;
                                        break;
                                }
                            }
                            lblMesaj.Text = $"Hakkımızda içerikleri yüklendi: {rowCount} kayıt bulundu.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Hakkımızda içerikleri yüklenirken hata: {ex.Message}";
            }
        }

        protected void btnHakkimizdaGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE Hakkimizda SET Icerik = @Icerik, SonGuncellemeTarihi = GETDATE() WHERE Bolum = @Bolum;
                        IF @@ROWCOUNT = 0
                            INSERT INTO Hakkimizda (Bolum, Icerik, SonGuncellemeTarihi) VALUES (@Bolum, @Icerik, GETDATE());
                    ";
                    string log = "";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Bolum", "Giris");
                        cmd.Parameters.AddWithValue("@Icerik", txtGiris.Text.Trim());
                        int rowsAffected = cmd.ExecuteNonQuery();
                        log += $"Giris: {rowsAffected} rows affected. ";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Bolum", "BizKimiz");
                        cmd.Parameters.AddWithValue("@Icerik", txtBizKimiz.Text.Trim());
                        int rowsAffected = cmd.ExecuteNonQuery();
                        log += $"BizKimiz: {rowsAffected} rows affected. ";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Bolum", "Misyon");
                        cmd.Parameters.AddWithValue("@Icerik", txtMisyon.Text.Trim());
                        int rowsAffected = cmd.ExecuteNonQuery();
                        log += $"Misyon: {rowsAffected} rows affected. ";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Bolum", "Hizmetler");
                        cmd.Parameters.AddWithValue("@Icerik", txtHizmetler.Text.Trim());
                        int rowsAffected = cmd.ExecuteNonQuery();
                        log += $"Hizmetler: {rowsAffected} rows affected.";
                    }

                    lblMesaj.Text = $"Hakkımızda içerikleri başarıyla güncellendi! ({log})";
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Hakkımızda güncellenirken hata: {ex.Message}";
            }
        }

        private void KategorileriDoldur()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT KategoriID, KategoriAdi FROM kategoriler WHERE KategoriAdi IN ('Kedi Ürünü', 'Köpek Ürünü')";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlKategoriler.DataSource = dt;
                        ddlKategoriler.DataTextField = "KategoriAdi";
                        ddlKategoriler.DataValueField = "KategoriID";
                        ddlKategoriler.DataBind();
                        ddlKategoriler.Items.Insert(0, new ListItem("Kategori Seçin", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Kategoriler yüklenirken hata: {ex.Message}";
            }
        }

        private void MarkalariDoldur()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MarkaID, MarkaAdi FROM markalar";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ddlMarkalar.DataSource = dt;
                        ddlMarkalar.DataTextField = "MarkaAdi";
                        ddlMarkalar.DataValueField = "MarkaID";
                        ddlMarkalar.DataBind();
                        ddlMarkalar.Items.Insert(0, new ListItem("Marka Seçin", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Markalar yüklenirken hata: {ex.Message}";
            }
        }

        private void UrunTurleriniDoldur()
        {
            try
            {
                DataTable dtUrunTurleri = new DataTable();
                dtUrunTurleri.Columns.Add("UrunTuru", typeof(string));
                dtUrunTurleri.Rows.Add("Ürün Türü Seçin");
                dtUrunTurleri.Rows.Add("Yatak");
                dtUrunTurleri.Rows.Add("Mama");
                dtUrunTurleri.Rows.Add("Oyuncak");
                dtUrunTurleri.Rows.Add("Tasma");
                dtUrunTurleri.Rows.Add("Su Kabı");

                ddlUrunTuru.DataSource = dtUrunTurleri;
                ddlUrunTuru.DataTextField = "UrunTuru";
                ddlUrunTuru.DataValueField = "UrunTuru";
                ddlUrunTuru.DataBind();
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Ürün türleri yüklenirken hata: {ex.Message}";
            }
        }

        private void SiparisleriListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT s.SiparisID, k.isimsoyisim AS MusteriAdi, u.UrunAdi, s.Adet, s.SiparisTarihi " +
                                   "FROM siparis s INNER JOIN kullanicilar k ON s.MusteriID = k.id INNER JOIN urunler u ON s.UrunID = u.UrunID";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvSiparisler.DataSource = dt;
                        gvSiparisler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Siparişler yüklenirken hata: {ex.Message}";
            }
        }

        private void MesajlariListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MesajID, AdSoyad, Eposta, Konu, Mesaj, Tarih, Okundu FROM mesajlar ORDER BY Tarih DESC";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvMesajlar.DataSource = dt;
                        gvMesajlar.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Mesajlar yüklenirken hata: {ex.Message}";
            }
        }

        protected void gvMesajlar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleRead")
            {
                int mesajID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string getStatusQuery = "SELECT Okundu FROM mesajlar WHERE MesajID = @MesajID";
                        bool okundu;
                        using (SqlCommand getCmd = new SqlCommand(getStatusQuery, conn))
                        {
                            getCmd.Parameters.AddWithValue("@MesajID", mesajID);
                            okundu = (bool)getCmd.ExecuteScalar();
                        }

                        string updateQuery = "UPDATE mesajlar SET Okundu = @Okundu WHERE MesajID = @MesajID";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Okundu", !okundu);
                            cmd.Parameters.AddWithValue("@MesajID", mesajID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MesajlariListele();
                    lblMesaj.Text = "Mesaj durumu güncellendi.";
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = $"Mesaj durumu güncellenirken hata: {ex.Message}";
                }
            }
        }

        protected void gvMesajlar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int mesajID = Convert.ToInt32(gvMesajlar.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM mesajlar WHERE MesajID = @MesajID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MesajID", mesajID);
                        cmd.ExecuteNonQuery();
                    }
                }
                MesajlariListele();
                lblMesaj.Text = "Mesaj başarıyla silindi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Mesaj silinirken hata: {ex.Message}";
            }
        }

        private void UrunleriListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT u.UrunID, u.UrunAdi, u.AlisFiyati, u.Fiyat, u.ResimYolu, u.Aciklama, u.Stok, k.KategoriAdi, m.MarkaAdi, u.UrunTuru " +
                                   "FROM urunler u INNER JOIN kategoriler k ON u.KategoriID = k.KategoriID INNER JOIN markalar m ON u.MarkaID = m.MarkaID";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvUrunler.DataSource = dt;
                        gvUrunler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Ürünler yüklenirken hata: {ex.Message}";
            }
        }

        private void KategorileriListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT KategoriID, KategoriAdi FROM kategoriler";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvKategoriler.DataSource = dt;
                        gvKategoriler.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Kategoriler yüklenirken hata: {ex.Message}";
            }
        }

        private void MarkalariListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MarkaID, MarkaAdi FROM markalar";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvMarkalar.DataSource = dt;
                        gvMarkalar.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Markalar yüklenirken hata: {ex.Message}";
            }
        }

        private void KullanicilariListele()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, isimsoyisim, telefonno FROM kullanicilar";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvKullanicilar.DataSource = dt;
                        gvKullanicilar.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Kullanıcılar yüklenirken hata: {ex.Message}";
            }
        }

        private void IstatistikleriGoster()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string siparisSayisiQuery = "SELECT COUNT(*) FROM siparis";
                    using (SqlCommand cmd = new SqlCommand(siparisSayisiQuery, conn))
                    {
                        int siparisSayisi = (int)cmd.ExecuteScalar();
                        lblSiparisSayisi.Text = $"Toplam Sipariş Sayısı: {siparisSayisi}";
                    }

                    string gelirQuery = "SELECT SUM(s.Adet * u.Fiyat) FROM siparis s INNER JOIN urunler u ON s.UrunID = u.UrunID";
                    using (SqlCommand cmd = new SqlCommand(gelirQuery, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal toplamGelir = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        lblToplamGelir.Text = $"Toplam Gelir: {toplamGelir:C}";
                    }

                    string kullaniciSayisiQuery = "SELECT COUNT(*) FROM kullanicilar";
                    using (SqlCommand cmd = new SqlCommand(kullaniciSayisiQuery, conn))
                    {
                        int kullaniciSayisi = (int)cmd.ExecuteScalar();
                        lblKullaniciSayisi.Text = $"Toplam Kullanıcı Sayısı: {kullaniciSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"İstatistikler yüklenirken hata: {ex.Message}";
            }
        }

        protected void gvSiparisler_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int siparisID = Convert.ToInt32(gvSiparisler.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM siparis WHERE SiparisID = @SiparisID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SiparisID", siparisID);
                        cmd.ExecuteNonQuery();
                    }
                }
                SiparisleriListele();
                IstatistikleriGoster();
                lblMesaj.Text = "Sipariş silindi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Sipariş silinirken hata: {ex.Message}";
            }
        }

        protected void btnUrunEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string urunAdi = txtUrunAdi.Text.Trim();
                if (!decimal.TryParse(txtAlisFiyati.Text.Trim(), out decimal alisFiyati) || alisFiyati <= 0)
                {
                    lblMesaj.Text = "Geçerli bir alış fiyatı girin.";
                    return;
                }
                if (!decimal.TryParse(txtFiyat.Text.Trim(), out decimal satisFiyati) || satisFiyati <= 0)
                {
                    lblMesaj.Text = "Geçerli bir satış fiyatı girin.";
                    return;
                }
                if (alisFiyati >= satisFiyati)
                {
                    lblMesaj.Text = "Alış fiyatı, satış fiyatından düşük olmalıdır.";
                    return;
                }
                string aciklama = txtAciklama.Text.Trim();
                if (!int.TryParse(txtStok.Text.Trim(), out int stok) || stok < 0)
                {
                    lblMesaj.Text = "Geçerli bir stok miktarı girin.";
                    return;
                }
                int kategoriID = int.Parse(ddlKategoriler.SelectedValue);
                int markaID = int.Parse(ddlMarkalar.SelectedValue);
                string urunTuru = ddlUrunTuru.SelectedValue;
                string renk = ddlRenk.SelectedValue;

                if (kategoriID == 0 || markaID == 0 || urunTuru == "Ürün Türü Seçin" || renk == "Renk Seçin")
                {
                    lblMesaj.Text = "Lütfen bir kategori, marka, ürün türü ve renk seçin.";
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT UrunID, Stok FROM urunler WHERE UrunAdi = @UrunAdi AND KategoriID = @KategoriID AND MarkaID = @MarkaID AND UrunTuru = @UrunTuru AND Renk = @Renk";
                    int existingUrunID = -1;
                    int existingStok = 0;
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                        checkCmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                        checkCmd.Parameters.AddWithValue("@MarkaID", markaID);
                        checkCmd.Parameters.AddWithValue("@UrunTuru", urunTuru);
                        checkCmd.Parameters.AddWithValue("@Renk", renk);
                        using (SqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                existingUrunID = reader.GetInt32(0);
                                existingStok = reader.GetInt32(1);
                            }
                        }
                    }

                    if (existingUrunID != -1)
                    {
                        string updateQuery = "UPDATE urunler SET Stok = @Stok WHERE UrunID = @UrunID";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@Stok", existingStok + stok);
                            updateCmd.Parameters.AddWithValue("@UrunID", existingUrunID);
                            updateCmd.ExecuteNonQuery();
                        }

                        txtUrunAdi.Text = "";
                        txtAlisFiyati.Text = "";
                        txtFiyat.Text = "";
                        txtAciklama.Text = "";
                        txtStok.Text = "";
                        ddlKategoriler.SelectedIndex = 0;
                        ddlMarkalar.SelectedIndex = 0;
                        ddlUrunTuru.SelectedIndex = 0;
                        ddlRenk.SelectedIndex = 0;

                        lblMesaj.Text = "Bu ürün zaten mevcut. Stok miktarı güncellendi.";
                        UrunleriListele();
                        LoadPriceChartData();
                        return;
                    }

                    string resimYolu = "";
                    if (fuResim.HasFile)
                    {
                        string uzanti = Path.GetExtension(fuResim.FileName).ToLower();
                        if (uzanti != ".jpg" && uzanti != ".jpeg" && uzanti != ".png")
                        {
                            lblMesaj.Text = "Lütfen yalnızca JPG, JPEG veya PNG dosyası yükleyin.";
                            return;
                        }
                        if (fuResim.PostedFile.ContentLength > 5 * 1024 * 1024)
                        {
                            lblMesaj.Text = "Dosya boyutu 5MB’tan büyük olamaz.";
                            return;
                        }
                        string fotoKlasoru = Server.MapPath("~/Foto/");
                        if (!Directory.Exists(fotoKlasoru))
                        {
                            Directory.CreateDirectory(fotoKlasoru);
                        }
                        string dosyaAdi = Guid.NewGuid().ToString() + uzanti;
                        string dosyaYolu = Path.Combine(fotoKlasoru, dosyaAdi);
                        fuResim.SaveAs(dosyaYolu);
                        resimYolu = "/Foto/" + dosyaAdi;
                    }
                    else
                    {
                        lblMesaj.Text = "Lütfen bir resim dosyası seçin.";
                        return;
                    }

                    string insertQuery = "INSERT INTO urunler (UrunAdi, AlisFiyati, Fiyat, ResimYolu, Aciklama, Stok, KategoriID, MarkaID, UrunTuru, Renk) " +
                                        "VALUES (@UrunAdi, @AlisFiyati, @Fiyat, @ResimYolu, @Aciklama, @Stok, @KategoriID, @MarkaID, @UrunTuru, @Renk)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunAdi", urunAdi);
                        cmd.Parameters.AddWithValue("@AlisFiyati", alisFiyati);
                        cmd.Parameters.AddWithValue("@Fiyat", satisFiyati);
                        cmd.Parameters.AddWithValue("@ResimYolu", resimYolu);
                        cmd.Parameters.AddWithValue("@Aciklama", aciklama);
                        cmd.Parameters.AddWithValue("@Stok", stok);
                        cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                        cmd.Parameters.AddWithValue("@MarkaID", markaID);
                        cmd.Parameters.AddWithValue("@UrunTuru", urunTuru);
                        cmd.Parameters.AddWithValue("@Renk", renk);
                        cmd.ExecuteNonQuery();
                    }

                    txtUrunAdi.Text = "";
                    txtAlisFiyati.Text = "";
                    txtFiyat.Text = "";
                    txtAciklama.Text = "";
                    txtStok.Text = "";
                    ddlKategoriler.SelectedIndex = 0;
                    ddlMarkalar.SelectedIndex = 0;
                    ddlUrunTuru.SelectedIndex = 0;
                    ddlRenk.SelectedIndex = 0;

                    lblMesaj.Text = "Ürün başarıyla eklendi!";
                    UrunleriListele();
                    LoadPriceChartData();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Ürün eklenirken hata: {ex.Message}";
            }
        }

        protected void gvUrunler_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int urunID = Convert.ToInt32(gvUrunler.DataKeys[e.RowIndex].Value);
            TextBox txtYeniAlisFiyati = (TextBox)gvUrunler.Rows[e.RowIndex].FindControl("txtYeniAlisFiyati");
            TextBox txtYeniFiyat = (TextBox)gvUrunler.Rows[e.RowIndex].FindControl("txtYeniFiyat");

            if (!decimal.TryParse(txtYeniAlisFiyati.Text.Trim(), out decimal yeniAlisFiyati) || yeniAlisFiyati <= 0)
            {
                lblMesaj.Text = "Geçerli bir alış fiyatı girin.";
                return;
            }
            if (!decimal.TryParse(txtYeniFiyat.Text.Trim(), out decimal yeniSatisFiyati) || yeniSatisFiyati <= 0)
            {
                lblMesaj.Text = "Geçerli bir satış fiyatı girin.";
                return;
            }
            if (yeniAlisFiyati >= yeniSatisFiyati)
            {
                lblMesaj.Text = "Alış fiyatı, satış fiyatından düşük olmalıdır.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE urunler SET AlisFiyati = @AlisFiyati, Fiyat = @Fiyat WHERE UrunID = @UrunID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AlisFiyati", yeniAlisFiyati);
                        cmd.Parameters.AddWithValue("@Fiyat", yeniSatisFiyati);
                        cmd.Parameters.AddWithValue("@UrunID", urunID);
                        cmd.ExecuteNonQuery();
                    }
                }
                gvUrunler.EditIndex = -1;
                UrunleriListele();
                IstatistikleriGoster();
                LoadPriceChartData();
                lblMesaj.Text = "Fiyatlar başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Fiyat güncellenirken hata: {ex.Message}";
            }
        }

        protected void gvUrunler_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int urunID = Convert.ToInt32(gvUrunler.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string siparisKontrolQuery = "SELECT COUNT(*) FROM siparis WHERE UrunID = @UrunID";
                    using (SqlCommand kontrolCmd = new SqlCommand(siparisKontrolQuery, conn))
                    {
                        kontrolCmd.Parameters.AddWithValue("@UrunID", urunID);
                        int siparisSayisi = (int)kontrolCmd.ExecuteScalar();
                        if (siparisSayisi > 0)
                        {
                            lblMesaj.Text = "Bu ürünle ilişkili siparişler var. Önce siparişleri silmelisiniz.";
                            return;
                        }
                    }

                    string resimYoluQuery = "SELECT ResimYolu FROM urunler WHERE UrunID = @UrunID";
                    string resimYolu = "";
                    using (SqlCommand cmd = new SqlCommand(resimYoluQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", urunID);
                        resimYolu = cmd.ExecuteScalar()?.ToString();
                    }

                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                        string dosyaYolu = Server.MapPath(resimYolu);
                        if (File.Exists(dosyaYolu))
                        {
                            File.Delete(dosyaYolu);
                        }
                    }

                    string query = "DELETE FROM urunler WHERE UrunID = @UrunID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", urunID);
                        cmd.ExecuteNonQuery();
                    }
                }
                UrunleriListele();
                LoadPriceChartData();
                lblMesaj.Text = "Ürün başarıyla silindi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Ürün silinirken hata: {ex.Message}";
            }
        }

        protected void gvUrunler_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUrunler.EditIndex = e.NewEditIndex;
            UrunleriListele();
        }

        protected void gvUrunler_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUrunler.EditIndex = -1;
            UrunleriListele();
        }

        protected void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string kategoriAdi = txtKategoriAdi.Text.Trim();
                if (string.IsNullOrEmpty(kategoriAdi))
                {
                    lblMesaj.Text = "Kategori adı boş olamaz.";
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM kategoriler WHERE KategoriAdi = @KategoriAdi";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@KategoriAdi", kategoriAdi);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            lblMesaj.Text = "Bu kategori zaten mevcut.";
                            return;
                        }
                    }

                    string query = "INSERT INTO kategoriler (KategoriAdi) VALUES (@KategoriAdi)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KategoriAdi", kategoriAdi);
                        cmd.ExecuteNonQuery();
                    }
                }
                txtKategoriAdi.Text = "";
                KategorileriListele();
                KategorileriDoldur();
                lblMesaj.Text = "Kategori başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Kategori eklenirken hata: {ex.Message}";
            }
        }

        protected void gvKategoriler_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int kategoriID = Convert.ToInt32(gvKategoriler.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string urunKontrolQuery = "SELECT COUNT(*) FROM urunler WHERE KategoriID = @KategoriID";
                    using (SqlCommand kontrolCmd = new SqlCommand(urunKontrolQuery, conn))
                    {
                        kontrolCmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                        int urunSayisi = (int)kontrolCmd.ExecuteScalar();
                        if (urunSayisi > 0)
                        {
                            lblMesaj.Text = "Bu kategoriye ait ürünler var. Önce ürünleri silmelisiniz.";
                            return;
                        }
                    }

                    string query = "DELETE FROM kategoriler WHERE KategoriID = @KategoriID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KategoriID", kategoriID);
                        cmd.ExecuteNonQuery();
                    }
                }
                KategorileriListele();
                KategorileriDoldur();
                lblMesaj.Text = "Kategori başarıyla silindi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Kategori silinirken hata: {ex.Message}";
            }
        }

        protected void btnMarkaEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string markaAdi = txtMarkaAdi.Text.Trim();
                if (string.IsNullOrEmpty(markaAdi))
                {
                    lblMesaj.Text = "Marka adı boş olamaz.";
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM markalar WHERE MarkaAdi = @MarkaAdi";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MarkaAdi", markaAdi);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            lblMesaj.Text = "Bu marka zaten mevcut.";
                            return;
                        }
                    }

                    string query = "INSERT INTO markalar (MarkaAdi) VALUES (@MarkaAdi)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MarkaAdi", markaAdi);
                        cmd.ExecuteNonQuery();
                    }
                }
                txtMarkaAdi.Text = "";
                MarkalariListele();
                MarkalariDoldur();
                lblMesaj.Text = "Marka başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Marka eklenirken hata: {ex.Message}";
            }
        }

        protected void gvMarkalar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int markaID = Convert.ToInt32(gvMarkalar.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string urunKontrolQuery = "SELECT COUNT(*) FROM urunler WHERE MarkaID = @MarkaID";
                    using (SqlCommand kontrolCmd = new SqlCommand(urunKontrolQuery, conn))
                    {
                        kontrolCmd.Parameters.AddWithValue("@MarkaID", markaID);
                        int urunSayisi = (int)kontrolCmd.ExecuteScalar();
                        if (urunSayisi > 0)
                        {
                            lblMesaj.Text = "Bu markaya ait ürünler var. Önce ürünleri silmelisiniz.";
                            return;
                        }
                    }

                    string query = "DELETE FROM markalar WHERE MarkaID = @MarkaID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MarkaID", markaID);
                        cmd.ExecuteNonQuery();
                    }
                }
                MarkalariListele();
                MarkalariDoldur();
                lblMesaj.Text = "Marka başarıyla silindi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Marka silinirken hata: {ex.Message}";
            }
        }

        protected void gvKullanicilar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int kullaniciID = Convert.ToInt32(gvKullanicilar.DataKeys[e.RowIndex].Value);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string siparisKontrolQuery = "SELECT COUNT(*) FROM siparis WHERE MusteriID = @MusteriID";
                    using (SqlCommand kontrolCmd = new SqlCommand(siparisKontrolQuery, conn))
                    {
                        kontrolCmd.Parameters.AddWithValue("@MusteriID", kullaniciID);
                        int siparisSayisi = (int)kontrolCmd.ExecuteScalar();
                        if (siparisSayisi > 0)
                        {
                            lblMesaj.Text = "Bu kullanıcıya ait siparişler var. Önce siparişleri silmelisiniz.";
                            return;
                        }
                    }

                    string query = "DELETE FROM kullanicilar WHERE id = @KullaniciID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        cmd.ExecuteNonQuery();
                    }
                }
                KullanicilariListele();
                IstatistikleriGoster();
                lblMesaj.Text = "Kullanıcı başarıyla silindi.";
            }
            catch (Exception ex)
            {
                lblMesaj.Text = $"Kullanıcı silinirken hata: {ex.Message}";
            }
        }
    }
}