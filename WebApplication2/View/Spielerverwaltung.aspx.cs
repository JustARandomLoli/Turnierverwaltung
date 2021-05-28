using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.View
{
    public partial class Spielerverwaltung : System.Web.UI.Page
    {

        Controller controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = Global.Controller;
            UpdateSpieler();
        }

        private void UpdateSpieler()
        {
            controller.PeopleFromDb();
            foreach (Person spieler in controller.GetPeople())
            {
               tableListe.Rows.Add(spieler.GetTableRow());
            }
        }

        protected void btnbestaetigen_onclick(object sender, EventArgs e)
        {
            Person p = new Person(-1, txtNachname.Text, txtVorname.Text);
            if (typeFootball.Checked) p.Fussballspieler(Convert.ToUInt32(numTore.Text));
            if (typeVolleyball.Checked) p.Volleyballspieler(Convert.ToUInt32(numPunkte.Text));

            p.InsertIntoDb(controller);
            tableListe.Rows.Add(p.GetTableRow());

            txtNachname.Text = "";
            txtVorname.Text = "";
            Response.Redirect(Request.RawUrl);
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Gateway.aspx");
        }
    }
}