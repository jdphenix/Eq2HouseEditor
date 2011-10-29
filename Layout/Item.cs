using System;
using System.Collections.Generic;
using System.Windows;

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

namespace LayoutSpace {
    public class Item : DependencyObject {
        private static int _count;

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof (int),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ItemIdProperty =
            DependencyProperty.Register("ItemId", typeof (long),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty DbIdProperty =
            DependencyProperty.Register("DbId", typeof (long),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ZProperty =
            DependencyProperty.Register("Z", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty PitchProperty =
            DependencyProperty.Register("Pitch", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty RollProperty =
            DependencyProperty.Register("Roll", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof (double),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CratedProperty =
            DependencyProperty.Register("Crated", typeof (bool),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof (string),
                                        typeof (Item), new UIPropertyMetadata(null));

        public static readonly DependencyProperty NotesProperty =
            DependencyProperty.Register("Notes", typeof (string),
                                        typeof (Item), new UIPropertyMetadata(null));

        internal Item() {
            _count++;
        }

        internal Item(long itemId, long dbId, double x, double y, double z, double rotation, double pitch, double roll,
                      double scale, bool crated, string name, string notes) {
            ItemId = itemId;
            DbId = dbId;
            X = x;
            Y = y;
            Z = z;
            Rotation = rotation;
            Pitch = pitch;
            Roll = roll;
            Scale = scale;
            Crated = crated;
            Name = name;
            Notes = notes;
        }

        public int Id {
            get { return (int) GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public long ItemId {
            get { return (long) GetValue(ItemIdProperty); }
            set { SetValue(ItemIdProperty, value); }
        }

        public long DbId {
            get { return (long) GetValue(DbIdProperty); }
            set { SetValue(DbIdProperty, value); }
        }

        public double X {
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public double Y {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public double Z {
            get { return (double) GetValue(ZProperty); }
            set { SetValue(ZProperty, value); }
        }

        public double Rotation {
            get { return (double) GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }

        public double Pitch {
            get { return (double) GetValue(PitchProperty); }
            set { SetValue(PitchProperty, value); }
        }

        public double Roll {
            get { return (double) GetValue(RollProperty); }
            set { SetValue(RollProperty, value); }
        }

        public double Scale {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public bool Crated {
            get { return (bool) GetValue(CratedProperty); }
            set { SetValue(CratedProperty, value); }
        }

        public string Name {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string Notes {
            get { return (string) GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        /// <summary>
        /// Gets the listing of Groups that this Item belongs to. 
        /// </summary>
        public IList<string> GetGroups() {
            var ret = new List<string>();
            if (Notes == String.Empty) return ret;
            if (Notes != String.Empty && !Notes.Contains("Yusdan Group"))
                return null;

            int indexStart = Notes.IndexOf("Yusdan Group:") + "Yusdan Group:".Length;
            int indexEnd = Notes.LastIndexOf(":") - indexStart; 
            string groups = Notes.Substring(indexStart, indexEnd);
            return new List<string>(groups.Split(':'));
        }

        public bool? AddGroup(string name) {
            if (Notes != String.Empty && !Notes.Contains("Yusdan Group"))
                return null;

            if (Notes == String.Empty) {
                Notes = "Yusdan Group:" + name + ":";
                return true; 
            }
            
            // If we get here, a group created by this utility likely exists. 
            Notes = Notes.Insert(Notes.Length, name + ":");
            return true; 
        }

    internal void ResetCount() {
            _count = 0;
        }
    }
}