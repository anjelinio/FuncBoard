using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FuncBoard
{
	class Display<T>
	{
		public Board<Func<T>> Board { get; private set; }

		public static Display<T> Create(Board<Func<T>> board)
		{
			return new Display<T>()
			{
				Board = board
			};
		}

		public void Draw()
		{
			Console.Clear();

			var headers = string.Join(" | ", Board.Columns.Select(c => c.Name));
			Console.WriteLine(headers);

			for (var r=0; r < Board.NoOfRows; r++)
			{
				var row = string.Join("   |   ", Board.Columns.Select(c => c.GetCell(r).Value()()));
				Console.WriteLine(row);
			}
		}
	}
}
