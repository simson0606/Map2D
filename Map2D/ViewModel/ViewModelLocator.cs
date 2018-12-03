using System;
using System.Linq;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Map2D.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var args = Environment.GetCommandLineArgs();
            var path = args.FirstOrDefault(x => x.EndsWith(".m2d"));

            SimpleIoc.Default.Register(() => new MainViewModel(path));
            SimpleIoc.Default.Register<CreateViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public CreateViewModel Create => ServiceLocator.Current.GetInstance<CreateViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}