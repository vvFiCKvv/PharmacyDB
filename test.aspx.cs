
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace PhamacyDB
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
            Div2.InnerText = "aaaaaaaaaaaa" + DateTime.Now.ToString();
            Label3.Text = DateTime.Now.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
            Div2.InnerText = "aaaaaaaaaaaa";
            Label3.Text = DateTime.Now.ToString();
        }
    }
}

