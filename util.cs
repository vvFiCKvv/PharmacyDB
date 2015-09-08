using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO.Compression;
using System.IO;
static class Util
{
    public static class Style
    {
        public static void setGrindviewStyle(ref GridView grd)
        {
            grd.AlternatingRowStyle.CssClass = "alt1";
            grd.GridLines = GridLines.None;
        }
    }
    public static bool enableSEO = false;//true;
    public static int resultSize = 15;
    public static int miniResultSize = 5;
    public static string removeSpaces(string str)
    {
        return str.Trim(" ".ToCharArray());
    }
    //(This Function for Converting data into hex format
    public static string stringToHex(string Data)
    {
        byte[] enc =
        ASCIIEncoding.UTF8.GetBytes(Data);
        StringBuilder sBuffer = new StringBuilder();
        for (int i = 0; i < enc.Length; i++)
        {
            sBuffer.Append(enc[i].ToString("x"));
        }
        return sBuffer.ToString().ToUpper();
    }

    public static String enCode(String str)
    {
        
        return hexToString(str);
    }
    public static String deCode(String str)
    {
        return hexToString(str);
    }
    //(This Function for Converting hex into data )

    public static string hexToString(string hexString)
    {

    byte [] enc = new byte[hexString.Length/2];        
    for (int i = 0; i <= hexString.Length - 2; i += 2)
    {
        byte b = byte.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
        enc[i/2] = b;
    }
    return ASCIIEncoding.UTF8.GetString(enc);
   

    }
    public static string Compress(string value)
    {

        return stringToHex(value);
    }

    public static string Decompress(string valueHex)
    {
        return hexToString(valueHex);
    }
    public static class Menu
    {

        public static String[] adminMenuName = { "Edit (Sub)Categories","Edit Pharmacy"};
        public static String[] adminMenuFile = { "/createCategory.aspx", "/createPharmacy.aspx"};

        public static String[] subMenuName = { "Λίστα Φαρμάκων", "Κατηγορίες Φαρμάκων", "Έλεγχος αλληλεπιδράσεων" };
        public static String[] subMenuFile = { "/viewPharmacies.aspx", "/viewCategories.aspx", "/checkInteractions.aspx" };
        public static String[] footerMenuName = { "About", "Privacy","Help" ,"Statistics"};
        public static String[] footerMenuFile = { "/about.aspx",  "/privacy.aspx", "/help.aspx", "/statistics.aspx" };
        public static String createFooterMenu()
        {
            String res = "<hr /><table align=\"right\"><tr>";
            for (int i = 0; i < footerMenuName.Length; i++)
            {
                String tmpItem = footerMenuName[i];
                res += "<td><a href=\"" + footerMenuFile[i] + "\">";
                res +=  tmpItem;
                res += "</a></td>";
            }
            res += "</tr></table>";
            return res;
        }
        public static String createMenu(String currPage)
        {
            String res = "";
            res += "<div><table cellspacing=\"0\" ><tr>";
            res += "<td  width=\"10%\"><img alt=\"\" src=\"/images/Pharmacy_Logo_for_web.gif\" height=\"100\" /></td><td>";
            res += "<table cellspacing=\"0\"  width=\"90%\" ><tr>";
            res += "<td><img alt=\"\" src=\"/images/logo.gif\" height=\"50\" /></td></tr><tr>";
            res += "<td><table  cellspacing=\"0\" width=\"100%\"><tr>";
            for (int i = 0; i < subMenuName.Length; i++)
            {
                if(i%3==0 && i!=0)
                {
                    res += "</tr><tr>";
                }
                String tmpItem = subMenuName[i];
                res += "<td width=\"1000\"><a href=\"" + subMenuFile[i] + "\"><p>";
                if (currPage.Contains(subMenuFile[i]))
                {
                    res += "<b>" + tmpItem + "</b>";
                }
                else
                {

                    res += tmpItem;
                }
                res += "</p></a></td>";
            }
            res += "</tr></table>";
            res += "</td></tr></table>";
            res += "</td></tr></table></div>";
            return res;
        }
        public static String createAdminMenu(String currPage)
        {
            String res = "";
            res += "<div><table cellspacing=\"0\" ><tr>";
            res += "<td  width=\"10%\"><img alt=\"\" src=\"/images/Pharmacy_Logo_for_web.gif\" height=\"100\" /></td><td>";
            res += "<table cellspacing=\"0\"  width=\"90%\" ><tr>";
            res += "<td><img alt=\"\" src=\"/images/logo.gif\" height=\"50\" /></td></tr><tr>";
            res += "<td><table  cellspacing=\"0\" width=\"100%\"><tr>";
            for (int i = 0; i < adminMenuName.Length; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    res += "</tr><tr>";
                }
                String tmpItem = adminMenuName[i];
                res += "<td width=\"1000\"><a href=\"" + adminMenuFile[i] + "\"><p>";
                if (currPage.Contains(adminMenuFile[i]))
                {
                    res += "<b>" + tmpItem + "</b>";
                }
                else
                {

                    res += tmpItem;
                }
                res += "</p></a></td>";
            }
            res += "</tr></table>";
            res += "</td></tr></table>";
            res += "</td></tr></table></div>";
            return res;
        }
       
    }
    public static class UrlRewriting
    {
        
        public static string encodeUrl(string input)
        {
            if (enableSEO == false)
                return input;
            String url = removeSpaces(input);
            String[] urlPart = url.Split("?".ToCharArray());
            // directly serve any non-aspx pages
            if (urlPart.Length < 2)
                return url;
            if (!urlPart[0].EndsWith(".aspx"))
                return url;
            
            String [] args = urlPart[1].Split("&".ToCharArray());
            String res = "";
            foreach (String arg in args)
            {
                String []tmp = arg.Split("=".ToCharArray());
                if (tmp[1] != "")
                {
                    res += tmp[0] + "/" + tmp[1] + "/";
                }
            }
            res += urlPart[0];
            return "/" +  Uri.EscapeUriString(res);
        }
        public static string decodeUrl(string urlU)
        {
            String url = Uri.UnescapeDataString(urlU);
            if (enableSEO == false)
                return url;
            // directly serve any non-aspx pages
            if (!url.EndsWith(".aspx")) return url;
            String[] urlPart = url.Split("/".ToCharArray());
            int cnt = urlPart.Length;
            String newUrl;
            newUrl = urlPart[0] + "/" + urlPart[cnt - 1] + "?";
            if (cnt < 2)
                return url;
            for (int i = 1; i < cnt - 2; i++)
            {
                newUrl += urlPart[i++] + "=" + urlPart[i] + "&";
            }
            return newUrl.Substring(0, newUrl.Length - 1);
        }

    }
}