using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public class Person
    {
        protected Boolean _IstFussballspieler;
        protected Boolean _IstVolleyballspieler;

        private Int32 _SID;
        private String _Nachname;
        private String _Vorname;
        private UInt32 _Tore;
        private UInt32 _Punkte;

        public UInt32 Tore { get => _Tore; }
        public UInt32 Punkte { get => _Punkte; }
        public Boolean IstFussballspieler { get => _IstFussballspieler; }
        public Boolean IstVolleyballspieler { get => _IstVolleyballspieler; }
        public Int32 SID { get => _SID; }
        public String Nachname { get => _Nachname; }
        public String Vorname { get => _Vorname; }
        public String Name { get => _Vorname + " " + _Nachname; }

        public Person(int sid, string nachname, string vorname)
        {
            this._SID = sid;
            this._Nachname = nachname;
            this._Vorname = vorname;
            this._IstFussballspieler = false;
            this._IstVolleyballspieler = false;
        }

        public void Fussballspieler(uint tore)
        {
            this._IstFussballspieler = true;
            this._Tore = tore;
        }

        public void Volleyballspieler(uint punkte)
        {
            this._IstVolleyballspieler = true;
            this._Punkte = punkte;
        }


        public void InsertIntoDb(Controller controller)
        {
            SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO people (nachname, vorname, fussballspieler, fussballspieler_tore, volleyballspieler, volleyballspieler_punkte) values (@nachname, @vorname, @fussballspieler, @tore, @volleyballspieler, @punkte)");
            cmd.Parameters.AddWithValue("@nachname", Nachname);
            cmd.Parameters.AddWithValue("@vorname", Vorname);
            cmd.Parameters.AddWithValue("@fussballspieler", (IstFussballspieler ? 1 : 0));
            cmd.Parameters.AddWithValue("@tore", Tore);
            cmd.Parameters.AddWithValue("@volleyballspieler", (IstVolleyballspieler ? 1 : 0));
            cmd.Parameters.AddWithValue("@punkte", Punkte);

            object result = controller.Run(cmd);
            if (result as DBNull != null)
            {
                this._SID = (Int32)result;
            }
            
            cmd.Dispose();
        }

        private TableCell CreateCell(object content)
        {
            TableCell c = new TableCell();
            if(content != null) c.Text = content.ToString();
            return c;
        }

        public TableRow GetTableRow()
        {
            TableRow row = new TableRow();


            int t = 0;
            if (IstFussballspieler) t += 1;
            if (IstVolleyballspieler) t += 2;

            row.Cells.Add(CreateCell(t));
            row.Cells.Add(CreateCell(SID));
            row.Cells.Add(CreateCell(Nachname));
            row.Cells.Add(CreateCell(Vorname));
            row.Cells.Add(CreateCell(IstFussballspieler ? (object)Tore : null));
            row.Cells.Add(CreateCell(IstVolleyballspieler ? (object)Punkte : null));

            return row;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return SID;
        }


    }
}