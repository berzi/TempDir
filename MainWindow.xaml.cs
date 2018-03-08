using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TempDir.backend;

namespace TempDir
{
    internal sealed partial class MainWindow
    {
        private readonly ObservableCollection<Directory> _directories = new ObservableCollection<Directory>();
        
        public MainWindow()
        {
            InitializeComponent();
            CurrentFoldersListBox.ItemsSource = _directories;
        }
        
        
        /* Enable/disable buttons needing selection depending on selection status. */
        private void CurrentFoldersListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable/disable buttons based on presence of selection.
            if (CurrentFoldersListBox.SelectedItems.Count == 0)
            {
                EditFolderButton.IsEnabled = false;
                UnmarkFolderButton.IsEnabled = false;
                MoveUpButton.IsEnabled = false;
                MoveDownButton.IsEnabled = false;
                return;
            }

            EditFolderButton.IsEnabled = true;
            UnmarkFolderButton.IsEnabled = true;
            MoveUpButton.IsEnabled = true;
            MoveDownButton.IsEnabled = true;
            
            // Enable/disable buttons based on position of selection.
            if (CurrentFoldersListBox.SelectedIndex == 0)
                MoveUpButton.IsEnabled = false;
            
            if (CurrentFoldersListBox.SelectedIndex == CurrentFoldersListBox.Items.Count - 1)
                MoveDownButton.IsEnabled = false;
        }
        
        
        /* Event handlers. */
        /* Open the MarkFolder window to allow setting up of a new TempFolder. */
        private void MarkFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: testing. Remove.
            // TODO: ensure name is unique.
            var toAdd = new Directory("Test1", "test1/");
            _directories.Insert(0, toAdd);
            
            // Select the newly added element.
            CurrentFoldersListBox.SelectedItem = toAdd;
            CurrentFoldersListBox.Focus();
        }
        
        /* Open the EditFolder window to allow editing of currently selected existing TempFolder. */
        private void EditFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Test. Insert confirm window before actual changes.
            var index = _directories.IndexOf(CurrentFoldersListBox.SelectedItem as Directory);
            var temp = _directories[index];
            temp.Name = "New name lul";
            
            // Remove the old element and replace it with the new one.
            _directories.Remove(CurrentFoldersListBox.SelectedItem as Directory);
            _directories.Insert(index, temp);
            
            // Select the new element.
            CurrentFoldersListBox.SelectedIndex = index;
            CurrentFoldersListBox.Focus();
        }
        
        /* Open the UnmarkFolder window to confirm unmarking of selected existing TempFolder. */
        private void UnmarkFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var index = CurrentFoldersListBox.SelectedIndex;

            var message = $"Are you sure you want to unmark folder \"{CurrentFoldersListBox.Items[index]}\"?";

            switch (MessageBox.Show(message, "Confirm unmarking",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No))
            {
                case MessageBoxResult.Yes:
                case MessageBoxResult.OK:
                    _directories.Remove(CurrentFoldersListBox.SelectedItem as Directory);
            
                    // Select next item in the list or the previous one if unavailable.
                    CurrentFoldersListBox.SelectedIndex = index;
                    if (CurrentFoldersListBox.SelectedItem == null)
                        CurrentFoldersListBox.SelectedIndex = index - 1;
                    CurrentFoldersListBox.Focus();
                    break;
                
                case MessageBoxResult.No:
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /* Move the selected element up the list. */
        private void MoveUpButton_Click(object sender, RoutedEventArgs e)
        {
            var index = CurrentFoldersListBox.SelectedIndex;
            
            _directories.Move(index, index-1);
            
            // Notify UI that selection has changed.
            CurrentFoldersListBox.SelectedItem = null;
            CurrentFoldersListBox.SelectedIndex = index-1;
        }

        /* Move the selected element down the list. */
        private void MoveDownButton_Click(object sender, RoutedEventArgs e)
        {
            var index = CurrentFoldersListBox.SelectedIndex;
            
            _directories.Move(index, index+1);
            
            // Notify UI that selection has changed.
            CurrentFoldersListBox.SelectedItem = null;
            CurrentFoldersListBox.SelectedIndex = index+1;
        }
    }
}