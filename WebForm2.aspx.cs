using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vetcare
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataList1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string isimsoyisim = TextBox1.Text.Trim();
                string telefonno = TextBox2.Text.Trim();
                string sifre = TextBox3.Text.Trim();
                string adres = TextBox4.Text.Trim();

                if (string.IsNullOrEmpty(isimsoyisim) || string.IsNullOrEmpty(telefonno) || string.IsNullOrEmpty(sifre))
                {
                    ShowMessage("İsim Soyisim, Telefon No ve Şifre alanları zorunludur!", "alert-danger");
                    return;
                }

                if (IsTelefonNoExists(telefonno))
                {
                    ShowMessage("Bu telefon numarası zaten kayıtlı! Lütfen başka bir telefon numarası kullanın.", "alert-danger");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO [dbo].[kullanicilar] (isimsoyisim, telefonno, sifre, adres) VALUES (@isimsoyisim, @telefonno, @sifre, @adres)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@isimsoyisim", isimsoyisim);
                        cmd.Parameters.AddWithValue("@telefonno", telefonno);
                        cmd.Parameters.AddWithValue("@sifre", sifre);
                        cmd.Parameters.AddWithValue("@adres", adres);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            ShowMessage("Kayıt başarıyla eklendi!", "alert-success");
                            BindDataList(isimsoyisim, telefonno, adres);
                            // Animasyonu başarılı kayıt sonrası tetikle
                            ScriptManager.RegisterStartupScript(this, GetType(), "triggerAnimation", "triggerAnimation();", true);
                        }
                        else
                        {
                            ShowMessage("Kayıt eklenemedi.", "alert-danger");
                        }
                    }
                }

                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage($"Hata: {ex.Message}", "alert-danger");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ClearForm();
            DataList1.Visible = false;
            ShowMessage("", "");
        }

        private bool IsTelefonNoExists(string telefonno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM kullanicilar WHERE telefonno = @telefonno";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@telefonno", telefonno);
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        conn.Close();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Telefon numarası kontrolü sırasında hata: {ex.Message}", "alert-danger");
                return true;
            }
        }

        private void BindDataList(string isimsoyisim, string telefonno, string adres)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("isimsoyisim");
                dt.Columns.Add("telefonno");
                dt.Columns.Add("adres");

                DataRow row = dt.NewRow();
                row["isimsoyisim"] = isimsoyisim;
                row["telefonno"] = telefonno;
                row["adres"] = adres;
                dt.Rows.Add(row);

                DataList1.DataSource = dt;
                DataList1.DataBind();
                DataList1.Visible = true;
            }
            catch (Exception ex)
            {
                ShowMessage($"Veri yükleme hatası: {ex.Message}", "alert-danger");
            }
        }

        private void ClearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
        }

        private void ShowMessage(string message, string cssClass)
        {
            if (string.IsNullOrEmpty(message))
            {
                MessagePanel.Visible = false;
            }
            else
            {
                MessageLabel.Text = message;
                MessagePanel.CssClass = $"alert {cssClass}";
                MessagePanel.Visible = true;
            }
        }

        protected void btnAdminGiris_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx", false);
        }

        protected void btnKullaniciGiris_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm3.aspx", false);
        }

        protected void btnUyeOl_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx", false);
        }

        protected void btnAnasayfa_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx", false);
        }
    }
}