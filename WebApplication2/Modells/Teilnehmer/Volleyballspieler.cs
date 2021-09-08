using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Volleyballspieler : PersonenTyp
    {

        public override string PREFIX => "volleyballspieler";
        
        public override String SHORT { get => "VOLLEY"; }

        private KeyValuePair<String, KeyValuePair<String, object>>[] _Eigenschaften;

        public override KeyValuePair<String, KeyValuePair<String, object>>[] Eigenschaften
        {
            get => _Eigenschaften;
            protected set => _Eigenschaften = value;
        }

        private Person _Person;

        public override Person Person { get => _Person; protected set => _Person = value; }

        public Volleyballspieler() {
            InitEigenschaften(0);
        }

        public Volleyballspieler(Person person, uint punkte)
        {
            this._Person = person;
            InitEigenschaften(punkte);

            person.Typen.Volleyballspieler = this;
        }

        private void InitEigenschaften(object value)
        {
            AddEigenschaft("Punkte", "INTEGER UNSIGNED DEFAULT 0", value);
        }
        

    }
}