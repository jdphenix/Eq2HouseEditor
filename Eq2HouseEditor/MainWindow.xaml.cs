﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using LayoutSpace;
using Microsoft.Win32;

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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Layout _workingLayout;
        private string _workingPath; 
        private bool _isDirty;
        private Storyboard _storyboard; 

        public MainWindow() {
            InitializeComponent();
        }

        #region Primary GUI

        protected void UpdateStatusBar(String s) {
            if (s != null) {
                this.statusBarLabel.Content = s;
            }
        }

        #endregion

        #region Spreadsheet GUI

        private GridViewColumnHeader _CurrentSortColumn = null;

        private void SortClick(object sender, RoutedEventArgs e) {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            Debug.Assert(column != null, "column != null");
            string field = column.Tag as string;

            if (_CurrentSortColumn != null) {
                lstItems.Items.SortDescriptions.Clear(); 
            }

            ListSortDirection newDirection = ListSortDirection.Ascending;
            if (_CurrentSortColumn == column) newDirection = ListSortDirection.Descending;

            _CurrentSortColumn = column; 
            lstItems.Items.SortDescriptions.Add(new SortDescription(field, newDirection));
        }
        
        protected void BindListView() {
            if (_workingLayout.Items != null) {
                lstItems.ItemsSource = _workingLayout.Items;
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e) {
            if (sender is TextBlock) {
                ((TextBlock)sender).Background = new SolidColorBrush(Color.FromArgb(82,0,255,0));
            }
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e) {
            if (sender is TextBlock) {
                ((TextBlock)sender).Background = new SolidColorBrush(Color.FromRgb(255,255,255));
            }
        }

        #endregion

        #region File IO Operations
        private void LoadLayoutFile() {
            var openDlg = new OpenFileDialog {Title = "Pick an EverQuest II Layout File"};
            if ((bool) openDlg.ShowDialog()) {
                _workingLayout = LayoutLoader.LoadLayout(openDlg.FileName);
                UpdateStatusBar("Opened file " + openDlg.FileName);
                BindListView();
                _workingPath = openDlg.FileName;
            } else {
                UpdateStatusBar("File open cancelled");
            }
        }

        private void SaveLayoutFile(bool openDialog) {
            if (openDialog) {
                var saveDlg = new SaveFileDialog {Title = "Save EverQuest II Layout File"};
                if ((bool) saveDlg.ShowDialog()) {
                    LayoutSaver.SaveLayout(saveDlg.FileName, _workingLayout);
                    UpdateStatusBar("Save successful, reloading layout file...");
                    _workingLayout = LayoutLoader.LoadLayout(saveDlg.FileName);
                    UpdateStatusBar("Opened file " + saveDlg.FileName);
                    _workingPath = saveDlg.FileName;
                    BindListView();
                    animateSuccessfulSave(statusBar);
                }
            } else {
                LayoutSaver.SaveLayout(_workingPath, _workingLayout);
                UpdateStatusBar("Save successful, reloading layout file...");
                _workingLayout = LayoutLoader.LoadLayout(_workingPath);
                UpdateStatusBar("Opened file " + _workingPath);
                BindListView();
                animateSuccessfulSave(statusBar);
                _isDirty = false; 
            }
        }

        private void animateSuccessfulSave(Control c) {
            //TODO: Ugly, repulsive pop-up dialog. Fix. 
            MessageBox.Show("Save successful!\n" + _workingPath);
            /*if (_storyboard == null) {
                _storyboard = new Storyboard();
            }
            ColorAnimation cmdAni = new ColorAnimation();
            cmdAni.From = ((SolidColorBrush) c.Background).Color;
            cmdAni.To = Color.FromRgb(0, 255, 0);
            cmdAni.Duration = TimeSpan.FromSeconds(1);
            _storyboard.Children.Add(cmdAni);
            Storyboard.SetTarget(cmdAni, c.Background);
            Storyboard.SetTargetProperty(cmdAni, new PropertyPath(SolidColorBrush.ColorProperty));
            _storyboard.Begin(this);*/
        }
        #endregion

        #region Menu GUI
        private void menuCopyFile_Click(object sender, RoutedEventArgs e) {
            
        }

        private void menuSave_Click(object sender, RoutedEventArgs e) {
            SaveLayoutFile(false);
        }

        private void menuSaveAs_Click(object sender, RoutedEventArgs e) {
            SaveLayoutFile(true);
        }

        private void menuExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void menuCopy_Click(object sender, RoutedEventArgs e) {

        }

        private void menuPaste_Click(object sender, RoutedEventArgs e) {

        }

        private void menuUndo_Click(object sender, RoutedEventArgs e) {

        }

        private void menuRedo_Click(object sender, RoutedEventArgs e) {

        }

        private void menuOpen_Click(object sender, RoutedEventArgs e) {
            LoadLayoutFile();
        }
        #endregion

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            _isDirty = true;
            
        }

        #region Group Editing GUI

        private void btnCreateGroup_Click(object sender, RoutedEventArgs e) {
            var selectedItems = lstItems.SelectedItems;
            CreateGroupWindow cgWin = new CreateGroupWindow();
            cgWin.ShowDialog();
            string name = cgWin.Name; 
            foreach(Item i in selectedItems) {
                i.AddGroup(name);
            }
        }

        private void btnEditGroup_Click(object sender, RoutedEventArgs e) {
            GroupEditWindow geWin = new GroupEditWindow();
            geWin.Items = _workingLayout.Items;
            geWin.ShowDialog();
        }
        #endregion
    }
}
