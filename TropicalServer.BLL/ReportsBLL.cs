using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TropicalServer.DAL;

namespace TropicalServer.BLL
{
    public class ReportsBLL
    {
        public bool GetUserAuthentication(string username, string password)
        {
            return (new ReportsDAL().GetUserAuthentication(username, password));
        }
        public DataSet GetProductByProductCategory_BLL(string newItemDescription)
        {
            return (new ReportsDAL().GetProductByProductCategory_DAL(newItemDescription));
        }

        public bool ValidateEmail(string email)
        {
            return (new ReportsDAL().ValidateEmail(email));
        }

        public DataSet GetCustSalesRepNumber_BLL(int newCustSaleRepNum)
        {
            return (new ReportsDAL().GetCustSalesRepNumber_DAL(newCustSaleRepNum));
        }

        public DataSet GetUsersSetting_BLL()
        {
            return (new ReportsDAL().GetUsersSetting_DAL());
        }

        public DataSet GetCustomersSetting_BLL()
        {
            return (new ReportsDAL().GetCustomersSetting_DAL());
        }

        public DataSet GetPriceGroupSetting_BLL()
        {
            return (new ReportsDAL().GetPriceGroupSetting_DAL());
        }

        public DataSet GetRouteInfo_BLL(int newRouteID)
        {
            return (new ReportsDAL().GetRouteInfo_DAL(newRouteID));
        }

        public DataSet GetOrdersByTimePeriod_BLL(string OrderDate)
        {
            return (new ReportsDAL().GetOrdersByTimePeriod(OrderDate));
        }

        public DataSet GetOrdersByTimePeriodAndCustID(string OrderDate, int CustomerNumber)
        {
            return (new ReportsDAL().GetOrdersByTimePeriodAndCustID(OrderDate, CustomerNumber));
        }

        public DataSet GetOrdersByTimePeriodAndCustName(string OrderDate, string CustomerName)
        {
            return (new ReportsDAL().GetOrdersByTimePeriodAndCustName(OrderDate, CustomerName));
        }

        public void UpdateOrder(string text1, string text2, string text3, string text4, string text5, string text6, string v)
        {
            ReportsDAL DAL = new ReportsDAL();
            DAL.UpdateOrder(text1, text2, text3, text4, text5, text6, v);
        }

        public void DeleteOrder(string orderid)
        {
            ReportsDAL DAL = new ReportsDAL();
            DAL.DeleteOrder(orderid);
        }

        public string GetOrderDetails(string orderid)
        {
            return (new ReportsDAL().GetOrderDetails(orderid));
        }
    }
}
