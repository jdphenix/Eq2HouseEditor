using System.Collections.Generic;
using System.IO;
using System.Text;

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
    public class LayoutSaver
    {
    /// <summary>
    /// Save the specified <code>Layout</code> to an EverQuest II House Layout
    /// File. 
    /// </summary>
        public static void SaveLayout(string path, Layout layout) {
            List<string> layoutLines = new List<string>();
            layoutLines.Add(layout.Version + ",Version Number");
            layoutLines.Add(layout.Zone);
            layoutLines.Add(layout.Id + ", Unique House Number.");
            foreach(Item i in layout.Items) {
                StringBuilder s = new StringBuilder();
                s.Append(i.ItemId); s.Append(",");
                s.Append(i.DbId); s.Append(",");
                s.Append(i.X); s.Append(",");
                s.Append(i.Y); s.Append(",");
                s.Append(i.Z); s.Append(",");
                s.Append(i.Rotation); s.Append(",");
                s.Append(i.Pitch); s.Append(",");
                s.Append(i.Roll); s.Append(",");
                s.Append(i.Scale); s.Append(",");
                s.Append(i.Crated); s.Append(",");
                s.Append(i.Name); s.Append(",");
                s.Append(i.Notes); 
                layoutLines.Add(s.ToString());
            }
            File.WriteAllLines(path, layoutLines.ToArray());
        }
    }
}
