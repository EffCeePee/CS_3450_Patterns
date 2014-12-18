using System.IO;


namespace ProxyPattern
{
    // purpsoe is to retrive values from a file
    class DataBase : IDatabase
    {
        private string _line;
        private string _fileName;
        private string key;
        private string value;
        private string[] _info;
        private char[] _params = { ' ' };
        private StreamReader _file;
        
        

        public DataBase(string _fileName)
        {
            this._fileName = _fileName;
            _file = new StreamReader(_fileName);

            
        }

        public string GetID()
        {
            return _fileName;
        }

        public bool Exists(string key)
        {
            _file = new StreamReader(_fileName);

            while ((_line = _file.ReadLine()) != null)
            {
                _info = _line.Split(_params, 2);
                value = _info[1];

                if (key == _info[0])
                {
                    return true;
                }

            }

            return false;
            //return _databases.ContainsKey(key);
        }

        public string Get(string key)
        {

            _file = new StreamReader(_fileName);

            while ((_line = _file.ReadLine()) != null)
            {
                _info = _line.Split(_params, 2);
                value = _info[1];

                if (key == _info[0])
                {
                    return _info[1];
                }

            }

            return "No such record: " + key;

        }
    }
}
