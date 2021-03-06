﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LayoutSpace;

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

namespace Eq2HouseEditor
{
    /// <summary>
    /// Interaction logic for GroupEditWindow.xaml
    /// </summary>
    public partial class GroupEditWindow : Window
    {
        private Dictionary<string, HashSet<Item>> _groups;

        private List<TextBox> _numericTextBoxes; 

        private List<Item> _items;
        public List<Item> Items {
            set {
                _items = value;
                populateDictionary();
            }
            private get { return _items;  }
        }

        public GroupEditWindow() {
            InitializeComponent();

            _numericTextBoxes = new List<TextBox>();
            _numericTextBoxes.Add(txtX);
            _numericTextBoxes.Add(txtY);
            _numericTextBoxes.Add(txtZ);
            _numericTextBoxes.Add(txtRotation);
            _numericTextBoxes.Add(txtPitch);
            _numericTextBoxes.Add(txtRoll);
            _numericTextBoxes.Add(txtScale);
            _numericTextBoxes.Add(txtDirection);
        }

        private bool validateTextBoxes() {
            foreach (TextBox b in _numericTextBoxes) {
                double tmp; 
                if (!Double.TryParse(b.Text, out tmp) &&
                b.Text != String.Empty)
                    return false; 
            }
            return true; 
        }

        private void populateDictionary() {
            _groups = new Dictionary<string, HashSet<Item>>();
            
            foreach(Item i in _items) {
                foreach(string groupName in i.GetGroups()) {
                    // If a group hasn't been seen yet
                    if (!_groups.ContainsKey(groupName)) {
                        _groups.Add(groupName, new HashSet<Item>());
                    }
                    _groups[groupName].Add(i);
                }
            }
            BindListView();
        }

        private void BindListView() {
            lstGroups.ItemsSource = _groups.Keys;
        }

        #region Value Changes

        private void btnChangeClick(object sender, RoutedEventArgs e) {
            if (lstGroups.SelectedItem == null) {
                return; 
            }
            if (validateTextBoxes()) {
                string btnTag = ((Button) sender).Tag.ToString();
                switch(btnTag) {
                    case "ChangeX":
                        foreach(Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.X += Double.Parse(txtX.Text);
                        }
                        break;
                    case "ChangeY": 
                        foreach(Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Y += Double.Parse(txtY.Text);
                        }
                        break;
                    case "ChangeZ":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Z += Double.Parse(txtZ.Text);
                        }
                        break;
                    case "ChangeRotation":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Rotation += Double.Parse(txtRotation.Text);
                        }
                        break;
                    case "ChangePitch":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Pitch += Double.Parse(txtPitch.Text);
                        }
                        break;
                    case "ChangeRoll":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Roll += Double.Parse(txtRoll.Text);
                        }
                        break;
                    case "ChangeScale":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Scale += Double.Parse(txtScale.Text);
                        }
                        break;
                }
            }
        }

        private void btnSetClick(object sender, RoutedEventArgs e) {
            if (lstGroups.SelectedItem == null) {
                return;
            }
            if (validateTextBoxes()) {
                string btnTag = ((Button)sender).Tag.ToString();
                switch (btnTag) {
                    case "SetX":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.X = Double.Parse(txtX.Text);
                        }
                        break;
                    case "SetY":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Y = Double.Parse(txtY.Text);
                        }
                        break;
                    case "SetZ":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Z = Double.Parse(txtZ.Text);
                        }
                        break;
                    case "SetRotation":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Rotation = Double.Parse(txtRotation.Text);
                        }
                        break;
                    case "SetPitch":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Pitch = Double.Parse(txtPitch.Text);
                        }
                        break;
                    case "SetRoll":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Roll = Double.Parse(txtRoll.Text);
                        }
                        break;
                    case "SetScale":
                        foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                            i.Scale = Double.Parse(txtScale.Text);
                        }
                        break;
                }
            }
        }

        #endregion

        #region Cardinal Direction Changes

        enum DirectionChangeStyle {
            Set, 
            Change, 
        }

        private DirectionChangeStyle currentMode; 

        private void btnDirectionClick(object sender, RoutedEventArgs e) {
            if (lstGroups.SelectedItem == null) {
                return;
            }
            if (validateTextBoxes()) {
                double value = Double.Parse(txtDirection.Text);
                string btnTag = ((Button)sender).Tag.ToString();
                if (currentMode == DirectionChangeStyle.Change)
                    directionChange(btnTag, value);
                else
                    directionSet(btnTag, value);
            }
        }

        private void directionSet(string btnTag, double value) {
            if (lstGroups.SelectedItem == null) {
                return;
            }
            switch (btnTag) {
                case "Up":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Y = value;
                    }
                    break;
                case "Down":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Y = value;
                    }
                    break;
                case "North":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Z = value;
                    }
                    break;
                case "South":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Z = value;
                    }
                    break;
                case "East":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.X = value;
                    }
                    break;
                case "West":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.X = value;
                    }
                    break;
            }
        }

        private void directionChange(string btnTag, double value) {
            if (lstGroups.SelectedItem == null) {
                return;
            }
            switch (btnTag) {
                case "Up":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Y += value;
                    }
                    break;
                case "Down":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Y -= value;
                    }
                    break;
                case "North":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Z -= value;
                    }
                    break;
                case "South":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.Z += value;
                    }
                    break;
                case "East":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.X -= value;
                    }
                    break;
                case "West":
                    foreach (Item i in _groups[lstGroups.SelectedItem.ToString()]) {
                        i.X += value;
                    }
                    break;
            }
        }

        private void btnSetDirectionClick(object sender, RoutedEventArgs e) {
            currentMode = DirectionChangeStyle.Set;
            btnSetDirection.Background = new SolidColorBrush(Color.FromArgb(144, 0, 255, 0));
            btnChangeDirection.Background = new SolidColorBrush(Color.FromArgb(255,255,255,255));
        }

        private void btnChangeDirectionClick(object sender, RoutedEventArgs e) {
            currentMode = DirectionChangeStyle.Change;
            btnSetDirection.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            btnChangeDirection.Background = new SolidColorBrush(Color.FromArgb(144, 0, 255, 0));
        }

        #endregion
    }
}
