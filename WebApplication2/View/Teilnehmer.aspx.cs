using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.View
{
    public partial class Teilnehmer : System.Web.UI.Page
    {

        Controller controller;
        PersonenTypListe typen;
        Person _teilnehmer;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = Global.Controller;
            typen = Global.personenTypListe;
            controller.PeopleFromDb();

            _teilnehmer = controller.GetPerson(Request.QueryString["sid"]);

            foreach (PersonenTyp typ in _teilnehmer.Typen)
            {
                ListItem item = new ListItem(typ.PREFIX);
                item.Value = typ.PREFIX;
                item.Selected = typ.Exists();
                TeilnehmerTypenList.Items.Add(item);

                foreach (KeyValuePair<string, KeyValuePair<string, object>> pair in typ.Eigenschaften)
                {
                    Label label = new Label();
                    label.Text = typ.SHORT + ": " + pair.Key + ": ";
                    label.ID = typ.PREFIX + "_label_" + pair.Key;

                    TextBox tb = new TextBox();
                    tb.ID = typ.PREFIX + "_" + pair.Key;
                    tb.Text = pair.Value.Value.ToString();

                    LiteralControl lb = new LiteralControl("<br />");

                    edit.Controls.Add(label);
                    edit.Controls.Add(tb);
                    edit.Controls.Add(lb);
                }
            }
            


            title.Text = _teilnehmer.Name;
            Title = _teilnehmer.Name;

            foreach(PersonenTyp typ in _teilnehmer.Typen.GetTypen()) {
                if (!typ.Exists()) continue;
                dataList.Items.Add(typ.GetType().Name);
                foreach (KeyValuePair<String, KeyValuePair<String, object>> pair in typ.Eigenschaften)
                {
                    dataList.Items.Add(pair.Key + ": " + pair.Value.Value);
                }
            }

        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in TeilnehmerTypenList.Items)
            {
                PersonenTyp pt = _teilnehmer.Typen.FindByPrefix(item.Value);
                if (item.Selected)
                {
                    
                    pt.SetPerson(_teilnehmer);

                    foreach (KeyValuePair<string, KeyValuePair<string, object>> pair in pt.Eigenschaften)
                    {
                        pt.Set(pair.Key, ((TextBox)edit.FindControl(pt.PREFIX + "_" + pair.Key)).Text);
                    }
                } else
                {
                    pt.SetPerson(null);
                }
            }

            _teilnehmer.InsertIntoDb(controller);
            Response.Redirect(Request.RawUrl);
        }

        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            _teilnehmer.MarkAsDeleted();
        }
    }
}