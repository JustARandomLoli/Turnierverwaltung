using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public abstract class PersonenTyp
    {

        public abstract String PREFIX { get; }
        
        public abstract String SHORT { get; }

        public abstract KeyValuePair<String, KeyValuePair<String, object>>[] Eigenschaften { get; protected set; }

        public abstract Person Person { get; protected set; }

        public virtual void PrepareStatement(SQLiteCommand cmd)
        {
            cmd.Parameters.AddWithValue("@" + PREFIX, Exists() ? 1 : 0);
            System.Diagnostics.Debug.WriteLine(PREFIX + " " + Exists());
            foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in Eigenschaften)
            {
                cmd.Parameters.AddWithValue("@" + PREFIX + "_" + pair.Key.ToLower(), pair.Value.Value);
            }
        }

        public void AddEigenschaft(string name, string db, object value)
        {
            List<KeyValuePair<String, KeyValuePair<String, object>>> list = new List<KeyValuePair<string, KeyValuePair<string, object>>>();
            if (Eigenschaften != null) list = Eigenschaften.ToList();
            list.Add(new KeyValuePair<String, KeyValuePair<String, object>>(name, new KeyValuePair<String, object>(db, value)));
            Eigenschaften = list.ToArray();
        }

        public void SetPerson(Person p)
        {
            if(Person == null) Person = p;
        }

        public object Get(string name)
        {
            foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in Eigenschaften)
            {
                if (pair.Key == name) return pair.Value.Value;
            }

            throw new Exception("Not Found 404 " + name);
        }

        public void Set(string key, object value)
        {
            int i = 0;
            foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in Eigenschaften)
            {
                if (Eigenschaften[i].Key == key)
                {
                    Eigenschaften[i++] = new KeyValuePair<String, KeyValuePair<String, object>>(pair.Key,
                        new KeyValuePair<String, object>(pair.Value.Key, value));
                    break;
                }
                else i++;
            }
        }

        public virtual void ParseReader(Person p, SQLiteDataReader reader)
        {
            if (!reader.GetBoolean(reader.GetOrdinal(PREFIX))) return;

            int i = 0;
            foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in Eigenschaften)
            {
                Eigenschaften[i++] = new KeyValuePair<String, KeyValuePair<String, object>>(pair.Key, 
                    new KeyValuePair<String, object>(pair.Value.Key, reader.GetValue(reader.GetOrdinal(PREFIX + "_" + pair.Key.ToLower()))));
            }
            Person = p;
        }

        public PersonenTyp() { }

        public bool Exists()
        {
            return Person != null;
        }

        public TableCell CreateCell(object content)
        {
            TableCell c = new TableCell();
            if (content != null) c.Text = content.ToString();
            return c;
        }


    }
}