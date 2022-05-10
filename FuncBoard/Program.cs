using System;
using System.Linq;
using fInt = System.Func<int>;

namespace FuncBoard
{
	class Program
	{
		static void Main(string[] args)
		{
			var board = Board<fInt>.Create(10, Factory.Unity, "Earnings", "Expenses", "Net");
			var display = Display<int>.Create(board);

			//Init(board);

			display.Draw();

			while (UserInput(board))
			{
				display.Draw();
			}	
		}

		static void Init(Board<fInt> board)
		{
			// earnings
			board.GetCell(0, 0).SetValue(Factory.Literal(1000));

			// expenses
			board.GetCell(1, 0).SetValue(Factory.Literal(-500));
			board.GetCell(1, 1).SetValue(Factory.Literal(-125));
			board.GetCell(1, 2).SetValue(Factory.Literal(-45));
			board.GetCell(1, 3).SetValue(Factory.Literal(-15));
			board.GetCell(1, 4).SetValue(Factory.Literal(-45));
			board.GetCell(1, 5).SetValue(Factory.Literal(-84));
			board.GetCell(1, 6).SetValue(Factory.Literal(-120));
			board.GetCell(1, 7).SetValue(Factory.Literal(-30));
			board.GetCell(1, 8).SetValue(Factory.Literal(-25));

			// sums
			var fEarnSum = Factory.Combine(board.Columns[0].Cells.Take(9).ToArray(), Factory.Add);
			board.GetCell(0, 9).SetValue(fEarnSum);

			var fSpendSum = Factory.Combine(board.Columns[1].Cells.Take(9).ToArray(), Factory.Add);
			board.GetCell(1, 9).SetValue(fSpendSum);

			// nets per row
			for (var row=0; row < 9; row++)
			{
				var x = board.Columns[0].Cells[row];
				var y = board.Columns[1].Cells[row];

				var rowSum = Factory.Combine(x, y, Factory.Add);
				board.GetCell(2, row).SetValue(rowSum);
			}

			// net total
			var fNetTotal = Factory.Combine(fEarnSum, fSpendSum, Factory.Add);
			board.GetCell(2, 9).SetValue(fNetTotal);
		}

		public static bool UserInput(Board<fInt> board)
		{
			Console.WriteLine();
			Console.WriteLine("SetValue: sv <column> <row> <value>");
			Console.WriteLine("SetFormula: sf <column> <row> <Add|Subtract> <col1>,<row1> <col2>,<row2> <colN>,<rowN>");
			Console.WriteLine("Exit: bye");

			var userInput = Console.ReadLine();

			if (userInput.StartsWith("sv"))
			{ 
				SetValue(board, userInput);
				return true;
			}
			else
				if (userInput.StartsWith("sf"))
				{
					SetFormula(board, userInput);
					return true;
				}
				else
					return userInput.IndexOf("bye") < 0 || string.Empty.Equals(userInput.Trim());
		}

		public static void SetValue(Board<fInt> board, string userInput)
		{
			var parts = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();

			var col = int.Parse(parts[0]);
			var row = int.Parse(parts[1]);
			var val = int.Parse(parts[2]);

			board.GetCell(col, row).SetValue(() => val);
		}

		public static void SetFormula(Board<fInt> board, string userInput)
		{
			var parts = userInput.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();

			var col = int.Parse(parts[0]);
			var row = int.Parse(parts[1]);

			var formulaName = parts[2];

			System.Func<int, int, int> formula = null;

			switch (formulaName)
			{
				case "Add":
					formula = Factory.Add;
					break;

				case "Subtract":
					formula = Factory.Subtract;
					break;
			}

			var cells = parts.Skip(3).Select(part => 
			{
				var xy = part.Split(',');
				var cell = board.GetCell(int.Parse(xy[0]), int.Parse(xy[1]));

				return cell;
			});

			var valueFunc = Factory.Combine(cells, formula);

			board.GetCell(col, row).SetValue(valueFunc);
		}
	}
}
