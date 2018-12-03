using System.Windows;
using System.Windows.Input;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Map2D.ViewModel
{
    public class CreateViewModel : ViewModelBase
    {
        public ICommand CreateCommand { get; set; }

        public int Rows { get; set; } = 16;

        public int Columns { get; set; } = 16;

        public CreateViewModel()
        {
            CreateCommand = new RelayCommand<Window>(Create);
        }

        private void Create(Window window)
        {
            var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            mainViewModel.InitializeMap(Rows, Columns);
            window.Close();
        }
    }
}