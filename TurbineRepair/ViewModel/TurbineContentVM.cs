using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurbineRepair.Infrastructure;

namespace TurbineRepair.ViewModel
{
    internal class TurbineContentVM : Base.ViewModel
    {
        public class ImageSource
        {
            public byte[] imageSource { get; set; }
        }

        public List<Turbine> Turbines { get; set; }
        public List<ImageSource> ImageSources { get; set; }


        public ICommand BackTurbineList { get; }
        private bool CanBackTurbineListExecute(object parametr) => true;
        private void OnBackTurbineListExecute(object parametr)
        {
            MainVM.mainVM.MainCurrentControl = new TurbineVM();
        }

        public TurbineContentVM()
        {

            Turbines = MainWindowViewModel.main.TurbinesAll.Where(x=>x.Id == MainWindowViewModel.main.OpenTurbine.Id).ToList();
            ImageSources = new List<ImageSource>();
            for (int i = 0; i < MainWindowViewModel.main.TurbineImage.Count; i++)
            {
                if (MainWindowViewModel.main.TurbineImage[i].Id == MainWindowViewModel.main.OpenTurbine.TurbineImage)
                {
                    ImageSources.Add(new ImageSource()
                    {
                        imageSource = MainWindowViewModel.main.TurbineImage[i].ImageOne
                    });

                    ImageSources.Add(new ImageSource()
                    {
                        imageSource = MainWindowViewModel.main.TurbineImage[i].ImageTwo
                    });

                    ImageSources.Add(new ImageSource()
                    {
                        imageSource = MainWindowViewModel.main.TurbineImage[i].ImageThree
                    });

                    ImageSources.Add(new ImageSource()
                    {
                        imageSource = MainWindowViewModel.main.TurbineImage[i].ImageFour
                    });

                }

            }
            BackTurbineList = new LambdaCommand(OnBackTurbineListExecute, CanBackTurbineListExecute);
        }
    }
}
