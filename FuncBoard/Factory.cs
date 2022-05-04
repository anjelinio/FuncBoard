using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using fInt = System.Func<int>;

namespace FuncBoard
{
	static class Factory
	{
		public static fInt Unity = () => 0;

		public static Func<int, int, int> Add => (x, y) => x + y;

		public static Func<int, int, int> Subtract => (x, y) => x - y;

		public static fInt Combine(Cell<fInt> x, Cell<fInt> y, Func<int, int, int> combine)
		{
			return () => combine(x.Value()(), y.Value()());
		}

		public static fInt Combine(IEnumerable<Cell<fInt>> cells, Func<int, int, int> combine)
		{
			var retVal = Combine(cells.First(), cells.Skip(1).First(), combine);

			foreach (var cell in cells.Skip(2).ToArray())
			{
				var buffer = retVal;
				retVal = () => combine(buffer(), cell.Value()());	
			}

			return retVal;
		}
	}
}
