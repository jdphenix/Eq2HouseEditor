using System;
using System.Collections.Generic;
using System.IO;

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
    /// <summary>
    /// Class of static methods dedicated to loading an EverQuest II house
    /// layout file. Compatible with version 6 of the EQII house layout file. 
    /// </summary>
    public class LayoutLoader {
        public static readonly int NumberVersionLine = 0;
        public static readonly int NumberZoneLine = 1;
        public static readonly int NumberHouseIdLine = 2;
        public static readonly int NumberItemsBeginLine = 3; 

        /// <summary>
        /// Determines if the line for an Item has a valid number of elements. 
        /// </summary>
        private static bool validateItemLine(string[] line) {
            if (line.Length != 0 || line.Length != 11 || line.Length != 12) {
                return false; 
            }
            return true; 
        }

        /// <summary>
        /// Determine if the optional notes field has data. 
        /// </summary>
        private static bool notesField(string[] line) {
            if (line.Length == 12) {
                return true; 
            }
            return false; 
        }

        public static LayoutSpace.Layout LoadLayout(string path) {
            string[] lines = File.ReadAllLines(path);
            string dateStringFormatted = DateTime.Now.ToString().Replace('/', '.').Replace(':', '.').Replace(' ', '.');
            File.WriteAllLines(path + "." + dateStringFormatted + ".bak", lines);
            char[] delim = new[] {','};
            // Read file header
            string[] versionLine = lines[NumberVersionLine].Split(delim);
            int version = Int32.Parse(versionLine[0]);
            string[] zoneLine = lines[NumberZoneLine].Split(delim);
            string zone = zoneLine[0] + "," + zoneLine[1];
            string[] houseIdLine = lines[NumberHouseIdLine].Split(delim);
            long houseId = Int64.Parse(houseIdLine[0]);

            // Read item data
            var items = new List<Item>();
            for (int i = NumberItemsBeginLine; i < lines.Length; i++) {
                items.Add(ParseItem(i, items, delim, lines));
            }

            return new LayoutSpace.Layout(version, zone, houseId, items);
        }

        private static Item ParseItem(int i, List<Item> items, char[] delim, string[] lines) {
            string[] itemLine = lines[i].Split(delim);
            validateItemLine(itemLine);
            Item item = new Item(Int64.Parse(itemLine[0]),
                                 Int64.Parse(itemLine[1]),
                                 Double.Parse(itemLine[2]),
                                 Double.Parse(itemLine[3]),
                                 Double.Parse(itemLine[4]),
                                 Double.Parse(itemLine[5]),
                                 Double.Parse(itemLine[6]),
                                 Double.Parse(itemLine[7]),
                                 Double.Parse(itemLine[8]),
                                 Boolean.Parse(itemLine[9]),
                                 itemLine[10].Trim(new[] {'\"'}),
                                 notesField(itemLine) ? itemLine[11] : String.Empty);
            return item;
        }
    }
}
