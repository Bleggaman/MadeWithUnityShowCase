using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

// This class was called _default
public class UserWebRequest : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void btnCallCSFunction_Click(object sender, EventArgs e)
    {
        MyFunction();
    }

    private void MyFunction()
    {
        //write your stuff here
        Response.Write("Bleg");
    }
}