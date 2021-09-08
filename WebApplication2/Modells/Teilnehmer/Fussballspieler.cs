using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Fussballspieler : PersonenTyp
    {
        public override string PREFIX => "fussballspieler";


        public override String SHORT { get => "FB"; }

        private Person _Person;

        public override Person Person { get => _Person; protected set => _Person = value; }

        private KeyValuePair<String, KeyValuePair<String, object>>[] _Eigenschaften;

        public override KeyValuePair<String, KeyValuePair<String, object>>[] Eigenschaften
        {
            get => _Eigenschaften;
            protected set => _Eigenschaften = value;
        }


        public Fussballspieler() {
            InitEigenschaften(0);
        }

        public Fussballspieler(Person person, uint tore)
        {
            InitEigenschaften(tore);
            this._Person = person;

            person.Typen.Fussballspieler = this;
        }

        private void InitEigenschaften(object value)
        {
            AddEigenschaft("Tore", "INTEGER UNSIGNED DEFAULT 0", value);
        }

        public override void PrepareStatement(SQLiteCommand cmd)
        {
            base.PrepareStatement(cmd);
        }

        public override void ParseReader(Person p, SQLiteDataReader reader)
        {
            base.ParseReader(p, reader);
        }
    }
}