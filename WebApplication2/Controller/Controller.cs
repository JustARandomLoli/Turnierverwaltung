using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.IO;

namespace WebApplication2
{
    public class Controller
    {

        SQLiteConnection conn;

        private List<Person> People;
        private List<Mannschaft> Mannschaften;
        private List<Spiel> Spiele;
        private List<Turnier> Turniere;

        public Controller()
        {
            People = new List<Person>();
            Mannschaften = new List<Mannschaft>();
            Spiele = new List<Spiel>();
            Turniere = new List<Turnier>();
            conn = new SQLiteConnection(@"Data Source=test.db;Version=3;", true);
            conn.Open();

            initDatabase();
            new Nutzer(0, false, "erik").AddPermissions("view_people").AddPermissions("view_turniere").AddPermissions("view_spiele").AddPermissions("view_mannschaften");
            new Nutzer(1, false, "elefant").AddPermissions("view_people").AddPermissions("view_turniere").AddPermissions("view_spiele").AddPermissions("view_mannschaften");
            new Nutzer(2, false, "admin").AddPermissions("* view_people").AddPermissions("* view_turniere").AddPermissions("* view_spiele").AddPermissions("* view_mannschaften");
        }

        public object Run(SQLiteCommand cmd)
        {
            SQLiteDataAdapter sda = new SQLiteDataAdapter();
            cmd.Connection = conn;
            sda.InsertCommand = cmd;
            object result = sda.InsertCommand.ExecuteScalar();
            cmd.Dispose();

            return result;
        }

        public void initDatabase()
        {
            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS people (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                deleted INTEGER DEFAULT 0,
                user INTEGER,
                nachname VARCHAR(64) NOT NULL,
                vorname VARCHAR(64) NOT NULL "
                + new PersonenTypListe().GenerateCreateStatement() + ");"));

            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS mannschaften (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user INTEGER,
                name VARCHAR(64) NOT NULL
            );"));

            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS mannschaften_people (
                person INTEGER,
                mannschaft INTEGER,

                UNIQUE(person, mannschaft)
            );"));

            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS spiele (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user INTEGER,
                mannschaft1 INTEGER,
                mannschaft2 INTEGER,
                punktestand1 INTEGER,
                punktestand2 INTEGER,
                spielart VARCHAR(64),

                turnier INTEGER DEFAULT NULL
            );"));

            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS turniere (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user INTEGER,
                name VARCHAR(64),
                spielart VARCHAR(64)
            );"));

        }

        public SQLiteConnection GetConnection()
        {
            return conn;
        }

        public void PeopleFromDb()
        {
            People.Clear();
            SQLiteCommand cmd = new SQLiteCommand(@"SELECT * FROM people WHERE deleted = 0");
            SQLiteDataReader reader;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {

                Person p = new Person((Int32)reader.GetInt64(0), reader.GetString(3), reader.GetString(4), reader.GetInt32(2));
                foreach(PersonenTyp typ in p.Typen.GetTypen()) typ.ParseReader(p, reader);

                People.Add(p);
            }
            cmd.Dispose();
            reader.Dispose();
        }

        public void SpieleFromDb()
        {
            PeopleFromDb();
            MannschaftFromDb();

            Spiele.Clear();
            Turniere.Clear();

            SQLiteCommand cmd = new SQLiteCommand(@"SELECT * FROM turniere");
            SQLiteDataReader reader;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                List<Spiel> spiels = new List<Spiel>();

                SQLiteCommand cmd2 = new SQLiteCommand(@"SELECT * FROM spiele WHERE turnier = @turnier");
                SQLiteDataReader reader2;
                cmd2.Connection = conn;
                cmd2.Parameters.AddWithValue("@turnier", reader.GetInt32(0));
                reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {

                    Spiel p = new Spiel((Int32)reader2.GetInt64(0), GetMannschaft((Int32)reader2.GetInt64(2)), GetMannschaft((Int32)reader2.GetInt64(3)), reader2.GetString(6), (Int32)reader2.GetInt64(1));
                    p.Punktestand[0] = (Int32)reader2.GetInt64(3);
                    p.Punktestand[1] = (Int32)reader2.GetInt64(4);

                    Spiele.Add(p);
                    spiels.Add(p);
                }
                cmd2.Dispose();
                reader2.Dispose();

                Turnier t = new Turnier(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), spiels, reader.GetInt32(1));
                Turniere.Add(t);
                foreach (Spiel s in spiels) s.Turnier = t;

            }
            cmd.Dispose();
            reader.Dispose();
        }

        public void MannschaftFromDb()
        {
            Mannschaften.Clear();
            SQLiteCommand cmd = new SQLiteCommand(@"SELECT * FROM mannschaften");
            SQLiteDataReader reader;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Mannschaft m = new Mannschaft((Int32)reader.GetInt64(0), reader.GetString(2), (Int32)reader.GetInt64(1));

                Mannschaften.Add(m);
            }
            cmd.Dispose();
            reader.Dispose();

            cmd = new SQLiteCommand(@"SELECT * FROM mannschaften_people");
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foreach(Mannschaft m in Mannschaften)
                {
                    if(m.SID == (Int32)reader.GetInt64(1))
                    {
                        foreach (Person p in People)
                        {
                            if (p.SID == (Int32)reader.GetInt64(0))
                            {
                                m.TeilnehmerHinzufuegen(p);
                            }
                        }
                    }
                }
                
            }
            cmd.Dispose();
            reader.Dispose();

        }

        public List<Person> GetPeople(Nutzer nutzer)
        {
            return People.FindAll(delegate(Person p) {
                return nutzer == null ? true : nutzer.HasPermission(p.PID.ToString() + " view_people");
            });
        }

        public List<Spiel> GetSpiele(Nutzer nutzer)
        {
            return Spiele.FindAll(delegate (Spiel s) {
                return nutzer == null ? true : nutzer.HasPermission(s.PID.ToString() + " view_spiele");
            });
        }

        public List<Turnier> GetTurniere(Nutzer nutzer)
        {
            return Turniere.FindAll(delegate (Turnier s) {
                return nutzer == null ? true : nutzer.HasPermission(s.PID.ToString() + " view_turniere");
            });
        }

        public List<Mannschaft> GetMannschaften(Nutzer nutzer)
        {
            return Mannschaften.FindAll(delegate (Mannschaft s) {
                return nutzer == null ? true : nutzer.HasPermission(s.PID.ToString() + " view_mannschaften");
            });
        }

        public Mannschaft GetMannschaft(int sid)
        {
            foreach(Mannschaft m in Mannschaften) if (m.SID == sid) return m;
            return null;
        }

        public Person GetPerson(int sid)
        {
            foreach (Person p in People) if (p.SID == sid) return p;
            return null;
        }

        public Mannschaft GetMannschaft(string sid)
        {
            foreach (Mannschaft m in Mannschaften) if (m.SID.ToString() == sid) return m;
            return null;
        }

        public Person GetPerson(string sid)
        {
            foreach (Person p in People) if (p.SID.ToString() == sid) return p;
            return null;
        }

        public Turnier GetTurnier(string sid)
        {
            foreach (Turnier t in Turniere) if (t.SID.ToString() == sid) return t;
            return null;
        }

        public Turnier GetTurnier(int sid)
        {
            foreach (Turnier t in Turniere) if (t.SID == sid) return t;
            return null;
        }

    }
}