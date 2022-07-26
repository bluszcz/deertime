using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ReactiveUI;
using DeerTime.ViewModels;
namespace DeerTime.Models
{
    public class TaskItem : ViewModelBase
    {
        public bool isActive = false;
        int curWork;
        int spentTime;
        public int task_id;
        bool isSelected = false;
        bool isDisplayed = true;
        public string Description { get; set; } = "a";
        public int taskID
        {
            get => task_id;
            set => this.RaiseAndSetIfChanged(ref task_id, value);
        }
        public int CurWork
        {
            get => curWork;
            set => this.RaiseAndSetIfChanged(ref curWork, value);
        }
        public int SpentTime
        {
            get => spentTime;
            set => this.RaiseAndSetIfChanged(ref spentTime, value);
        }
        public bool IsDone { get; set; }
        // public int SpentTime {get; set; } = 0;
        public bool IsActive
        {
            get => isActive;
            set => this.RaiseAndSetIfChanged(ref isActive, value);
        }
        public bool IsDisplayed
        {
            get => isDisplayed;
            set => this.RaiseAndSetIfChanged(ref isDisplayed, value);
        }
        public bool IsSelected
        {
            get => isSelected;
            set => this.RaiseAndSetIfChanged(ref isSelected, value);
        }
        public DateTimeOffset LastStart { get; set; }
        // void updateSpentTime() {
        //     curWork = (DateTimeOffset.Now-LastStart).Minutes;
        //     if (IsActive==false) {
        //     } else { 
        //      updateSpentTime();
        //     }
        // }
        
        public  TaskItem()
        {
                var _updateFMT = this.WhenAnyValue(
                y => y.SpentTime
                ).Subscribe(y => updateFormattedST());
        }
        string formattedSpentTime = "";

        public string FormattedSpentTime
        {
            get => formattedSpentTime;
            set => this.RaiseAndSetIfChanged(ref formattedSpentTime, value);
        }
        public string updateFormattedST() {
            string outputText = "";
            if (SpentTime<61){
                outputText = $"{SpentTime} min";
            } else
            {
                int spentHours = ((int)(SpentTime/60));
                int spentMinutes = SpentTime%60;
                outputText = $"{spentHours} hour[s] {spentMinutes} min";
            }
            return FormattedSpentTime = outputText;
        }
        public void TaskPause()
        {
            IsActive = false;
            DateTimeOffset stopTime = DateTimeOffset.Now;
            TimeSpan diff = stopTime - LastStart;
            int curSpentMinutes = diff.Minutes > 0 ? diff.Minutes : 1;
            SpentTime += curSpentMinutes;
            Debug.WriteLine("Pause Item" + SpentTime);
            testEvent("SHOW");
        }
        public void TaskStart()
        {
            Debug.WriteLine("Start Item");
            LastStart = DateTimeOffset.Now;
            CurWork += 1;
            Debug.WriteLine("current Work " + CurWork);

            IsActive = true;
            testEvent("HIDE");
            // updateSpentTime();
        }

        public void testEvent(string opType)
        {

            VisibilityChangedEventArgs args = new VisibilityChangedEventArgs();
            args.Message = "Some new test message";
            args.OpType = opType;
            args.Item = this;
            OnVisibilityChanged(args);
        }


        protected virtual void OnVisibilityChanged(VisibilityChangedEventArgs e)
        {
            EventHandler<VisibilityChangedEventArgs> handler = VisibilityChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public  event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

        override public string ToString()
        {
            return "TaskItem " + Description;
        }
    }
}