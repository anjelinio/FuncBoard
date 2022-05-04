using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using fInt = System.Func<int>;
using fIntCombinator = System.Func<System.Func<int>, System.Func<int>, System.Func<int>>;

namespace Monoid.Tests
{
	static class Factory
	{
		public static System.Func<T> Literal<T>(T value)
		{
			return () =>
			{
				return value;
			};
		}

		public static Func<T> Combine<T>(Func<Func<T>, Func<T>, Func<T>> combinator, Func<T> f1, Func<T> f2)
		{
			return combinator(f1, f2);
		}

		public static Func<T> Combine<T>(Func<Func<T>, Func<T>, Func<T>> combinator, params Func<T>[] f)
		{
			return () =>
			{
				var retVal = f.First();

				foreach (var func in f.Skip(1))
					retVal = combinator(retVal, func);

				return retVal();
			};
		}
	}
}
