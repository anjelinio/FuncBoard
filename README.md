# FuncBoard
A little silly spreadsheet made with function composition

![alt text](https://github.com/anjelinio/FuncBoard/blob/master/FuncBoard1.png?raw=true)

![alt text](https://github.com/anjelinio/FuncBoard/blob/master/FuncBoard2.png?raw=true)

![alt text](https://github.com/anjelinio/FuncBoard/blob/master/FuncBoard3.png?raw=true)


```
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
```
