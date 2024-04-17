using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.Models
{
	public class ProjectInfo
	{
		private readonly FileInfo _info;

		public string Name => _info.Name;

		public string FullName => _info.FullName;

		public string Extension => _info.Extension;

		public string NameOnly => Path.GetFileNameWithoutExtension(Name);

		public string? DirectoryName => _info.DirectoryName;

		public DirectoryInfo? Directory => _info.Directory;

		public DateTime LastAccessTime => _info.LastAccessTime;

		public bool Exists => _info.Exists;

		public ProjectInfo(string fileName)
		{
			_info = new FileInfo(fileName);
		}
	}
}
