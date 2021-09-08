using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Trainer : PersonenTyp
    {

        public override string PREFIX => "trainer";
        public override String SHORT { get => "TRAINER"; }

        private Person _Person;

        public override Person Person { get => _Person; protected set => _Person = value; }

        private KeyValuePair<String, KeyValuePair<String, object>>[] _Eigenschaften;

        public override KeyValuePair<String, KeyValuePair<String, object>>[] Eigenschaften
        {
            get => _Eigenschaften;
            protected set => _Eigenschaften = value;
        }


        public Trainer() {
            InitEigenschaften();
        }

        public Trainer(Person person)
        {
            this._Person = person;
            InitEigenschaften();

            person.Typen.Trainer = this;
        }

        private void InitEigenschaften()
        {
            _Eigenschaften = new KeyValuePair<string, KeyValuePair<string, object>>[0];
        }

    }
}