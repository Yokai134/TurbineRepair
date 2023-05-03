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
        public static ProjectVM projectVM;


        private ProjectDatum _selectedProject;
        public ProjectDatum SelectedProject
        {
            get => _selectedProject;
            set => Set(ref _selectedProject,value);
        }
        public List<ProjectDatum> Projects { get; set; }

        public ProjectVM() 
        {
            if (MainWindowViewModel.main.CurrentUser.Role != 1)
            {
                Projects = MainWindowViewModel.main.ProjectData.Where(x => x.ProjectExecutor == MainWindowViewModel.main.CurrentUser.Id).ToList();
            }

            Projects = MainWindowViewModel.main.ProjectData;
          
            projectVM = this;
            
        }
    }
}
