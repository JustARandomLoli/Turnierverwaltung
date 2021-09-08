using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class Turnier : ICollection<Spiel>
    {

        private List<Spiel> _Spiele;
        private String _Spielart;
        private String _Name;
        private Int32 ?_SID;

        public List<Spiel> Spiele { get => _Spiele; }
        public String Spielart { get => _Spielart; }
        public String Name { get => _Name; }
        public Int32 ?SID { get => _SID; }
        public Int32 ?PID { get; protected set; }

        #region Collection
        public int Count => _Spiele.Count;

        public bool IsReadOnly => ((ICollection<Spiel>)_Spiele).IsReadOnly;

        #endregion

        public Turnier(int ?sid, String name, String spielart, List<Spiel> spiele, int ?pid) {
            this._SID = sid;
            this._Spiele = spiele;
            this._Name = name;
            this._Spielart = spielart;
            this.PID = pid;
        }

        public Turnier(String name, String spielart, List<Spiel> spiele, int ?pid) : this(null, name, spielart, spiele, pid)
        {

        }

        public void SaveToDb()
        {
            SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO turniere (name, spielart, user) values (@name, @spielart, @user);");

            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@spielart", Spielart);
            cmd.Parameters.AddWithValue("@user", PID);


            object result = Global.Controller.Run(cmd);
            if (result as DBNull != null)
            {
                if (SID == null) this._SID = (Int32)result;
            }

            cmd.Dispose();
        }

        #region Collection
        public void CopyTo(Spiel[] array, int index)
        {
            _Spiele.CopyTo(array, index);
        }
        
        public void Add(Spiel item)
        {
            ((ICollection<Spiel>)_Spiele).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Spiel>)_Spiele).Clear();
        }

        public bool Contains(Spiel item)
        {
            return ((ICollection<Spiel>)_Spiele).Contains(item);
        }

        public bool Remove(Spiel item)
        {
            return ((ICollection<Spiel>)_Spiele).Remove(item);
        }

        IEnumerator<Spiel> IEnumerable<Spiel>.GetEnumerator()
        {
            return ((ICollection<Spiel>)_Spiele).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((ICollection<Spiel>)_Spiele).GetEnumerator();
        }
        #endregion


    }
}