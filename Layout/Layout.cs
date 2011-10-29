using System;
using System.Collections.Generic;

/*
Copyright 2011 Justin Davis. All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are
permitted provided that the following conditions are met:

   1. Redistributions of source code must retain the above copyright notice, this list of
      conditions and the following disclaimer.

   2. Redistributions in binary form must reproduce the above copyright notice, this list
      of conditions and the following disclaimer in the documentation and/or other materials
      provided with the distribution.

THIS SOFTWARE IS PROVIDED BY Justin Davis ''AS IS'' AND ANY EXPRESS OR IMPLIED
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Justin Davis OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those of the
authors and should not be interpreted as representing official policies, either expressed
or implied, of Justin Davis.
 */

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
