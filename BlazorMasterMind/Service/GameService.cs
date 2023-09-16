using BlazorMasterMind.Extensions;
using BlazorMasterMind.Modeles;

namespace BlazorMasterMind.Service;

public class GameService
{
    public List<Cell[]> RowsGuessed { get; set; } = new(); 

    private Cell[] RowToGuess { get; set; }

    public readonly int RowSize = 5;

    public Cell[] GenerateRandomRow()
    {
        Cell[] cells = new Cell[RowSize];
        ColorMasterMind[] colors = (ColorMasterMind[])Enum.GetValues(typeof(ColorMasterMind));

        for (int i = 0; i < RowSize; i++)
        {
            cells[i] =(new Cell(colors[Random.Shared.Next(0, colors.Length)]));
        }
        return cells;
    }

    public void StartGame()
    {
        RowToGuess = GenerateRandomRow();
        Console.WriteLine("--------------");
        foreach (Cell cell in RowToGuess)
        {
            Console.WriteLine(cell.ColorMasterMind.GetDescription());
        }
    }

    public void ProcessRow(Cell[] cells)
    {

        foreach (Cell cell in cells)
        {
            /*if (cell.)
            {

            }*/
        }
    }
}
