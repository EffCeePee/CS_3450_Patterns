using System;

namespace ProxyPattern
{
    class SecureDB : IDatabase
    {
        
        private readonly IDatabase _sdb;
        private readonly IDatabase _udb;
        private string _uname;
        private string _pword;
        private bool _valid = false;

        public SecureDB(ref IDatabase db, ref IDatabase userdb)
        {
            _sdb = db;
            _udb = userdb;
            Console.Write("Enter Username: ");
            _uname = Console.ReadLine();
            Console.Write("Enter Password: ");
            _pword = Console.ReadLine();
            

        }

        private void Validate()
        {
            while (!_valid)
            {
                if (_uname != null && (_pword == _udb.Get(_uname)))
                {
                    _valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid Username or Password.");

                    Console.Write("Enter Username: ");
                    _uname = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    _pword = Console.ReadLine();
                }
            }

        }

        public string GetID()
        {
            if (!_valid)
            {
                Validate();
            }

            if (_valid)
                return _sdb.GetID();
            else
                return "invalid username and password";
        }


        public bool Exists(string key)
        {
            if (!_valid)
            {
                Validate();
            }
            if (_valid)
                return _sdb.Exists(key);
            else
                return false;
        }

        public string Get(string key)
        {
            if (!_valid)
            {
                Validate();
            }
            
            if(_valid)
                return _sdb.Get(key);
            else
                return "invalid username and password";
        }
    }
}
