using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Spiel
    {
        private int ?_sid;

        private List<KeyValuePair<Person, Int32>> _teilnehmer;
        private Mannschaft[] _mannschaften;
        private Int32[] _punktestand;
        private String _spielart;
        private Turnier _Turnier;

        public List<KeyValuePair<Person, Int32>> Teilnehmer { get => _teilnehmer; }
        public Mannschaft[] Mannschaften { get => _mannschaften; }
        public Int32[] Punktestand { get => _punktestand; }
        public Int32 ?SID { get => _sid; }
        public String Spielart { get => _spielart; }
        public Turnier Turnier { get => _Turnier; set => _Turnier = value; }
        public Int32 ?PID { get; protected set; }

        public Spiel(int ?sid, Mannschaft m1, Mannschaft m2, String spielart, int ?pid) {
            _teilnehmer = new List<KeyValuePair<Person, Int32>>();
            _mannschaften = new Mannschaft[2];
            _mannschaften[0] = m1;
            _mannschaften[1] = m2;
            _punktestand = new Int32[2];
            _punktestand[0] = 0;
            _punktestand[1] = 0;
            _spielart = spielart;
            PID = pid;

            foreach (Person p in m1) _teilnehmer.Add(new KeyValuePair<Person, int>(p, 0));
            foreach (Person p in m2) _teilnehmer.Add(new KeyValuePair<Person, int>(p, 0));
            
        }

        public Spiel(Mannschaft m1, Mannschaft m2, String spielart, int ?pid) : this(null, m1, m2, spielart, pid)
        {

        }

        public int GetPunkte(Mannschaft m)
        {
            int i = 0;
            foreach (Mannschaft cm in Mannschaften) {
                if (cm.Equals(m)) return Punktestand[i];
                i++;
            }
            return 0;
        }

        public TableRow GetTableRow()
        {
            TableRow row = new TableRow();

            TableCell spiel = new TableCell();
            spiel.Text = Mannschaften[0] + " vs. " + Mannschaften[1];

            TableCell punkte = new TableCell();
            punkte.Text = Punktestand[0] + " : " + Punktestand[1];

            row.Cells.Add(spiel);
            row.Cells.Add(punkte);

            return row;
        }

        public void SaveToDb()
        {
            SQLiteCommand cmd;
            

            if(SID == null) cmd = new SQLiteCommand(@"INSERT INTO spiele (mannschaft1, mannschaft2, punktestand1, punktestand2, spielart, turnier, user) values (@mannschaft1, @mannschaft2, @punktestand1, @punktestand2, @spielart, @turnier, @user);");
            else cmd = new SQLiteCommand(@"UPDATE spiele SET punktestand1 = @punktestand1, punktestand2 = @punktestand2 WHERE id = @id;");

            if(SID == null)
            {

                cmd.Parameters.AddWithValue("@mannschaft1", Mannschaften[0].SID);
                cmd.Parameters.AddWithValue("@mannschaft2", Mannschaften[1].SID);
                cmd.Parameters.AddWithValue("@spielart", Spielart);
                cmd.Parameters.AddWithValue("@turnier", Turnier.SID);
                cmd.Parameters.AddWithValue("@user", PID);

            }
            else cmd.Parameters.AddWithValue("@id", SID);
            cmd.Parameters.AddWithValue("@punktestand1", Punktestand[0]);
            cmd.Parameters.AddWithValue("@punktestand2", Punktestand[1]);
            

            object result = Global.Controller.Run(cmd);
            if (result as DBNull != null)
            {
                if (SID == null) this._sid = (Int32)result;
            }

            cmd.Dispose();
        }

    }
}