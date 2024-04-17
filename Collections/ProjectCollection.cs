using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableForGto.Models;

namespace TableForGto.Collections
{
	public class ProjectCollection : ObservableCollection<ProjectInfo>
	{
		public ProjectCollection(): 
			base() { }

		public ProjectCollection(IEnumerable<ProjectInfo> collection) :
			base(collection) { }

		public new void Add(ProjectInfo projectInfo)
		{
			if (!Items.Any(project => project.Name == projectInfo.Name))
			{
				Items.Add(projectInfo);
			}
		}
	}
}
