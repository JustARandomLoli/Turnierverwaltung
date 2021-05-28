using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class Turnier
    {

        private Spiel _Spiel;

        public Spiel Spiel { get => _Spiel; }

        public Turnier(Spiel spiel) {
            this._Spiel = spiel;
        }

    }
}