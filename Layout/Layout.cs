using System;
using System.Collections.Generic;

namespace LayoutSpace
{
    public class Layout
    {
        public int Version { get; private set; }
        public string Zone { get; private set; }
        public long Id { get; private set; }
        public List<Item> Items { get; private set; } 

        private Layout() {
        }

        public Layout(int version, string zone, long id, IEnumerable<Item> items) {
            this.Version = version;
            this.Zone = zone;
            this.Id = id;
            this.Items = items as List<Item>;
        }

        internal Boolean AddItem(Item i) {
            Items.Add(i);
            return true; 
        }

        internal Boolean AddItem(IEnumerable<Item> i) {
            Items.AddRange(i);
            return true; 
        }
    }
}
