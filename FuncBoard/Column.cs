using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncBoard
{
	class Column<T>
	{
		public string Name { get; protected set; }

		public IEnumerable<Cell<T>> Cells { get; protected set; }

		public static Column<T> Create(string name, T defaultValue, int cellCount = 10)
		{
			return new Column<T>()
			{
				Name = name,
				Cells = Enumerable.Range(0, cellCount).Select(_ => new Cell<T>(defaultValue)).ToArray()
			};
		}

		public Cell<T> GetCell(int index)
		{
			return Cells.Skip(index).First();
		}
 	}
}
