using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DeerTime.Views
{
    public partial class AddItemView : UserControl
    {
        public AddItemView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}