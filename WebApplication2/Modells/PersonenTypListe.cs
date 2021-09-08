using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class PersonenTypListe : ICollection<PersonenTyp>
    {

        public Volleyballspieler Volleyballspieler;
        public Fussballspieler Fussballspieler;
        public Handballspieler Handballspieler;
        public Trainer Trainer;
        public Physiotherapeut Physiotherapeut;

        public PersonenTyp[] Typen { get => GetTypen(); }

        public int Count => ((ICollection<PersonenTyp>)Typen).Count;

        public bool IsReadOnly => ((ICollection<PersonenTyp>)Typen).IsReadOnly;

        public PersonenTypListe()
        {
            this.Volleyballspieler = new Volleyballspieler();
            this.Fussballspieler = new Fussballspieler();
            this.Handballspieler = new Handballspieler();
            this.Trainer = new Trainer();
            this.Physiotherapeut = new Physiotherapeut();
        }

        public PersonenTyp[] GetTypen()
        {
            return new PersonenTyp[] {
                Volleyballspieler,
                Fussballspieler,
                Handballspieler,
                Trainer,
                Physiotherapeut
            };
        }

        public PersonenTyp FindByPrefix(string prefix)
        {
            foreach(PersonenTyp typ in GetTypen())
            {
                if(typ.PREFIX.ToLower() == prefix.ToLower())
                {
                    return typ;
                }
            }

            throw new Exception("404 not found: " + prefix);
        }

        public String GenerateCreateStatement()
        {
            String statement = "";

            foreach(PersonenTyp typ in GetTypen())
            {
                statement += ", " + typ.PREFIX + " BOOLEAN DEFAULT 0";
                
                foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in typ.Eigenschaften)
                {
                    Console.WriteLine(pair.Value.ToString());
                    statement += ", " + typ.PREFIX + "_" + pair.Key.ToLower() + " " + pair.Value.Key;
                }
            }

            return statement;
        }

        public void Add(PersonenTyp item)
        {
            ((ICollection<PersonenTyp>)Typen).Add(item);
        }

        public void Clear()
        {
            ((ICollection<PersonenTyp>)Typen).Clear();
        }

        public bool Contains(PersonenTyp item)
        {
            return ((ICollection<PersonenTyp>)Typen).Contains(item);
        }

        public void CopyTo(PersonenTyp[] array, int arrayIndex)
        {
            ((ICollection<PersonenTyp>)Typen).CopyTo(array, arrayIndex);
        }

        public bool Remove(PersonenTyp item)
        {
            return ((ICollection<PersonenTyp>)Typen).Remove(item);
        }

        public IEnumerator<PersonenTyp> GetEnumerator()
        {
            return ((ICollection<PersonenTyp>)Typen).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<PersonenTyp>)Typen).GetEnumerator();
        }
    }
}