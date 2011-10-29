using System.Collections.Generic;
using System.IO;
using System.Text;

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
