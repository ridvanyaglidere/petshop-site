using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls; // Ensure this is included for TextBox, Label, Button

namespace petshopint2
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        // Declare controls as protected fields to match WebForm7.aspx
        protected TextBox txtAdSoyad;
        protected TextBox txtEposta;
        protected TextBox txtKonu;
        protected TextBox txtMesaj;
        protected Label lblMesaj;
        protected Button btnGonder;

        // Connection string (consistent with WebForm6.aspx.cs)
        private string connectionString = "Server=RIDVAN\\SQLEXPRESS;Database=int2;Trusted_Connection=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize lblMesaj
                lblMesaj.Text = "";
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                // Server-side validation (as a fallback to client-side validation)
                string adSoyad = txtAdSoyad.Text.Trim();
                string eposta = txtEposta.Text.Trim();
                string konu = txtKonu.Text.Trim();
                string mesaj = txtMesaj.Text.Trim();

                if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(eposta) || string.IsNullOrEmpty(konu) || string.IsNullOrEmpty(mesaj))
                {
                    lblMesaj.Text = "Lütfen tüm alanları doldurun!";
                    return;
                }

                // Validate email format (server-side)
                if (!System.Text.RegularExpressions.Regex.IsMatch(eposta, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    lblMesaj.Text = "Geçerli bir e-posta adresi girin!";
                    return;
                }

                // Insert data into the mesajlar table
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO mesajlar (AdSoyad, Eposta, Konu, Mesaj) " +
                                   "VALUES (@AdSoyad, @Eposta, @Konu, @Mesaj)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        cmd.Parameters.AddWithValue("@Eposta", eposta);
                        cmd.Parameters.AddWithValue("@Konu", konu);
                        cmd.Parameters.AddWithValue("@Mesaj", mesaj);

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                // Clear the form
                txtAdSoyad.Text = "";
                txtEposta.Text = "";
                txtKonu.Text = "";
                txtMesaj.Text = "";

                // Call JavaScript to show success message and trigger animation
                ScriptManager.RegisterStartupScript(this, GetType(), "successScript", "onSubmissionSuccess();", true);
            }
            catch (Exception ex)
            {
                lblMesaj.Text = "Mesaj gönderilirken hata: " + ex.Message;
            }
        }
    }
}