using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Electronicsmgtsystem_071
{
    public partial class login : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        protected void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);

            SqlCommand cmd = new SqlCommand("SELECT Name FROM tblCustomer WHERE email=@email AND password=@pass",con);
            cmd.Parameters.AddWithValue("@email", txt_email.Text);
            cmd.Parameters.AddWithValue("@pass", txt_pass.Text);

            con.Open();
            object result = cmd.ExecuteScalar();
            if (result != null) 
            {
                Session["Email"] = txt_email.Text;
                Session["Name"] = result.ToString();

                Response.Redirect("registrationform.aspx");
            }
            else
            {
                lblMsg.Text = "Invalid email or password.";
            }
            con.Close();

        }
    }
}