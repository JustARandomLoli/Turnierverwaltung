using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace WebApplication2
{
    public class Mannschaft
    {

        private Int32 _SID;
        private String _Name;
        private List<Person> _Teilnehmer;

        public Int32 SID { get => _SID; }
        public String Name { get => _Name; }
        public Person[] Teilnehmer { get => _Teilnehmer.ToArray(); }

        public Mannschaft(int sid, string name)
        {
            this._SID = sid;
            this._Name = name;
            this._Teilnehmer = new List<Person>();
        }

        public void TeilnehmerHinzufuegen(Person person)
        {
            _Teilnehmer.Add(person);
        }

        public void TeilnehmerEntfernen(Person person)
        {
            _Teilnehmer.Remove(person);
        }

        public void InsertIntoDb(Controller controller)
        {
            SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO mannschaften (name) values (@name)");
            cmd.Parameters.AddWithValue("@name", Name);

            object result = controller.Run(cmd);
            if (result as DBNull != null)
            {
                this._SID = (Int32)result;
            }

            cmd.Dispose();
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

    }
}