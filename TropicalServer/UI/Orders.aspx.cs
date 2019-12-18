using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TropicalServer.BLL;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace TropicalServer.UI
{
    public partial class Orders : System.Web.UI.Page
    {
        readonly ReportsBLL BLL = new ReportsBLL();
        static DataTable dt1 = new DataTable();
        static DataTable dt2 = new DataTable();
        static DataSet ds = new DataSet();
        static string orderDate;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //DisplayOrders("Today");
            //grid.DataBind();
        }

        protected void DisplayOrders(string orderdate)
        {
            orderDate = orderdate;
            ds = BLL.GetOrdersByTimePeriod_BLL(orderdate);
            dt1 = ds.Tables[0];
            //System.Diagnostics.Debug.WriteLine(dt1.Columns[6].ColumnName);
      
            grid.DataSource = ds;
            grid.DataBind();
        }

        protected void DisplayOrders(string orderdate, int customerNumber)
        {
            ds = BLL.GetOrdersByTimePeriodAndCustID(orderdate, customerNumber);
            dt1 = ds.Tables[0];

            grid.DataSource = ds;
            grid.DataBind();
        }

        protected void DisplayOrders(string orderdate, string custName)
        {
            ds = BLL.GetOrdersByTimePeriodAndCustName(orderdate, custName);
            dt1 = ds.Tables[0];

            grid.DataSource = ds;
            grid.DataBind();
            
        }

        protected void OrderDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(criteria1.SelectedValue);
            DisplayOrders(criteria1.SelectedValue);
        }
        
        [WebMethod]
        public static List<int> GetByCustomerID(string prefixText, int count)
        {
            //System.Diagnostics.Debug.WriteLine(dt.Columns[2]);
            //System.Diagnostics.Debug.WriteLine(dt.Rows[3][0].ToString());
            
            var results = from c in dt1.AsEnumerable()
                          where c.Field<int>("OrderCustomerNumber").ToString().StartsWith(prefixText)
                          select c;

            List<int> custList = results.AsEnumerable()
                .Select(r => r.Field<int>("OrderCustomerNumber")).Distinct().ToList();

            return custList;
        }

        [WebMethod]
        public static List<string> GetByCustomerName(string prefixText, int count){

            GetCustomerName gcn = new GetCustomerName();
            List<string> custList = gcn.GetCustomerNames(dt1, prefixText);
            //System.Diagnostics.Debug.WriteLine(dt1.Rows.Count);
            return custList;
        }
        
        public void DisplaySelectedCustomerID(object sender, EventArgs e)
        {
            string custID = customerid.Text;
            int number;
            bool success = Int32.TryParse(custID, out number);

            if (success && number > 99) DisplayOrders(orderDate, number);
     
        }

        public void DisplaySelectedCustomerName(object sender, EventArgs e)
        {
            string custName = customername.Text;
            if(custName != null && custName != "")
                DisplayOrders(orderDate, custName);
        }

        protected void ViewDetails(object sender, EventArgs e)
        {
            
            
        }

        protected void EditDetails(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            
            grid.EditIndex = e.NewEditIndex;
            grid.DataSource = dt1;
            grid.DataBind();
        }

        protected void CancelEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            grid.EditIndex = -1;
            grid.DataSource = dt1;
            grid.DataBind();
        }

        protected void UpdateEdit(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = grid.Rows[e.RowIndex];
            System.Diagnostics.Debug.WriteLine(dt1.Rows[row.DataItemIndex]["OrderID"].ToString());
            dt1.Rows[row.DataItemIndex]["OrderTrackingNumber"] = ((TextBox)(row.Cells[0].Controls[0])).Text;
            dt1.Rows[row.DataItemIndex]["OrderDate"] = ((TextBox)(row.Cells[1].Controls[0])).Text;
            dt1.Rows[row.DataItemIndex]["OrderCustomerNumber"] = ((TextBox)(row.Cells[2].Controls[0])).Text;
            dt1.Rows[row.DataItemIndex]["CustName"] = ((TextBox)(row.Cells[3].Controls[0])).Text;
            dt1.Rows[row.DataItemIndex]["CustOfficeAddress1"] = ((TextBox)(row.Cells[4].Controls[0])).Text;
            dt1.Rows[row.DataItemIndex]["OrderRouteNumber"] = ((TextBox)(row.Cells[5].Controls[0])).Text;

            grid.EditIndex = -1;

            
            BLL.UpdateOrder(
                ((TextBox)(row.Cells[0].Controls[0])).Text,
                ((TextBox)(row.Cells[1].Controls[0])).Text,
                ((TextBox)(row.Cells[2].Controls[0])).Text,
                ((TextBox)(row.Cells[3].Controls[0])).Text,
                ((TextBox)(row.Cells[4].Controls[0])).Text,
                ((TextBox)(row.Cells[5].Controls[0])).Text,
                dt1.Rows[row.DataItemIndex]["OrderID"].ToString()
            );


            grid.DataSource = dt1;
            grid.DataBind();

        }

        protected void DeleteDetails(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = grid.Rows[e.RowIndex];
            string orderid = dt1.Rows[row.DataItemIndex]["OrderID"].ToString();
            
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                BLL.DeleteOrder(orderid);
                DisplayOrders(orderDate);
            }
            else if (dialogResult == DialogResult.No)
            {
                DisplayOrders(orderDate);
            }
        }

        protected void ViewDetails(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grid.Rows[e.NewSelectedIndex];
            string orderid = dt1.Rows[row.DataItemIndex]["OrderID"].ToString();

            string message = BLL.GetOrderDetails(orderid);

            DialogResult dialogResult = MessageBox.Show(message, "View", MessageBoxButtons.OK);
            if (dialogResult == DialogResult.OK)
            {
                
            }
            System.Diagnostics.Debug.WriteLine(orderid);
        }
    }
}