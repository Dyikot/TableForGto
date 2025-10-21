using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableForGto.Models
{
    public class Student
    {
        public int? Place { get; set; }
        public string FullName { get; set; } = string.Empty;
		public string Group { get; set; } = string.Empty;
        public Dictionary<string, object> Results { get; set; } = [];
	}
}
