using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableForGto.Models.ColumnTypes;

namespace TableForGto.Models.Columns
{
    public class ResultColumn: Column
    {
        public IColumnType Type { get; set; } = new TextColumn();
    }
}
