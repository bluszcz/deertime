using System.Collections.Generic;
using System.Collections.ObjectModel;
using DeerTime.Models;
using System.Diagnostics;
using System;
using ReactiveUI;
using Avalonia.Threading;

namespace DeerTime.ViewModels
{
    public class DeerTimeMainViewModel : ViewModelBase
    {
            string description;
            int spentTime;
            string formattedSpentTime = "";
            string curWork;
            string _fontColor="Red";
            bool isWorking=false;
            MainWindowViewModel mainMW;
            public DateTimeOffset timeOfStart;
            public DispatcherTimer dispatcherTimer;

        public DeerTimeMainViewModel(IEnumerable<TaskItem> items, MainWindowViewModel mw)
        {
            CurWork = "00:00:00";

            dispatcherTimer = new Avalonia.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            // dispatcherTimer.Start();

            Items = new ObservableCollection<TaskItem>(items);
            mainMW =  mw;
            var okEnabled = this.WhenAnyValue(
                x => x.SelectedItem
                ).Subscribe(x => updateSelected(x));

 
        }

        void updateSelected(TaskItem ti)

        {
            mainMW.updateProperElem(ti);
        }
     

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            // lblSeconds.Content = DateTime.Now.Second;

            // Forcing the CommandManager to raise the RequerySuggested event
            // CommandManager.InvalidateRequerySuggested();

            TimeSpan dateDiff = DateTimeOffset.Now - timeOfStart;

            CurWork = $"{dateDiff.Hours.ToString().PadLeft(2,'0'),2}:{dateDiff.Minutes.ToString().PadLeft(2,'0'),2}:{dateDiff.Seconds.ToString().PadLeft(2,'0'),2}";
            
        }

        TaskItem selectedItem;
        public TaskItem SelectedItem
        {
            get => selectedItem;
            set => this.RaiseAndSetIfChanged(ref selectedItem, value);
        }
        public string CurWork
        {
            get => curWork;
            set => this.RaiseAndSetIfChanged(ref curWork, value);
        }

        public bool IsWorking
        {
            get => isWorking;
            set => this.RaiseAndSetIfChanged(ref isWorking, value);
        }
        public void AddItem()
        {
            if ((this.Description!="")&&(this.description!=null))
            {
                Debug.WriteLine("Test!2 -> calling addItemEvent for: " + this.Description); 
                Debug.WriteLine(this.Description.Length);
                var NewItem = new TaskItem {Description = Description};
                addItemEvent(NewItem);
                
            } else
            {
                Debug.WriteLine("Empty string");
            };
        }
        // }
        // public void TaskStart()
        // {
        //     Debug.WriteLine("Start");
        // }

        // public void TaskPause()
        // {
        //     Debug.WriteLine("Pause");
        // }
        public ObservableCollection<TaskItem> Items { get; }
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }
        
        public int SpentTime
        {
            get => spentTime;
            set => this.RaiseAndSetIfChanged(ref spentTime, value);
        }





        public string FontColor
        {
            get => _fontColor;
            set => this.RaiseAndSetIfChanged(ref _fontColor, value);
        }


        public void addItemEvent(TaskItem newItem)
        {

                NewObjectAddedEventArgs args = new NewObjectAddedEventArgs();
                args.Item = newItem;
                this.Description = "";
                OnNewObjectAdded(args);
        }


        protected virtual void OnNewObjectAdded (NewObjectAddedEventArgs e)
        {
            EventHandler<NewObjectAddedEventArgs> handler = NewObjectAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<NewObjectAddedEventArgs> NewObjectAdded;    

    }
}