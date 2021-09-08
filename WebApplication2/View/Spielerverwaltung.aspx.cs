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
        PersonenTypListe typen;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = Global.Controller;
            typen = Global.personenTypListe;
            UpdateSpieler();

            foreach(PersonenTyp typ in typen.GetTypen())
            {
                ListItem item = new ListItem(typ.PREFIX);
                item.Value = typ.PREFIX;
                TeilnehmerTypenList.Items.Add(item);

                foreach(KeyValuePair<string, KeyValuePair<string, object>> pair in typ.Eigenschaften)
                {
                    Label label = new Label();
                    label.Text = typ.SHORT + ": " + pair.Key + ": ";
                    label.ID = typ.PREFIX + "_label_" + pair.Key;

                    TextBox tb = new TextBox();
                    tb.ID = typ.PREFIX + "_" + pair.Key;

                    LiteralControl lb = new LiteralControl("<br />");

                    placeholderEigenschaften.Controls.Add(label);
                    placeholderEigenschaften.Controls.Add(tb);
                    placeholderEigenschaften.Controls.Add(lb);
                }
            }
        }

        private void UpdateSpieler()
        {
            controller.PeopleFromDb();
            foreach (Person spieler in controller.GetPeople(Nutzer.GetNutzer(this.Page.User.Identity.Name)))
            {
               tableListe.Rows.Add(spieler.GetTableRow());
            }
        }

        protected void btnbestaetigen_onclick(object sender, EventArgs e)
        {
            Person p = new Person(null, txtNachname.Text, txtVorname.Text, Nutzer.GetNutzer(this.Page.User.Identity.Name).GroupID);

            foreach (ListItem item in TeilnehmerTypenList.Items)
            {
                if (item.Selected)
                {
                    PersonenTyp pt = p.Typen.FindByPrefix(item.Value);
                    pt.SetPerson(p);

                    foreach (KeyValuePair<string, KeyValuePair<string, object>> pair in pt.Eigenschaften)
                    {
                        pt.Set(pair.Key, ((TextBox)placeholderEigenschaften.FindControl(pt.PREFIX + "_" + pair.Key)).Text);
                    }

                    //if (item.Value == p.Typen.Fussballspieler.PREFIX) new Fussballspieler(p, Convert.ToUInt32(numTore.Text));
                    //if (item.Value == p.Typen.Volleyballspieler.PREFIX) new Volleyballspieler(p, Convert.ToUInt32(numPunkte.Text));
                    //if (item.Value == p.Typen.Handballspieler.PREFIX) new Handballspieler(p, Convert.ToUInt32(numHandballPunkte.Text));
                }
            }
            
            

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