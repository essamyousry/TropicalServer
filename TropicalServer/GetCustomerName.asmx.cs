using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TropicalServer
{
    /// <summary>
    /// Summary description for GetCustomerName
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetCustomerName : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> GetCustomerNames(DataTable dt1, string text)
        {
            System.Diagnostics.Debug.WriteLine(dt1.Rows[0][3].ToString());
            var results = from c in dt1.AsEnumerable()
                          where c.Field<string>("CustName").ToLower().StartsWith(text)
                          select c;

            List<string> custList = results.AsEnumerable()
                .Select(r => r.Field<string>("CustName")).Distinct().ToList();

            return custList;
        }
    }
}
