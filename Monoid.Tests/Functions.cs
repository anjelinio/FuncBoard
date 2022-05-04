using System;
using Xunit;

using intCombine = System.Func<int, int, int>;

using fint = System.Func<int>;
using fintCombine = System.Func<System.Func<int>, System.Func<int>, System.Func<int>>;

namespace Monoid.Tests
{
	public class Functions
	{
		[Fact]
		public void Function_Combination_1()
		{
			fint one = () => 1;
			fint two = () => 2;

			Func<fint, fint, fint> add = (f1, f2) => 
			{
				return () => f1() + f2();
			};

			Assert.Equal(one() + two(), add(one, two)());

			Func<fint, fint, fint> subtract = (f1, f2) =>
			{
				return () => f1() - f2();
			};

			Assert.Equal(one() - two(), subtract(one, two)());

			Func<fint, fint, intCombine, fint> combine = (f1, f2, combinator) => 
			{
				return () => combinator(f1(), f2());
			};

			var subtract2 = combine(one, two, (x, y) => x - y);

			Assert.Equal(one() - two(), subtract2());
		}

		[Fact]
		public void Function_Combination_2()
		{
			var one = Factory.Literal(1);
			var two = Factory.Literal(2);

			fintCombine add = (x, y) => () => x() + y(); 
			fintCombine subtract = (x, y) => () => x() - y();

			var addThem = Factory.Combine(add, one, two);
			var subtractThem = Factory.Combine(subtract, one, two);

			Assert.Equal(one() + two(), addThem());
			Assert.Equal(one() - two(), subtractThem());

			two = Factory.Literal(3);

			var i = subtractThem();
			Assert.NotEqual(-2, i);
		}

		class Cell<T>
		{
			private T value;
			public Cell(T value)
			{
				this.value = value;
			}

			public void SetValue(T value) => this.value = value;

			public T Value() => value;
		}

		[Fact]
		public void Function_Combination_3()
		{
			var cellOne = new Cell<int>(1);
			var cellTwo = new Cell<int>(2);

			fint one = cellOne.Value;
			fint two = cellTwo.Value;

			fintCombine add = (x, y) => () => x() + y();
			fintCombine subtract = (x, y) => () => x() - y();

			var addThem = Factory.Combine(add, one, two);
			var subtractThem = Factory.Combine(subtract, one, two);

			Assert.Equal(one() + two(), addThem());
			Assert.Equal(one() - two(), subtractThem());

			cellTwo.SetValue(3);

			var i = subtractThem();
			Assert.Equal(-2, i);
		}
	}
}
