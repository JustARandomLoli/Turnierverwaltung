using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Collections;

namespace WebApplication2
{
    public class Mannschaft : ICollection<Person>
    {

        private Int32 _SID;
        private String _Name;
        private List<Person> _Teilnehmer;

        public Int32 SID { get => _SID; }
        public String Name { get => _Name; }
        public Person[] Teilnehmer { get => _Teilnehmer.ToArray(); }
        public Int32 ?PID { get; protected set; }

        #region Collection
        public int Count => _Teilnehmer.Count;

        public bool IsReadOnly => ((ICollection<Person>)_Teilnehmer).IsReadOnly;
        #endregion

        public Mannschaft(int sid, string name, int ?pid)
        {
            this._SID = sid;
            this._Name = name;
            this._Teilnehmer = new List<Person>();
            this.PID = pid;
        }

        public void TeilnehmerHinzufuegen(Person person)
        {
            _Teilnehmer.Add(person);
        }

        public bool TeilnehmerEntfernen(Person person)
        {
            SQLiteCommand cmd = new SQLiteCommand(@"DELETE FROM mannschaften_people WHERE person = @person AND mannschaft = @mannschaft)");
            cmd.Parameters.AddWithValue("@person", person.SID);
            cmd.Parameters.AddWithValue("@mannschaft", SID);

            Global.Controller.Run(cmd);

            return _Teilnehmer.Remove(person);
        }

        public void InsertIntoDb(Controller controller)
        {
            SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO mannschaften (name, user) values (@name, @user)");
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@user", PID);

            object result = controller.Run(cmd);
            if (result as DBNull != null)
            {
                this._SID = (Int32)result;
            }

            cmd.Dispose();
        }

        public override String ToString()
        {
            return Name;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return SID;
        }

        #region Collection

        public void Add(Person item)
        {
            TeilnehmerHinzufuegen(item);
        }

        public void Clear()
        {
            ((ICollection<Person>)_Teilnehmer).Clear();
        }

        public bool Contains(Person item)
        {
            return ((ICollection<Person>)_Teilnehmer).Contains(item);
        }

        public void CopyTo(Person[] array, int arrayIndex)
        {
            ((ICollection<Person>)_Teilnehmer).CopyTo(array, arrayIndex);
        }

        public bool Remove(Person item)
        {
            return TeilnehmerEntfernen(item);
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return ((ICollection<Person>)_Teilnehmer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<Person>)_Teilnehmer).GetEnumerator();
        }

        #endregion

    }
}