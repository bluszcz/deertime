using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using ReactiveUI;

namespace DeerTime.ViewModels
{
    public class AddItemViewModel : ViewModelBase
    {
        public AddItemViewModel () {

        }
        bool working=false;
        public string Description { get; set; }
        public bool Working 
        {
            get => working;
            set => this.RaiseAndSetIfChanged(ref working, value);
        }        public void AddTaskItem()
        {
            Debug.WriteLine("Test!" + Description);
        }

        public IObservable<string> TaskName { get; }

    }
}