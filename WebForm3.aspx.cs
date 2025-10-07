using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace petshopint2
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TestConnection();
            }
        }

        private void TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Bağlantı başarılı, istersen log tut burada
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Bağlantı hatası: {ex.Message}", "alert-danger");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string telefonno = TextBox2.Text.Trim();
                string sifre = TextBox3.Text.Trim();

                if (string.IsNullOrEmpty(telefonno) || string.IsNullOrEmpty(sifre))
                {
                    ShowMessage("Telefon No ve Şifre alanları zorunludur!", "alert-danger");
                    // Animasyonu tetikle
                    ScriptManager.RegisterStartupScript(this, GetType(), "triggerAnimation", "triggerAnimation();", true);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM [dbo].[kullanicilar] WHERE telefonno = @telefonno AND sifre = @sifre";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@telefonno", telefonno);
                        cmd.Parameters.AddWithValue("@sifre", sifre);

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        conn.Close();

                        if (count > 0)
                        {
                            // Giriş başarılı, telefonno'yu Session'a kaydet
                            Session["TelefonNo"] = telefonno;
                            Response.Redirect("WebForm4.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            ShowMessage("Hatalı Telefon No veya Şifre!", "alert-danger");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Hata: {ex.Message}", "alert-danger");
            }
            // Animasyonu her durumda tetikle (başarılı veya başarısız giriş sonrası)
            ScriptManager.RegisterStartupScript(this, GetType(), "triggerAnimation", "triggerAnimation();", true);
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
            Response.Redirect("WebForm8.aspx", false);
        }
    }
}