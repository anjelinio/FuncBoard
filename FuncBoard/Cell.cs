using System;
using System.Collections.Generic;
using System.Text;

namespace FuncBoard
{
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
