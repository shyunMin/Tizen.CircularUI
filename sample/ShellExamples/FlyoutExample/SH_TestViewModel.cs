using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XFColor = Xamarin.Forms.Color;

namespace FlyoutExample
{
    public class DailyTargetViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public XFColor TitleColor { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string ButtonText { get; set; }
        public XFColor ButtonColor { get; set; }
        public Command ButtonCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DailyTargetModel
    {

    }
}
