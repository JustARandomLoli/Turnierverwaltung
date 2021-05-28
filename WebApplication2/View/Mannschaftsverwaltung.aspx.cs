using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;

namespace WebApplication2.View
{
    public partial class Mannschaftsverwaltung : System.Web.UI.Page
    {

        Controller controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = Global.Controller;

            if(!IsPostBack)
            {
                UpdateMannschaften();
                UpdatePeopleList();
            }
        }

        private void UpdateMannschaften()
        {
            controller.PeopleFromDb();
            controller.MannschaftFromDb();
            listMannschaften.Items.Clear();
            listTeilnehmer.Items.Clear();
            foreach (Mannschaft m in controller.GetMannschaften()) listMannschaften.Items.Add(new ListItem(m.Name, m.GetHashCode().ToString()));
            foreach (Person p in controller.GetPeople()) listTeilnehmer.Items.Add(new ListItem(p.Name, p.GetHashCode().ToString()));
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Person p = controller.GetPerson(listTeilnehmer.SelectedValue);
            Mannschaft m = controller.GetMannschaft(listMannschaften.SelectedValue);

            SQLiteCommand cmd = new SQLiteCommand(@"INSERT INTO mannschaften_people (person, mannschaft) values (@person, @mannschaft)");
            cmd.Parameters.AddWithValue("@person", p.SID);
            cmd.Parameters.AddWithValue("@mannschaft", m.SID);

            try {
                controller.Run(cmd);
                m.TeilnehmerHinzufuegen(p);
            } catch(SQLiteException err) {

            } finally {
                cmd.Dispose();
                UpdatePeopleList();
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Mannschaft m = new Mannschaft(-1, txtName.Text);
            m.InsertIntoDb(controller);
            Response.Redirect(Request.RawUrl);
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Gateway.aspx");
        }

        protected void listMannschaften_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePeopleList();
        }

        public void UpdatePeopleList()
        {
            foreach (TableRow row in tableListe.Rows) if (!(row is TableHeaderRow)) tableListe.Rows.Remove(row);
            foreach (Person spieler in controller.GetMannschaft(listMannschaften.SelectedValue).Teilnehmer) tableListe.Rows.Add(spieler.GetTableRow());
        }

        
    }
}