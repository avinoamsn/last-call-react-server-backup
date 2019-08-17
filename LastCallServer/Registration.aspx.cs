﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LastCallServer
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lastcallEntities db = new lastcallEntities();
            foodtype[] foodtypes = (from x in db.foodtypes select x).ToArray();

            string s = "";
            bool b = false;

            foreach (foodtype f in foodtypes)
            {
                s += "<asp:CheckBox ID=\"pref" + f.id.ToString() + "\" runat=\"server\" Checked=\"false\" Text=\"" +  f.foodtype1 + "\" onclick=\"preference(this, " + f.id.ToString() + ")\"/>";
                s += (b ? "<br/>" : "&nbsp;&nbsp;&nbsp;&nbsp;");
                b = !b;
            }

            preferences.Controls.Clear();
            preferences.Controls.Add(Page.ParseControl(s));

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            lastcallEntities db = new lastcallEntities();
            subscriber s = (from x in db.subscribers where x.email == email.Text select x).FirstOrDefault();

            if (s != null)
            {
                ErrorMessage.Text = "Username is already registered";
                return;
            }

            s = new subscriber() {
                deliveryaddress = address.Text,
                email = email.Text,
                emailoffers = (byte)(emailoffers.Checked ? 1 : 0),
                friendlyname = friendlyname.Text,
                onmailinglist = (byte)(mailinglist.Checked ? 1 : 0),
                password = password.Text,
                phone = telephone.Text,
                textoffers = (byte)(textoffers.Checked ? 1 : 0)
            };

            db.subscribers.Add(s);
            db.SaveChanges();

            string[] prefs = foodprefs.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string p in prefs)
            {
                foodpreference pref = new foodpreference()
                {
                    subscriberid = s.id,
                    preferenceid = Int32.Parse(p)
                };
                db.foodpreferences.Add(pref);
            }

            db.SaveChanges();
        }
    }
}