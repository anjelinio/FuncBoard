using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncBoard
{
	class Board<T>
	{
		public int NoOfRows { get; private set; }

		public IEnumerable<Column<T>> Columns { get; protected set; }

		public static Board<T> Create(int noOfRows, T defaultValue, params string[] columnHeaders)
		{
			return new Board<T>()
			{
				NoOfRows = noOfRows,
				Columns = columnHeaders.Select(h => Column<T>.Create(h, defaultValue, noOfRows)).ToArray()	
			};
		}

		public Cell<T> GetCell(int index, int row)
		{
			return Columns.Skip(index).First().GetCell(row);
		}
	}
}
