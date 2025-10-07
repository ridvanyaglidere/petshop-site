using System;
using System.Web.UI;
using System.Data.SqlClient;

namespace vetcare
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminID"] != null)
                {
                    Response.Redirect("WebForm6.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                if (Session["TelefonNo"] != null)
                {
                    Response.Redirect("WebForm4.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void btnAdminGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT AdminID FROM adminler WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        cmd.Parameters.AddWithValue("@Sifre", sifre);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            Session["AdminID"] = result.ToString();
                            lblMesaj.Text = "Giriş başarılı! Yönlendiriliyorsunuz...";
                            Response.Redirect("WebForm6.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            lblMesaj.Text = "Hatalı kullanıcı adı veya şifre.";
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Hata: " + ex.Message;
            }

            // Animasyonu her durumda tetikle (başarılı veya başarısız giriş sonrası)
            ScriptManager.RegisterStartupScript(this, GetType(), "triggerAnimation", "triggerAnimation();", true);
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