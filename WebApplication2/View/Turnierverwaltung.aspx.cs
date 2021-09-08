using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication2.View
{
    public partial class Turnierverwaltung : System.Web.UI.Page
    {
        Controller controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = Global.Controller;
            labb.Text = Directory.GetCurrentDirectory();


            controller.PeopleFromDb();
            controller.MannschaftFromDb();
            controller.SpieleFromDb();

            if (IsPostBack) return;
            

            foreach (Turnier t in controller.GetTurniere(Nutzer.GetNutzer(this.Page.User.Identity.Name))) turniere.Items.Add(new ListItem(t.Name, t.SID.ToString()));
            foreach (Mannschaft m in controller.GetMannschaften(Nutzer.GetNutzer(this.Page.User.Identity.Name)))
            {
                mannschaft1.Items.Add(new ListItem(m.Name, m.SID.ToString()));
                mannschaft2.Items.Add(new ListItem(m.Name, m.SID.ToString()));
            }

            foreach(PersonenTyp typ in new PersonenTypListe())
            {
                spielart.Items.Add(new ListItem(typ.SHORT, typ.PREFIX));
            }

            UpdatePeopleList();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Gateway.aspx");
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Turnier t = new Turnier(name.Text, spielart.SelectedValue, new List<Spiel>(), Nutzer.GetNutzer(this.Page.User.Identity.Name).GroupID);
            t.SaveToDb();
            controller.SpieleFromDb();
            Response.Redirect(Request.RawUrl);
        }

        protected void addspiel_Click(object sender, EventArgs e)
        {
            Turnier t = controller.GetTurnier(turniere.SelectedValue);
            Spiel s = new Spiel(controller.GetMannschaft(mannschaft1.SelectedValue), controller.GetMannschaft(mannschaft2.SelectedValue), t.Spielart, Nutzer.GetNutzer(this.Page.User.Identity.Name).GroupID);
            s.Turnier = t;
            s.Punktestand[0] = Convert.ToInt32(punke1.Text);
            s.Punktestand[1] = Convert.ToInt32(punkte2.Text);
            s.SaveToDb();
            controller.SpieleFromDb();
            Response.Redirect(Request.RawUrl);
        }

        protected void turniere_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePeopleList();
        }

        public void UpdatePeopleList()
        {
            System.Diagnostics.Debug.WriteLine(turniere.SelectedValue);
            if (turniere.SelectedValue != "")
            {
                foreach (TableRow row in spiele.Rows) if (!(row is TableHeaderRow)) spiele.Rows.Remove(row);
                foreach (Spiel spiel in controller.GetTurnier(turniere.SelectedValue)) spiele.Rows.Add(spiel.GetTableRow());

            }
        }

    }
}