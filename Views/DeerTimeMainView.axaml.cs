using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DeerTime.Views
{
    public partial class DeerTimeMainView : UserControl
    {
        public DeerTimeMainView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}