using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Physiotherapeut : PersonenTyp
    {

        public override string PREFIX => "physiotherapeut";
        public override String SHORT { get => "PHYSIO"; }

        private Person _Person;

        public override Person Person { get => _Person; protected set => _Person = value; }

        private KeyValuePair<String, KeyValuePair<String, object>>[] _Eigenschaften;

        public override KeyValuePair<String, KeyValuePair<String, object>>[] Eigenschaften
        {
            get => _Eigenschaften != null ? _Eigenschaften : new KeyValuePair<string, KeyValuePair<string, object>>[0];
            protected set => _Eigenschaften = value;
        }


        public Physiotherapeut() {
        }

        public Physiotherapeut(Person person, uint punkte)
        {
            this._Person = person;

            person.Typen.Physiotherapeut = this;
        }

    }
}