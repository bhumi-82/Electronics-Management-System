using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Caching;

namespace Electronicsmgtsystem_071
{
    public partial class registrationform : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
         

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("login.aspx");
            }
            lblTopEmail.Text = Session["Email"].ToString();

           
            if (!IsPostBack)
            {
                LoadType();
                LoadGrid();
                LoadSearchBrand();
            }
        }

        void LoadType()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from tblType", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlType.DataSource = dt;
            ddlType.DataTextField = "type";
            ddlType.DataValueField = "t_id";
            ddlType.DataBind();
            ddlType.Items.Insert(0, "--Select Type--");
        }

        void LoadSearchBrand()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter("select * from tblBrand", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlSearchBrand.DataSource = dt;
            ddlSearchBrand.DataTextField = "BrandName";
            ddlSearchBrand.DataValueField = "b_id";
            ddlSearchBrand.DataBind();

            ddlSearchBrand.Items.Insert(0, "All");
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);

            SqlCommand cmd =
                new SqlCommand("select * from tblBrand where t_id=" + ddlType.SelectedValue, con);
            cmd.Parameters.AddWithValue("@tid",ddlType.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlBrand.DataSource = dt;
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "b_id";
            ddlBrand.DataBind();
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            string color = "";
            if (rbt_blue.Checked) color = "Blue";
            if (rbt_black.Checked) color = "Black";
            if (rbt_gold.Checked) color = "Gold";
            if (rbt_rosegold.Checked) color = "Rose Gold";

            string accessories = "";
            if (chk_charger.Checked) accessories += "Charger";
            if (chk_headphone.Checked) accessories += "Headphones";
            if (chk_touchpen.Checked) accessories += "Touch Pen";
            if (chk_wirelessmouse.Checked) accessories += "Wireless Mouse";

            string imgpath = "";
            if (fu_image.HasFile)
            {
                string filename = fu_image.FileName;

                string path = Server.MapPath("~/Images/") + filename;
                fu_image.SaveAs(path);
                imgpath = "Images/" + filename;

            }
            SqlCommand cmd = new SqlCommand("insert into tblDevice values(@b, @m, @d, @p, @q, @c, @a,@i)", con);

            cmd.Parameters.AddWithValue("@b", ddlBrand.SelectedValue);
            cmd.Parameters.AddWithValue("@m", txtModel.Text);
            cmd.Parameters.AddWithValue("@d", txtDescription.Text);
            cmd.Parameters.AddWithValue("@p", txtPrice.Text);
            cmd.Parameters.AddWithValue("@q", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@c", color);
            cmd.Parameters.AddWithValue("@a", accessories);
            cmd.Parameters.AddWithValue("@i", imgpath);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadGrid();
        }

       

       void LoadGrid()
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter(
                @"select d.d_id,t.type,b.BrandName,d.model,d.description,d.price,d.quantity,d.color,d.accessories,d.image_path
                  from tblDevice d
                  inner join tblBrand b on d.b_id=b.b_id
                 inner join tblType t on b.t_id=t.t_id", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gv_display.DataSource = dt;
            gv_display.DataBind();
        }

        protected void gv_display_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow r = gv_display.SelectedRow;

            rbt_blue.Checked = false;
            rbt_black.Checked = false;
            rbt_gold.Checked = false;
            rbt_rosegold.Checked = false;

            chk_charger.Checked = false;
            chk_headphone.Checked = false;
            chk_touchpen.Checked = false;
            chk_wirelessmouse.Checked = false;

            d_Id.Value = r.Cells[1].Text.Trim();
            string brandName = r.Cells[2].Text.Trim();
            string typeid = "";

            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            string query = "select t_id,b_id from tblBrand where BrandName=@brandname";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@brandname",brandName);
            SqlDataReader dr = cmd.ExecuteReader();
            string b_Id = "";

            if (dr.Read())
            {
                typeid = dr["t_id"].ToString();
                b_Id = dr["b_id"].ToString();
            }
            con.Close();

            ddlType.SelectedValue = typeid;
            SqlConnection con2 = new SqlConnection(strcon);

            con2.Open();
            SqlCommand cmd2 = new SqlCommand("select * from tblBrand where t_id=@t_id",con2);
            cmd2.Parameters.AddWithValue("@t_id",typeid);
            ddlBrand.DataSource = cmd2.ExecuteReader();
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "b_id";
            ddlBrand.DataBind();
            con2.Close();

            ddlBrand.SelectedValue = b_Id;
            txtModel.Text = r.Cells[3].Text;
            txtDescription.Text = r.Cells[4].Text;
            txtPrice.Text = r.Cells[5].Text;
            txtQuantity.Text = r.Cells[6].Text;

            string color = r.Cells[7].Text;

            if (color == "Blue")
                rbt_blue.Checked = true;
            if (color == "Black")
                rbt_black.Checked = true;
            if (color == "Gold")
                rbt_gold.Checked = true;
            if (color == "Rose Gold")
                rbt_rosegold.Checked = true;

            string accessories = r.Cells[8].Text;
            if (accessories.Contains("Charger"))
                chk_charger.Checked = true;
            if (accessories.Contains("Headphones"))
                chk_headphone.Checked = true;
            if (accessories.Contains("Touch Pen"))
                chk_touchpen.Checked = true;
            if (accessories.Contains("Wireless Mouse"))
                chk_wirelessmouse.Checked = true;

        }

        protected void gv_display_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            int id = Convert.ToInt32(gv_display.DataKeys[e.RowIndex].Value);

            SqlCommand cmd =
                new SqlCommand("delete from tblDevice where d_id=@id", con);

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadGrid();
        }

        protected void ddlSearchBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearchBrand.SelectedIndex == 0)
            {
                LoadGrid();
                return;
            }
            SqlConnection con = new SqlConnection(strcon);
            SqlDataAdapter da = new SqlDataAdapter(
                @"select d.*, b.BrandName
                  from tblDevice d
                  join tblBrand b on d.b_id=b.b_id
                  where d.b_id=" + ddlSearchBrand.SelectedValue, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gv_display.DataSource = dt;
            gv_display.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlType.SelectedIndex = 0;
            ddlBrand.Items.Clear();
            ddlBrand.Items.Add("Select Brand");
            txtModel.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";

            rbt_black.Checked = false;
            rbt_blue.Checked = false;
            rbt_gold.Checked = false;
            rbt_rosegold.Checked = false;

            chk_charger.Checked = false;
            chk_headphone.Checked = false;
            chk_touchpen.Checked = false;
            chk_wirelessmouse.Checked = false;

        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);

            string color = "";
            if (rbt_blue.Checked) color = "Blue";
            if (rbt_black.Checked) color = "Black";
            if (rbt_gold.Checked) color = "Gold";
            if (rbt_rosegold.Checked) color = "Rose Gold";

            string accessories = "";
            if (chk_charger.Checked) accessories = "Charger";
            if (chk_headphone.Checked) accessories = "Headphones";
            if (chk_touchpen.Checked) accessories = "Touch Pen";
            if (chk_wirelessmouse.Checked) accessories = "Wireless Mouse";

            accessories = accessories.TrimEnd(',');

            string imgpath = "";
            if (fu_image.HasFile)
            {
                string filename = fu_image.FileName;
                string path = Server.MapPath("~/Images/") + filename;
                fu_image.SaveAs(path);
                imgpath = "Images/" + filename;
            }

            SqlCommand cmd = new SqlCommand("update tblDevice set b_id=@b, model=@m, description=@d, price=@p, quantity=@q,color= @c,accessories=@a,image_path=@i where d_id=@id",con);
            cmd.Parameters.AddWithValue("@b", ddlBrand.SelectedValue);
            cmd.Parameters.AddWithValue("@m", txtModel.Text);
            cmd.Parameters.AddWithValue("@d", txtDescription.Text);
            cmd.Parameters.AddWithValue("@p", txtPrice.Text);
            cmd.Parameters.AddWithValue("@q", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@c", color);
            cmd.Parameters.AddWithValue("@a", accessories);
            cmd.Parameters.AddWithValue("@i", imgpath);
            cmd.Parameters.AddWithValue("@id",d_Id.Value);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            LoadGrid();




       }
    }
}