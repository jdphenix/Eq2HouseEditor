using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LayoutSpace;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args) {
            Layout l = LayoutLoader.LoadLayout(@"F:\eq2\saved_house_layouts\phlikk_tenebrous_2");
            LayoutSaver.SaveLayout(@"F:\eq2\saved_house_layouts\phlikk_tenebrous_2", l);
        }
    }
}
