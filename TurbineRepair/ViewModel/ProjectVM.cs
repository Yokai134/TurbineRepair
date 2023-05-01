using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurbineRepair.Migration;
using TurbineRepair.Test;

namespace TurbineRepair.ViewModel
{
    internal class ProjectVM : Base.ViewModel
    {
        TurbinerepairContext context = new TurbinerepairContext();

        public List<ProjectDatum> Projects { get; set; }

        public ProjectVM() 
        {
            Projects = MainWindowViewModel.main.ProjectData.Where(x=>x.ProjectExecutor == MainWindowViewModel.main.CurrentUser.Id).ToList();                              
        }
    }
}
