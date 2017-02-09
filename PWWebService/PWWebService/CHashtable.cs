using System.Collections;
using System.Collections.Generic;

namespace PWWebService
{
    public class CHashtable
    {
        private Hashtable m_Data = null;

        public CHashtable()
        {
            m_Data = new Hashtable();
        }
        public CHashtable(Hashtable inData)
        {
            m_Data = new Hashtable();
            if (inData == null)
                return;

            foreach (DictionaryEntry _itm in inData)
                m_Data.Add(_itm.Key, _itm.Value);
        }
        public CHashtable(object[] inData)
        {
            m_Data = new Hashtable();
            if (inData == null)
                return;

            for (int _i = 0; _i < inData.Length; _i++)
                m_Data.Add(_i, inData[_i]);
        }

        public int Count { get { return m_Data.Count; } }
        public bool IsExist(object inKey)
        {
            return m_Data.ContainsKey(inKey);
        }
        /// <summary>
        /// возвращает, или параметр по ключу, или, 
        /// в случает отсутствия такового, обект, указанный во втором входном параметре
        /// </summary>
        /// <param name="Key">ключевое название параметра</param>
        /// <param name="def">значение, возвращаемое при отсутствии искомого ключа</param>
        /// <returns></returns>
        public object this[object inKey, object def]
        {
            get
            {
                if (m_Data.ContainsKey(inKey))
                    return m_Data[inKey];
                return def;
            }
        }
        public object this[object inKey]
        {
            get
            {
                if (m_Data.ContainsKey(inKey))
                    return m_Data[inKey];
                return null;
            }

            set
            {
                if (m_Data.ContainsKey(inKey))
                    m_Data[inKey] = value;
                else
                    m_Data.Add(inKey, value);
            }
        }

        public bool Add(object inKey, object inValue)
        {
            if (m_Data.ContainsKey(inKey))
                return false;

            m_Data.Add(inKey, inValue);
            return true;
        }

        public bool Remove(object inKey)
        {
            if (m_Data.ContainsKey(inKey))
            {
                m_Data.Remove(inKey);
                return true;
            }
            return false;
        }
        public List<object> Values
        {
            get
            {
                List<object> _ret = new List<object>();
                if (m_Data == null)
                    return _ret;

                foreach (object _itm in m_Data.Values)
                    _ret.Add(_itm);

                return _ret;
            }
        }
        public List<object> Keys
        {
            get
            {
                List<object> _ret = new List<object>();
                if (m_Data == null)
                    return _ret;

                foreach (object _itm in m_Data.Keys)
                    _ret.Add(_itm);

                return _ret;
            }
        }


    }
}