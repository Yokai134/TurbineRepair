using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class ProjectVM : Base.ViewModel
    {

        public ObservableCollection<ProjectInfo> Projects { get; set; }

        public ProjectVM() 
        { 
            Projects = new ObservableCollection<ProjectInfo>();

            for(int i = 0; i < 5; i++) 
            {
                Projects.Add(new ProjectInfo()
                {
                    nameProject = $"test{i}",
                    executor = $"test{i}",
                    status = $"test{i}",
                    dateStart = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    dateEnd = Convert.ToDateTime(DateTime.Now.ToShortDateString())
                });
            }
           
            
           
        }
    }
}
