using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace PWWServ_Client.Models
{
    public class PWServiceClient
    {
        private string _BASE_URL = "http://localhost:57868/PWWService.svc/";

        public List<Customer> All()
        {
            try {
                WebClient _wb = new WebClient();
                string _json = _wb.DownloadString(_BASE_URL + "allcust");
                JavaScriptSerializer _jss = new JavaScriptSerializer();

                return _jss.Deserialize<List<Customer>>(_json);
            }
            catch(Exception ex) {

                return null;
            }
        }

        public Customer Find(string inEmail)
        {
            try
            {
                WebClient _wb = new WebClient();
                string _url = string.Format(_BASE_URL + "findcust/{0}", inEmail);
                string _json = _wb.DownloadString(_url);
                JavaScriptSerializer _jss = new JavaScriptSerializer();

                return _jss.Deserialize<Customer>(_json);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Transfer> FindCustTransfers(string inEmail)
        {
            try
            {
                WebClient _wb = new WebClient();
                string _url = string.Format(_BASE_URL + "findtransf/{0}", inEmail);
                string _json = _wb.DownloadString(_url);
                JavaScriptSerializer _jss = new JavaScriptSerializer();

                return _jss.Deserialize<List<Transfer>>(_json);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Customer Login(string inEmail, string inPassword)
        {
            try
            {
                WebClient _wb = new WebClient();
                string _url = string.Format(_BASE_URL + "login/{0}/{1}", inEmail, inPassword);
                string _json = _wb.DownloadString(_url);
                JavaScriptSerializer _jss = new JavaScriptSerializer();
                return _jss.Deserialize<Customer>(_json);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool CreateCustomer(Customer inCustomer)
        {
            try
            {
                DataContractJsonSerializer _ser = new DataContractJsonSerializer(typeof(Customer));
                MemoryStream _mem = new MemoryStream();
                _ser.WriteObject(_mem, inCustomer);
                string _data = Encoding.UTF8.GetString(_mem.ToArray(), 0, (int)_mem.Length);
                WebClient _wb = new WebClient();
                _wb.Headers["Content-type"] = "application/json";
                _wb.Encoding = Encoding.UTF8;
                _wb.UploadString(_BASE_URL + "createcust", "POST", _data);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool Edit(Customer inCustomer)
        {
            try
            {
                DataContractJsonSerializer _ser = new DataContractJsonSerializer(typeof(Customer));
                MemoryStream _mem = new MemoryStream();
                _ser.WriteObject(_mem, inCustomer);
                string _data = Encoding.UTF8.GetString(_mem.ToArray(), 0, (int)_mem.Length);
                WebClient _wb = new WebClient();
                _wb.Headers["Content-type"] = "application/json";
                _wb.Encoding = Encoding.UTF8;
                _wb.UploadString(_BASE_URL + "editcust", "PUT", _data);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool Delete(Customer inCustomer)
        {
            try
            {
                DataContractJsonSerializer _ser = new DataContractJsonSerializer(typeof(Customer));
                MemoryStream _mem = new MemoryStream();
                _ser.WriteObject(_mem, inCustomer);
                string _data = Encoding.UTF8.GetString(_mem.ToArray(), 0, (int)_mem.Length);
                WebClient _wb = new WebClient();
                _wb.Headers["Content-type"] = "application/json";
                _wb.Encoding = Encoding.UTF8;
                _wb.UploadString(_BASE_URL + "deletecust", "DELETE", _data);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public bool CreateTransfer(Transfer inTransfer)
        {
            try
            {
                Console.WriteLine("PWServiceClient.CreateTransfer.1");
                DataContractJsonSerializer _ser = new DataContractJsonSerializer(typeof(Transfer));
                MemoryStream _mem = new MemoryStream();
                _ser.WriteObject(_mem, inTransfer);
                string _data = Encoding.UTF8.GetString(_mem.ToArray(), 0, (int)_mem.Length);
                WebClient _wb = new WebClient();
                _wb.Headers["Content-type"] = "application/json";
                _wb.Encoding = Encoding.UTF8;
                _wb.UploadString(_BASE_URL + "createtransf", "POST", _data);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}