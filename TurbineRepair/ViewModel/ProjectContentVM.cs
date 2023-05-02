using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurbineRepair.ViewModel
{
    internal class ProjectContentVM : Base.ViewModel
    {
        public List<ProjectDatum> Projects { get; set; }

        public class ImageSource
        {
            public string imageSource { get; set; }
        }      
        public List<ImageSource> ImageSources { get; set; }
        public ProjectContentVM() 
        { 
            Projects = MainWindowViewModel.main.ProjectData.Where(x=>x.Id == MainWindowViewModel.main.CurrentProject.Id).ToList();
            ImageSources = new List<ImageSource>();
            for(int i = 0; i < MainWindowViewModel.main.TurbineImage.Count; i++) 
            {
                if (MainWindowViewModel.main.TurbineImage[i].Id == MainWindowViewModel.main.CurrentProject.ProjectTurbineNavigation.TurbineImage)
                {
                    ImageSources.Add(new ImageSource(){ 
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
        }
    }
}
