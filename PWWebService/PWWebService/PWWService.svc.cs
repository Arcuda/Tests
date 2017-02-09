using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace PWWebService
{
    public enum eHashTypes { None, MD5, SHA256, SHA512 };
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PWWService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PWWService.svc or PWWService.svc.cs at the Solution Explorer and start debugging.
    public class PWWService : IPWWService
    {
        /// <summary>
        /// Генерация Хэш-кода на основе входного значения, либо, если входное значение пустое, текущего времени.
        /// Если тип Хэш-кода не выбран, то выдаётся строка нулевого кода.
        /// </summary>
        /// <param name="inValue">входное значение</param>
        /// <param name="inHasType">тип генерируемого Хэш-кода</param>
        /// <returns></returns>
        private string GetHash(string inValue, eHashTypes inHasType)
        {
            string sVal = string.Empty;
            if (string.IsNullOrEmpty(inValue) || string.IsNullOrWhiteSpace(inValue))
                sVal = DateTime.Now.ToString(@"dMyyyyhhmmss");
            else
            {
                if (inHasType == eHashTypes.None)
                    return "00000000000000000000000000000000";

                sVal = inValue;
            }

            byte[] baValue = Encoding.UTF8.GetBytes(sVal);

            if (inHasType == eHashTypes.MD5)
            {
                MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
                baValue = x.ComputeHash(baValue);
                x.Dispose();
            }
            else
            {
                byte[] skey = new Byte[64];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                // The array is now filled with cryptographically strong random bytes.
                rng.GetBytes(skey);
                rng.Dispose();
                // Initialize the keyed hash object
                if (inHasType == eHashTypes.SHA256)
                {
                    HMACSHA256 hashKey = new HMACSHA256(skey);
                    byte[] hashValue = hashKey.ComputeHash(baValue);
                    hashKey.Dispose();
                }
                if (inHasType == eHashTypes.SHA512)
                {
                    HMACSHA512 hashKey = new HMACSHA512(skey);
                    baValue = hashKey.ComputeHash(baValue);
                    hashKey.Dispose();
                }
            }
            StringBuilder retValue = new StringBuilder();
            foreach (byte b in baValue)
            {
                retValue.Append(b.ToString("x2").ToUpper());
            }
            return retValue.ToString();
        }


        #region Customer
        public List<Customer> AllCust()
        {
            using(PWEntities pwe = new PWEntities()) {
                return pwe.CustomerEntities.Select(pe => new Customer {
                    Email = pe.email,
                    FirstName = pe.firstname,
                    SecondName = pe.secondname
                }).ToList<Customer>();
            };
        }
        public Customer FindCust(string inEmail)
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.CustomerEntities.Where(pe => pe.email.Equals(inEmail)).Select(pe => new Customer
                {
                    Email = pe.email,
                    FirstName = pe.firstname,
                    SecondName = pe.secondname
                }).FirstOrDefault<Customer>();
            };
        }
        public Customer Login(string inEmail, string inPassword)
        {
            using (PWEntities pwe = new PWEntities())
            {
                string _hesh = GetHash(inPassword, eHashTypes.MD5);
                Customer _ret = pwe.CustomerEntities.Where(pe => (pe.email.Equals(inEmail) && pe.passhesh.Equals(_hesh))).Select(pe => new Customer
                {
                    Email = pe.email,
                    FirstName = pe.firstname,
                    SecondName = pe.secondname,
                    PassHesh = pe.passhesh
                }).FirstOrDefault<Customer>();
                return _ret;
            };
        }
        public bool CreateCust(Customer inCustomer)
        {
            using (PWEntities pwe = new PWEntities())
            {
                try {
                    CustomerEntity ce = new CustomerEntity();
                    ce.email = inCustomer.Email;
                    ce.firstname = inCustomer.FirstName;
                    ce.secondname = inCustomer.SecondName;
                    ce.passhesh = GetHash(inCustomer.PassHesh, eHashTypes.MD5);
                    pwe.CustomerEntities.Add(ce);
                    pwe.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        public bool EditCust(Customer inCustomer)
        {
            using (PWEntities pwe = new PWEntities())
            {
                try
                {
                    CustomerEntity ce = pwe.CustomerEntities.Single(pe => pe.email.Equals(inCustomer.Email));
                    ce.firstname = inCustomer.FirstName;
                    ce.secondname = inCustomer.SecondName;
                    pwe.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        public bool DeleteCust(Customer inCustomer)
        {
            using (PWEntities pwe = new PWEntities())
            {
                try
                {
                    CustomerEntity ce = pwe.CustomerEntities.Single(pe => pe.email.Equals(inCustomer.Email));
                    pwe.CustomerEntities.Remove(ce);
                    pwe.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        #endregion Customer

        #region Transfer
        public List<Transfer> AllTransf()
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.TransferEntities.Select(pe => new Transfer
                {
                    Id = (Int32)(pe.id)
                    , WhenDT = pe.whendt
                    , Summa = (double)(pe.summa)
                    , Amount = (double)(pe.amount)
                    , Client = pe.client
                    , Notes = pe.notes
                    , Source = (Int32)((pe.source == null) ? -1 : pe.source)
                }).ToList<Transfer>();
            };
        }
        public List<Transfer> FindTransf(string inClient)
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.TransferEntities.Where(pe => pe.client.Equals(inClient)).Select(pe => new Transfer
                {
                    Id = (Int32)(pe.id),
                    WhenDT = pe.whendt,
                    Summa = (Double)(pe.summa),
                    Amount = (Double)(pe.amount),
                    Client = pe.client,
                    Notes = pe.notes,
                    Source = (Int32)((pe.source == null) ? -1 : pe.source)
                }).ToList<Transfer>();
            };
        }
        public bool CreateTransf(Transfer inTransfer)
        {
            using (PWEntities pwe = new PWEntities())
            {
                try
                {
                    TransferEntity te = new TransferEntity();
                    te.id = inTransfer.Id;
                    te.whendt = inTransfer.WhenDT;
                    te.summa = (decimal)(inTransfer.Summa);
                    te.amount = (decimal)(inTransfer.Amount);
                    te.client = inTransfer.Client;
                    te.notes = inTransfer.Notes;
                    te.source = (Int64)(inTransfer.Source);

                    pwe.TransferEntities.Add(te);
                    pwe.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        #endregion Customer

        #region Message
        List<Message> AllMsg()
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.MessageEntities.Select(me => new Message
                {
                    WhenDT = me.whendt,
                    Owner = me.owner,
                    Text = me.message1,
                    StackTrace = me.stacktrace
                }).ToList<Message>();
            };
        }
        List<Message> FindMsg(string inKey)
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.MessageEntities.Where(me => me.owner.Equals(inKey)).Select(me => new Message
                {
                    WhenDT = me.whendt,
                    Owner = me.owner,
                    Text = me.message1,
                    StackTrace = me.stacktrace
                }).ToList<Message>();
            };
        }
        public bool CreateMsg(Message inMessage)
        {
            using (PWEntities pwe = new PWEntities())
            {
                try
                {
                    MessageEntity me = new MessageEntity();
                    me.whendt = inMessage.WhenDT;
                    me.owner = inMessage.Owner;
                    me.message1 = inMessage.Text;
                    me.stacktrace = inMessage.StackTrace;

                    pwe.MessageEntities.Add(me);
                    pwe.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        #endregion Message

        #region Event
        public List<Event> AllEvnt()
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.EventEntities.Select(ee => new Event
                {
                    EventDT = ee.eventdt,
                    Process = ee.process,
                    Client = ee.client,
                    Transfer = Convert.ToInt32(ee.transfer.Value)
                }).ToList<Event>();
            };
        }
        public List<Event> FindEvnt(string inKey)
        {
            using (PWEntities pwe = new PWEntities())
            {
                return pwe.EventEntities.Where(ee => ee.client.Equals(inKey)).Select(ee => new Event
                {
                    EventDT = ee.eventdt,
                    Process = ee.process,
                    Client = ee.client,
                    Transfer = Convert.ToInt32(ee.transfer.Value)
                }).ToList<Event>();
            };
        }
        public bool CreateEvnt(Event inEvent)
        {
            using (PWEntities pwe = new PWEntities())
            {
                try
                {
                    EventEntity ee = new EventEntity();
                    ee.eventdt = inEvent.EventDT;
                    ee.process = inEvent.Process;
                    ee.client = inEvent.Client;
                    ee.transfer = Convert.ToUInt32(inEvent.Transfer);

                    pwe.EventEntities.Add(ee);
                    pwe.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        #endregion Event
    }
}
