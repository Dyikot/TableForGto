using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableForGto.Models;

namespace TableForGto.Serializers
{
	public class ProjectSerializer
	{
		private const string Name = "ProjectsInfo.tgto.projects";

		private static FileInfo _fileInfo = new($"{Environment.CurrentDirectory}\\{Name}");

		public static void Serialize(IEnumerable<ProjectInfo> projects)
		{
			File.WriteAllLines(
				_fileInfo.FullName, 
				projects.Select(project => project.FullName)
			);
		}

		public static IEnumerable<ProjectInfo> Deserialize()
		{
			return _fileInfo.Exists ?
				File.ReadAllLines(_fileInfo.FullName)
				.Select(line => new ProjectInfo(line))
				.OrderByDescending(project => project.LastAccessTime):
				Enumerable.Empty<ProjectInfo>();
		}
	}
}
