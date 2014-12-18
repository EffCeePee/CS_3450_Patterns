using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            string _line;
            StreamReader _file = new StreamReader("file.txt");
            List<string> _commands = new List<string>();
            int i = 0;
            char [] _params = {' '};
            List<Database> _DBS = new List<Database>();
            Database currentDatabase = null;
            string[] _info;
            string _id = null;
            string _key = null;
            string _value = null;
            string _cmd = null;
            string _dest = "backup.txt";
            List<string> _successComands = new List<string>();
            bool _success = false;


            while ((_line = _file.ReadLine()) != null)
            {
                _commands.Add(_line);

            }


   
            


            for (int j = 0; j < _commands.Count; j++)
            {

                _info = _commands[j].Split(_params, 4);
                _cmd = _info[0];
                _success = false;


                switch(_cmd)
                { 
                    case "A":

                        _id = _info[1];
                        _key = _info[2];
                        _value = _info[3];

                        currentDatabase = new helper().getCurrent(_id, ref _DBS, ref _info, ref currentDatabase);

                        AddCommand _add = new AddCommand(currentDatabase, _key, _value);
                        CmdListInvoker _cmdAdd = new CmdListInvoker(_add);
                       _success = _cmdAdd.doCommand();

                       if (_success)
                           _successComands.Add(_commands[j]);
     

                        break;
                        

                    case "R":
                        _id = _info[1];
                        _key = _info[2];
                        

                        currentDatabase = new helper().getCurrent(_id, ref _DBS, ref _info, ref currentDatabase);
                        

                        RemoveCommand _remove = new RemoveCommand(currentDatabase, _key);
                        CmdListInvoker _cmdRemove = new CmdListInvoker(_remove);
                        _success = _cmdRemove.doCommand();
                        if (_success)
                            _successComands.Add(_commands[j]);

                        break;

                    case "U":

                        _id = _info[1];
                        _key = _info[2];
                        _value = _info[3];

                        currentDatabase = new helper().getCurrent(_id, ref _DBS, ref _info, ref currentDatabase);

                        
                        UpdateCommand _update = new UpdateCommand(currentDatabase, _key, _value);
                        CmdListInvoker _cmdUpdate = new CmdListInvoker(_update);
                        _success =  _cmdUpdate.doCommand();
                        if (_success)
                            _successComands.Add(_commands[j]);

                        break;

                    default:
                        Console.WriteLine("Couldn't Execute Command");
                        break;

                }//end switch

            }// end for loop


            for (int b = 0; b < _DBS.Count; b++)
            {
                currentDatabase = _DBS[b];

                BackupCommand _backup = new BackupCommand(currentDatabase, _dest);
                CmdListInvoker _cmdBackup = new CmdListInvoker(_backup);
                _cmdBackup.doCommand();

            }



            for (int z = 0; z < _DBS.Count; z++)
            {
                Dictionary<string, string> _tmp = new Dictionary<string, string>();

                currentDatabase = _DBS[z];
                {
                    _tmp = currentDatabase.getDic();
                }

                Console.WriteLine("Database " + _DBS[z].getID() + ":");


                foreach (KeyValuePair<string, string> keyvalue in _tmp)
                {
                    Console.WriteLine(keyvalue.Key + " " + keyvalue.Value);
                }



            }


            Console.WriteLine("Beggining Undo:");

            Console.ReadLine();    





            for (int j = 0; j < _successComands.Count; j++)
            {

                _info = _successComands[j].Split(_params, 4);
                _cmd = _info[0];
                _success = false;


                switch (_cmd)
                {
                    case "A":

                        _id = _info[1];
                        _key = _info[2];
                        _value = _info[3];

                        currentDatabase = new helper().getCurrent(_id, ref _DBS, ref _info, ref currentDatabase);

                        AddCommand _add = new AddCommand(currentDatabase, _key, _value);
                        CmdListInvoker _cmdAdd = new CmdListInvoker(_add);
                        _cmdAdd.undo();

                     


                        break;


                    case "R":
                        _id = _info[1];
                        _key = _info[2];


                        currentDatabase = new helper().getCurrent(_id, ref _DBS, ref _info, ref currentDatabase);


                        RemoveCommand _remove = new RemoveCommand(currentDatabase, _key);
                        CmdListInvoker _cmdRemove = new CmdListInvoker(_remove);
                        _cmdRemove.undo();
         

                        break;

                    case "U":

                        _id = _info[1];
                        _key = _info[2];
                        _value = _info[3];

                        currentDatabase = new helper().getCurrent(_id, ref _DBS, ref _info, ref currentDatabase);


                        UpdateCommand _update = new UpdateCommand(currentDatabase, _key, _value);
                        CmdListInvoker _cmdUpdate = new CmdListInvoker(_update);
                        _cmdUpdate.undo();
                

                        break;

                    default:
                        Console.WriteLine("Couldn't Execute Command");
                        break;

                }//end switch

            }// end for loop


            for (int z = 0; z < _DBS.Count; z++)
            {
                Dictionary<string, string> _tmp = new Dictionary<string, string>();

                currentDatabase = _DBS[z];
                {
                    _tmp = currentDatabase.getDic();
                }

                Console.WriteLine("Database " + _DBS[z].getID() + ":");


                foreach (KeyValuePair<string, string> keyvalue in _tmp)
                {
                    Console.WriteLine(keyvalue.Key + " " + keyvalue.Value);
                }



            }

            Console.ReadLine();

        } // end main   


    } // end class 



    // helper class to get current database
    public class helper
    {
        // helper function
        public Database getCurrent(string _id, ref List<Database> _DBS, ref string[] _info, ref Database _cdb)
        {
            bool _found = false;
            Database currentDatabase = _cdb;


            _found = false;

            if (_DBS.Count == 0)
            {
                currentDatabase = new Database(_id);
                _DBS.Add(currentDatabase);
            }

            for (int k = 0; k < _DBS.Count && _found == false; k++)
            {
                if (_DBS[k].getID() == _info[1])
                {
                    currentDatabase = _DBS[k];
                    _found = true;
                }
            }

            if (_found == false && _info[0] == "A")
            {
                currentDatabase = new Database(_id);
                _DBS.Add(currentDatabase);
                _found = true;
            }

            return currentDatabase;
        }

    }

    public class CmdListInvoker //invoker has methods that call the correct execute commands.
    {
        ICommand _cmd;
        bool success = false;

        public CmdListInvoker(ICommand _cmd)
        {
            this._cmd = _cmd;
        }

        public bool doCommand()
        {
          return success = _cmd.execute();
        }

        public void undo()
        {
           _cmd.undo();
        }
    }


    public interface ICommand //command interface
    {
        bool execute();
        void undo();
    }

    public class AddCommand : ICommand //concrete command
    {
        bool success = false;
        Recievers db;
        string key;
        string value;
        public AddCommand(Database db, string key, string value)
        {
            this.key = key;
            this.value = value;
            this.db = db;
        }

        public bool execute()
        {
            success = db.add(key,value);
            return success;
        }

        public void undo()
        {
            db.remove(key);
        }
    }

    public class RemoveCommand : ICommand
    {
        bool success = false;
        Recievers db;
        string key;
        string value;
        public RemoveCommand(Database db, string key)
        {
            this.key = key;
            value = db.getVal(key);
            this.db = db;
        }

        public bool execute()
        {
            
          return success = db.remove(key);
        }

        public void undo()
        {
            db.add(key,value);
        }

    }


    public class UpdateCommand : ICommand
    {
        bool success = false;
        string key;
        string value;
        Recievers db;
        public UpdateCommand(Database db, string key, string value)
        {
            this.key = key;
            this.value = value;
            this.db = db;
        }

        public bool execute()
        {
           return success = db.update(key, value);
        }

        public void undo()
        {
            db.undoUpdate(key);
        }
    }

    public class BackupCommand : ICommand
    {
        bool success = false;
        string _dest;
        Recievers db;
        public BackupCommand(Database db, string _dest)
        {
            this._dest = _dest; 
            this.db = db;
        }

        public bool execute()
        {
            return success = db.backup(_dest);
            
        }

        public void undo()
        {
            Console.WriteLine("Can't unwrite a file");
        }
    }


    public interface Recievers
    {
        bool add(string key, string value);
        bool remove(string key);
        bool update(string key, string value);
        bool backup(string dest);
        void undoUpdate(string key);
        string getVal(string Key);
        Dictionary<string,string> getDic();

    }

    public class Database : Recievers//reciever contains the actual code to execute which is called by the invoker.
    {
        string id;
        string key;
        string value;
        bool success = false;
        Dictionary<string, string> _dic = new Dictionary<string,string>();
        string[] undoUpdates = new string[1000];
        int countupdates = 0;

        public Database(string id)
        {
            this.id = id;
        }

        public Dictionary<string,string> getDic()
        {
            return _dic;
        }

        public string getID()
        {
            return id;
        }

             
        public bool add(string k, string v)
        {
            key = k;
            value = v;
            if (!_dic.ContainsKey(k))
            {
                _dic.Add(key, value);
                return true;
            }
            else
            {
               return false;
            }
        }

        public bool remove(string k)
        {
            key = k;
            success = _dic.Remove(key);
            
            if(success)
            {
                return true;
            }
            else
            {
                
                return false;
            }
        }

        public bool update(string k, string v)
        {
            key = k;
            value = v;

            if (_dic.ContainsKey(k))
            {
                undoUpdates[countupdates] = _dic[k];
                _dic[k] = v;
                countupdates++;
                return true;
            }
            else
            {
                
                return false;
            }
        }

        public void undoUpdate(string k)
        {
            _dic[k] = undoUpdates[countupdates];
            countupdates--;
            
        }

        public bool backup(string _dest)
        {
           
            foreach(KeyValuePair<string,string> keyvalue in _dic)
            {
                File.AppendAllText(_dest, string.Format("{0} {1} {2}", keyvalue.Key, keyvalue.Value, Environment.NewLine));
            }

            return true;
            
        }

        public string getVal(string k)
        {
            if(_dic.ContainsKey(k))
            {
                return _dic[k];
            }
            else
            {
                return null;
            }
        }

    }
}
