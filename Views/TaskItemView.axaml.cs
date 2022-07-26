using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DeerTime.Views
{
    public partial class TaskItemView : UserControl
    {
        public TaskItemView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}