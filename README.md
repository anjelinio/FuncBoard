# FuncBoard
A little silly spreadsheet made with function composition

![alt text](https://github.com/anjelinio/FuncBoard/blob/master/FuncBoard1.png?raw=true)

![alt text](https://github.com/anjelinio/FuncBoard/blob/master/FuncBoard2.png?raw=true)

![alt text](https://github.com/anjelinio/FuncBoard/blob/master/FuncBoard3.png?raw=true)


```
static void Init(Board<fInt> board)
{
	// earnings
	board.GetCell(0, 0).SetValue(() => 1000);

	// expenses
	board.GetCell(1, 0).SetValue(() => -500);
	board.GetCell(1, 1).SetValue(() => -125);
	board.GetCell(1, 2).SetValue(() => -45);
	board.GetCell(1, 3).SetValue(() => -15);
	board.GetCell(1, 4).SetValue(() => -45);
	board.GetCell(1, 5).SetValue(() => -84);
	board.GetCell(1, 6).SetValue(() => -120);
	board.GetCell(1, 7).SetValue(() => -30);
	board.GetCell(1, 8).SetValue(() => -25);

	// sums
	var fEarnSum = Factory.Combine(board.Columns.First().Cells.Take(9).ToArray(), Factory.Add);
	board.GetCell(0, 9).SetValue(fEarnSum);

	var fSpendSum = Factory.Combine(board.Columns.Skip(1).First().Cells.Take(9).ToArray(), Factory.Add);
	board.GetCell(1, 9).SetValue(fSpendSum);

	// nets per row
	for (var row=0; row < 9; row++)
	{
		var x = board.Columns.First().Cells.Skip(row).First();
		var y = board.Columns.Skip(1).First().Cells.Skip(row).First();

		var rowSum = Factory.Combine(x, y, Factory.Add);
		board.GetCell(2, row).SetValue(rowSum);
	}

	// net total
	var fNetTotal = Factory.Combine(fEarnSum, fSpendSum, Factory.Add);
	board.GetCell(2, 9).SetValue(fNetTotal);
}
```
