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

        private Int32 ?_SID;
        private String _Nachname;
        private String _Vorname;
        private PersonenTypListe _Typen;

        public PersonenTypListe Typen { get => _Typen; }
        public Int32 ?SID { get => _SID; }
        public String Nachname { get => _Nachname; }
        public String Vorname { get => _Vorname; }
        public String Name { get => Vorname + " " + Nachname; }
        public Int32 ?PID { get; protected set; }

        public Person(int ?sid, string nachname, string vorname, int ?pid)
        {
            this._SID = sid;
            this._Nachname = nachname;
            this._Vorname = vorname;
            this.PID = pid;

            this._Typen = new PersonenTypListe();
        }

        public void MarkAsDeleted()
        {
            SQLiteCommand cmd = new SQLiteCommand(@"UPDATE people SET deleted = 1 WHERE id = @id;");

            if (_SID != null) cmd.Parameters.AddWithValue("@id", SID);
           
            
            object result = Global.Controller.Run(cmd);
            if (result as DBNull != null)
            {
                this._SID = (Int32)result;
            }

            cmd.Dispose();
        }

        public void Delete()
        {
            SQLiteCommand cmd = new SQLiteCommand(@"DELETE FROM people WHERE id = @id;");

            if (_SID != null) cmd.Parameters.AddWithValue("@id", SID);


            object result = Global.Controller.Run(cmd);
            if (result as DBNull != null)
            {
                this._SID = (Int32)result;
            }

            cmd.Dispose();
        }


        public void InsertIntoDb(Controller controller)
        {
            String fields = "";
            String values = "";
            String update = "";
            foreach (PersonenTyp typ in Typen.GetTypen()) {

                fields += ", " + typ.PREFIX;
                values += ", @" + typ.PREFIX;
                update += ", " + typ.PREFIX + " = @" + typ.PREFIX;
      
                foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in typ.Eigenschaften)
                {
                    fields += ", " + typ.PREFIX + "_" + pair.Key.ToLower();
                    values += ", @" + typ.PREFIX + "_" + pair.Key.ToLower();
                    update += ", " + typ.PREFIX + "_" + pair.Key.ToLower() + " = @" + typ.PREFIX + "_" + pair.Key.ToLower();
                }
            }

            SQLiteCommand cmd;
            if(_SID == null) cmd = new SQLiteCommand(@"INSERT INTO people (nachname, vorname, user" + fields + ") values (@nachname, @vorname, @user" + values + ");");
            else cmd = new SQLiteCommand(@"UPDATE people SET nachname = @nachname, vorname = @vorname" + update + " WHERE id = @id;");

            if(_SID != null) cmd.Parameters.AddWithValue("@id", SID);

            cmd.Parameters.AddWithValue("@nachname", Nachname);
            cmd.Parameters.AddWithValue("@vorname", Vorname);
            cmd.Parameters.AddWithValue("@user", PID);

            foreach (PersonenTyp typ in Typen.GetTypen()) {
                typ.PrepareStatement(cmd);
            }

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
            return GetTableRow(new TableCell[0]);
        }

        public TableRow GetTableRow(WebControl control)
        {
            TableCell c = new TableCell();
            c.Controls.Add(control);
            return GetTableRow(new TableCell[] { c });
        }

        public TableRow GetTableRow(TableCell[] extraCells)
        {
            TableRow row = new TableRow();

            string t = "";
            foreach (PersonenTyp typ in Typen.GetTypen()) if (typ.Exists()) t += ", " + typ.SHORT;
            if (t.StartsWith(", ")) t = t.Substring(2);

            TableCell tCell = new TableCell();
            tCell.Controls.Add(new HyperLink { Text = SID.ToString(), NavigateUrl = "javascript:OpenModal("+SID+");",  });

            row.Cells.Add(tCell);
            row.Cells.Add(CreateCell(Nachname));
            row.Cells.Add(CreateCell(Vorname));

            foreach (TableCell cell in extraCells) row.Cells.Add(cell);

            /*foreach (PersonenTyp typ in Typen.GetTypen()) {
                typ.AddToTable(row.Cells);
            }*/

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
            return SID == null ? -1 : (int)SID;
        }


    }
}