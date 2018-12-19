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

        public Visibility RowsErrorVisibility { get; set; } = Visibility.Hidden;
        public Visibility ColumnsErrorVisibility { get; set; } = Visibility.Hidden;
        public CreateViewModel()
        {
            CreateCommand = new RelayCommand<Window>(Create);
        }

        private void Create(Window window)
        {
            if (Rows > 4 && Rows <= 16 && Columns > 4 && Columns <= 16)
            {
                var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
                mainViewModel.InitializeMap(Rows, Columns);
                window.Close();
            }
            if (Rows > 4 && Rows <= 16)
            {
                RowsErrorVisibility = Visibility.Hidden;
            }
            else
            {
                RowsErrorVisibility = Visibility.Visible;
            }
            RaisePropertyChanged(() => RowsErrorVisibility);

            if (Columns > 4 && Columns <= 16)
            {
                ColumnsErrorVisibility = Visibility.Hidden;
            }
            else
            {
                ColumnsErrorVisibility = Visibility.Visible;
            }
            RaisePropertyChanged(() => ColumnsErrorVisibility);
        }
    }
}