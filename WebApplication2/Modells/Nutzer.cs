using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class Nutzer
    {

        public static List<Nutzer> Nutzers;
        
        public bool IsGroup { get; protected set; }
        public int ID { get; protected set; }
        private List<String> Permissions { get; set; }
        public String DisplayName { get; protected set; }
        public Nutzer Group { get; protected set; }
        public bool HasGroup { get => Group != null; }
        public int GroupID { get => HasGroup ? Group.ID : ID; }

        public Nutzer(int ID, bool IsGroup, string DisplayName)
        {
            this.ID = ID;
            this.IsGroup = IsGroup;
            this.DisplayName = DisplayName;
            this.Permissions = new List<String>();

            if (Nutzer.Nutzers == null) Nutzer.Nutzers = new List<Nutzer>();
            Nutzer.Nutzers.Add(this);
        }

        public static Nutzer GetNutzer(string name)
        {
            foreach(Nutzer n in Nutzers)
            {
                if (n.DisplayName == name) return n;
            }

            return null;
        }

        public Nutzer(bool IsGroup, string DisplayName) : this(-1, IsGroup, DisplayName) { }

        public void AddPermission(string permission)
        {
            Permissions.Add(permission);
        }

        public Nutzer AddPermissions(string permission)
        {
            AddPermission(permission);
            return this;
        }

        public String[] GetPermissions()
        {
            return Permissions.ToArray<String>();
        }

        public bool HasPermission(int id, string permission)
        {
            return HasPermission(id + " " + permission);
        }

        public bool HasPermission(string permission)
        {
            foreach (String p in Permissions)
            {
                String p1 = p;

                String[] arr = p.Split(' ');
                String[] per = permission.Split(' ');

                if (!(arr[0] == "*" || int.TryParse(arr[0], out _))) p1 = (IsGroup || !HasGroup ? ID : GroupID) + " " + p;

                String[] p2 = p1.Split(' ');

                bool v = true;
                for(int i = 0; i < per.Length; i++)
                {
                    if (!(per[i] == p2[i] || p2[i] == "*")) v = false;
                }

                if (v) return true;
            }

            return false;
        }



    }
}