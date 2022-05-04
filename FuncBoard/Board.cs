using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncBoard
{
	class Board<T>
	{
		public int NoOfRows { get; private set; }

		public Column<T>[] Columns { get; protected set; }

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

	class Column<T>
	{
		public string Name { get; protected set; }

		public Cell<T>[] Cells { get; protected set; }

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

	class Cell<T>
	{
		private T value;
		public Cell(T value)
		{
			this.value = value;
		}

		public void SetValue(T value)
		{
			this.value = value;
		}

		public T Value()
		{
			return this.value;
		}
	}
}
