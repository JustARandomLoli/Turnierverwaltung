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

        public Controller()
        {
            People = new List<Person>();
            Mannschaften = new List<Mannschaft>();
            conn = new SQLiteConnection(@"Data Source=test.db;Version=3;", true);
            conn.Open();

            initDatabase();
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
                nachname VARCHAR(64) NOT NULL,
                vorname VARCHAR(64) NOT NULL,
                fussballspieler BOOLEAN DEFAULT 0,
                fussballspieler_tore INTEGER UNSIGNED DEFAULT 0,
                volleyballspieler BOOLEAN DEFAULT 0,
                volleyballspieler_punkte INTEGER UNSIGNED DEFAULT 0
            );"));

            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS mannschaften (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(64) NOT NULL
            );"));

            Run(new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS mannschaften_people (
                person INTEGER,
                mannschaft INTEGER,

                UNIQUE(person, mannschaft)
            );"));

        }

        public SQLiteConnection GetConnection()
        {
            return conn;
        }

        public void PeopleFromDb()
        {
            People.Clear();
            SQLiteCommand cmd = new SQLiteCommand(@"SELECT * FROM people");
            SQLiteDataReader reader;
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Boolean istFussballer = reader.GetBoolean(3);
                Boolean istVolleyballspieler = reader.GetBoolean(5);

                Person p = new Person((Int32)reader.GetInt64(0), reader.GetString(1), reader.GetString(2));
                if (istFussballer) p.Fussballspieler((UInt32)reader.GetInt32(4));
                if (istVolleyballspieler) p.Volleyballspieler((UInt32)reader.GetInt32(6));

                People.Add(p);
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
                Mannschaft m = new Mannschaft((Int32)reader.GetInt64(0), reader.GetString(1));

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

        public List<Person> GetPeople()
        {
            return People;
        }

        public List<Mannschaft> GetMannschaften()
        {
            return Mannschaften;
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

    }
}