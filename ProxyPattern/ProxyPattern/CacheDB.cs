

using System.ComponentModel.Design.Serialization;
using System.Xml.Schema;

namespace ProxyPattern
{
    class CacheDB : IDatabase
    {
        private const int COLUMN = 5;
        private const int ROW = 2; 
        private IDatabase cdb;
        private int index = 0;
        private int used = 0;
        private int current = 0;
        private string[,] cache = new string[COLUMN, ROW];
        private string value;
        
        

        public CacheDB(ref IDatabase sdb)
        {
            cdb = sdb;
        }

        public string GetID()
        {
            return cdb.GetID();
        }

        public bool Exists(string key)
        {
         
            return cdb.Exists(key);
        }

        public bool Incache(string key)
        {
            for (int i = 0; i < COLUMN; i++)
            {
                if (cache[i, 0] == key)
                {
                    index = i;
                    return true;
                }
            }

            return false;

        }

        public void Addcache(string key, string value)
        {

            if (used < COLUMN)
            {
                cache[used, 0] = key;
                cache[used, 1] = value;
            }
            else
            {
                used = 0;
                cache[used, 0] = key;
                cache[used, 1] = value;
            }

            used++;
        }

        public string Get(string key)
        {
            if (Incache(key))
            {
                return "retrieving " + '"' + cache[index, 0] + '"' + "from cache";
            }
            else
            {
                value = cdb.Get(key);
                if (value.Contains("No such record:"))
                {
                    return value;
                }
                else
                {
                    Addcache(key, value);
                    return cdb.Get(key);
                }
                
            }
            
        }
    }
}
