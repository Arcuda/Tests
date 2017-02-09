using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PWWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPWWService" in both code and config file together.
    [ServiceContract]
    public interface IPWWService
    {
        #region Customer
        [OperationContract]
        [WebInvoke(Method="GET", UriTemplate="allcust", ResponseFormat=WebMessageFormat.Json)]
        List<Customer> AllCust();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findcust/{inEmail}", ResponseFormat = WebMessageFormat.Json)]
        Customer FindCust(string inEmail);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "login/{inEmail}/{inPassword}", ResponseFormat = WebMessageFormat.Json)]
        Customer Login(string inEmail, string inPassword);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createcust", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool CreateCust(Customer inCustomer);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "editcust", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool EditCust(Customer inCustomer);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "deletecust", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool DeleteCust(Customer inCustomer);
        #endregion Customer

        #region Transfer
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "alltransf", ResponseFormat = WebMessageFormat.Json)]
        List<Transfer> AllTransf();
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "findtransf/{inClient}", ResponseFormat = WebMessageFormat.Json)]
        List<Transfer> FindTransf(string inClient);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createtransf", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool CreateTransf(Transfer inTransfer);
        #endregion Customer

    }
}
