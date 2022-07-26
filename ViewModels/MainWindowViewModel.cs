using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using DeerTime.Services;
using ReactiveUI;
using DeerTime.Models;

namespace DeerTime.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string description;
        bool working = false;
        Database _db;
        public MainWindowViewModel(Database db)
        {
            _db = db;
            description = "AAA";
            AddDeerItem = new AddItemViewModel();
            List = new DeerTimeMainViewModel(db.GetItems(), this);
            List.NewObjectAdded += AddItem;
            for (int i = 0; i<List.Items.Count; i++) 
            {
                List.Items[i].VisibilityChanged += listItem_VisibilityChanged;
            }
        }
        
        public void updateProperElem(TaskItem ti)
        {
            Debug.WriteLine("I Will try to update with " + ti);
        }
                string updateVisibilityCheck()
        {
            return ("");
        }
        public void listItem_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            
            Debug.WriteLine("Starting update task DB "+e.Item.Description);
            _db.UpdateTask(e.Item);
            Debug.WriteLine(updateVisibilityCheck());
            Debug.WriteLine(e);

                if (e.OpType=="HIDE")
                {
                    Working = true;
                    List.SelectedItem=e.Item;
                    List.dispatcherTimer.Start();
                    List.CurWork = "00:00:00";
                    List.FontColor="Green";
                    List.IsWorking = true;
                    List.timeOfStart = DateTimeOffset.Now;

                } else if (e.OpType=="SHOW")
                {
                    List.FontColor="Red";
                    List.IsWorking = false;
                    List.dispatcherTimer.Stop();
                }


            Debug.WriteLine(
                "Vibility triggered with the message: {0} ",
                e.Message
            );
            for (int i = 0; i<List.Items.Count; i++) 
            {

                if (e.OpType=="HIDE")
                {

                    List.Items[i].IsDisplayed = List.Items[i].IsActive;
                    List.Items[i].IsSelected =  List.Items[i].IsActive;
                } else if (e.OpType=="SHOW") 
                {
                    Working = false;

                    List.Items[i].IsDisplayed = true;
                }

            }
        }
        void listItem_ShowAll(object sender, VisibilityChangedEventArgs e)
        {
            List.dispatcherTimer.Stop();
            Debug.WriteLine(
                "Vibility triggered with the message: {0} ",
                e.Message
            );

            for (int i = 0; i<List.Items.Count; i++) 
            {
                Debug.WriteLine(List.Items[i]);
                if (e.OpType=="HIDE")
                {
                    Debug.WriteLine(List.Items[i].IsActive);
                    Debug.WriteLine(List.Items[i].IsDisplayed);
                    List.Items[i].IsDisplayed = true;
                } else if (e.OpType=="HIDE") 
                {

                }
            }
        }



        public DeerTimeMainViewModel List { get; }
        public AddItemViewModel AddDeerItem { get; }

        public void AddItem(object sender, NewObjectAddedEventArgs e)
        {
            Debug.WriteLine("Adding item inside MainWindowViewModel and it is:" + e.Description);
            var NewItem = e.Item;
            NewItem.VisibilityChanged += listItem_VisibilityChanged;
            int task_id = _db.AddItemDB(NewItem);
            NewItem.taskID = task_id;
            List.Items.Insert(0, NewItem);
            NewItem.TaskStart();
        }
        public string Description
        {
            get => description;
            // set => this.RaiseAndSetIfChanged(ref description, value);
        }

        public bool Working 
        {
            get => working;
            set => this.RaiseAndSetIfChanged(ref working, value);
        }
    }

    class VisibilityChanger
    {




    }

    public class VisibilityChangedEventArgs : EventArgs
    {
        public string Message;
        public string OpType = "HIDE";
        public TaskItem Item;
        
    }

    public class NewObjectAddedEventArgs : EventArgs
    {
        public string Description;
        public TaskItem Item;
    }
}
