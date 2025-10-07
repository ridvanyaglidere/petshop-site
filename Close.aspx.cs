using System;
using System.Web.UI;

namespace petshopint2
{
    public partial class Close : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
        }
    }
}