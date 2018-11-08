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
    public static void MyFunction()
    {
        Response.Write("Bleg FROM C#");
    }
}