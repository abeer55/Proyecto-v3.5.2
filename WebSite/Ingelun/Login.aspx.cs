using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IngelunNegocio;


public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Request.Cookies["TPIPav"] != null)
            {

                loginControl.UserName = HttpContext.Current.Request.Cookies["TPIPav"]["UserName"].ToString();
                TextBox tb = (TextBox)loginControl.FindControl("Password");
                tb.Attributes["Value"] = HttpContext.Current.Request.Cookies["TPIPav"]["Password"].ToString();



            }

            //if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            //{
            //    loginControl.UserName = Request.Cookies["UserName"].Value;
            //    TextBox tb = (TextBox)loginControl.FindControl("Password");
            //    tb.Attributes["Value"] =Request.Cookies["Password"].Value;

            //}
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {

        //if (GestorUsuarios.VerificarUsuarioClave(Login1.UserName, Login1.Password))
        //{
        //    e.Authenticated = true;  // 
        //}
        //else
        //{
        //    e.Authenticated = false;

        //}

    }

    protected void login_Authenticate(object sender, AuthenticateEventArgs e)
    {

        if (GestorUsuarios.VerificarUsuarioClave(loginControl.UserName, loginControl.Password))
        {
            e.Authenticated = true;  // genera cookie de seguridad con datos del usuario (sin los roles)
            if (chkRecordarme.Checked)
            {
                //Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                //Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);

                HttpCookie cookie = new HttpCookie("TPIPav");
                cookie.Values.Add("UserName", loginControl.UserName);
                cookie.Values.Add("Password", loginControl.Password);

                cookie.Expires = DateTime.Now.AddDays(30);
                //graba la cookie en el cliente
                Response.Cookies.Add(cookie);

                //else
                //{
                //    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                //}
                //Response.Cookies["UserName"].Value = loginControl.UserName.Trim();
                //Response.Cookies["Password"].Value = loginControl.Password.Trim();
            }
        }
            else
            {
                e.Authenticated = false;

            }

        }
    }
