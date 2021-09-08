using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.View
{
    public partial class Ranking : System.Web.UI.Page
    {

        private Controller controller;

        protected void Page_Load(object sender, EventArgs e)
        {
            controller = Global.Controller;
        
            controller.SpieleFromDb();

            foreach(Spiel spiel in controller.GetSpiele(Nutzer.GetNutzer(this.Page.User.Identity.Name)))
            {
                for(int i = 0; i < 2; i++)
                {
                    Mannschaft m = spiel.Mannschaften[i];
                    int Punkte = spiel.Punktestand[i];

                    TableRow row = new TableRow();

                    TableCell turnier = new TableCell();
                    TableCell mannschaft = new TableCell();
                    TableCell pd = new TableCell();
                    TableCell punkte = new TableCell();


                    turnier.Text = spiel.Turnier.Name;
                    mannschaft.Text = m.Name;
                    pd.Text = (Punkte - spiel.Punktestand[(i + 1) % 2]).ToString();
                    punkte.Text = Punkte.ToString();


                    row.Cells.Add(turnier);
                    row.Cells.Add(mannschaft);
                    row.Cells.Add(pd);
                    row.Cells.Add(punkte);

                    tableListe.Rows.Add(row);
                }

            }

        }
    }
}