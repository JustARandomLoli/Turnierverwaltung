using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Handballspieler : PersonenTyp
    {

        public override string PREFIX => "handballspieler";
        public override String SHORT { get => "HB"; }

        private Person _Person;

        public override Person Person { get => _Person; protected set => _Person = value; }

        private KeyValuePair<String, KeyValuePair<String, object>>[] _Eigenschaften;

        public override KeyValuePair<String, KeyValuePair<String, object>>[] Eigenschaften
        {
            get => _Eigenschaften;
            protected set => _Eigenschaften = value;
        }


        public Handballspieler() {
            InitEigenschaften(0);
        }

        public Handballspieler(Person person, uint punkte)
        {
            this._Person = person;
            InitEigenschaften(punkte);

            person.Typen.Handballspieler = this;
        }

        private void InitEigenschaften(object value)
        {
            AddEigenschaft("Punkte", "INTEGER UNSIGNED DEFAULT 0", value);
        }

    }
}