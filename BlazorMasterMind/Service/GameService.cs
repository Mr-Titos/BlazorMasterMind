using BlazorMasterMind.Extensions;
using BlazorMasterMind.Modeles;

namespace BlazorMasterMind.Service;

public class GameService
{
    public int NbGuess { get; set; } = 7;

    public int RowSize { get; set; } = 5;
    public List<Cell[]> RowsGuessed { get; set; } = new();

    private Cell[] RowToGuess { get; set; }

    private Dictionary<ColorMasterMind, int> NumberColors = new Dictionary<ColorMasterMind, int>();

    private int NbGuessed { get; set; } = 0;


    public Cell[] GenerateRandomRow()
    {
        Cell[] cells = new Cell[RowSize];
        ColorMasterMind[] colors = (ColorMasterMind[])Enum.GetValues(typeof(ColorMasterMind));

        for (int i = 0; i < RowSize; i++)
        {
            cells[i] = new Cell(colors[Random.Shared.Next(0, colors.Length)]);
        }
        return cells;
    }

    public Cell[] GetNewRow()
    {
        Cell[] cells = new Cell[RowSize];

        for (int i = 0; i < RowSize; i++)
        {
            cells[i] = new Cell(ColorMasterMind.Gray);
        }
        return cells;
    }

    public void StartGame()
    {
        RowsGuessed.Clear();
        NumberColors.Clear();
        RowToGuess = GenerateRandomRow();
        ColorMasterMind[] colors = (ColorMasterMind[])Enum.GetValues(typeof(ColorMasterMind));
        foreach (ColorMasterMind color in colors)
        {
            NumberColors.Add(color, RowToGuess.Where(c => c.ColorMasterMind == color).Count());
        }

        CheatIfDEBUG();
    }

    private void CheatIfDEBUG()
    {
        bool isDebug = false;
#if DEBUG
        isDebug = true;
#endif
        if (isDebug)
        {
            Console.WriteLine("--------------");
            foreach (Cell cell in RowToGuess)
            {
                Console.WriteLine(cell.ColorMasterMind.GetDescription());
            }
        }
    }

    public bool ProcessRow(Cell[] cells, out bool win)
    {
        Dictionary<ColorMasterMind, int> copyCounter = NumberColors.ToDictionary(nbC => nbC.Key, nbC => nbC.Value);
        foreach (Cell cell in cells)
        {
            cell.ColorIndicator = ColorIndicator.Error;

            // If there is this color in the row and in the good index to find...
            if (RowToGuess.ElementAt(Array.IndexOf(cells, cell)).ColorMasterMind == cell.ColorMasterMind)
            {
                cell.ColorIndicator = ColorIndicator.Success;
                copyCounter[cell.ColorMasterMind]--;
            }
            // Else If there is still this color in the row to find...
            else if (copyCounter.TryGetValue(cell.ColorMasterMind, out int i))
            {
                if (i > 0)
                {
                    copyCounter[cell.ColorMasterMind]--;
                    cell.ColorIndicator = ColorIndicator.Warning;
                }
            }
        }
        // Remove copies of indicators -> There are cases (when the counter is bypassed in the earlier foreach)
        // where there are 1 or more colors in the warning indicator instead of the error one.
        var invalidCountersKeys = copyCounter.Where(cc => cc.Value < 0).ToDictionary(nbC => nbC.Key, nbC => nbC.Value).Keys;
        foreach (Cell cell in cells.Where(cell => invalidCountersKeys.Any(cKey => cKey == cell.ColorMasterMind) && cell.ColorIndicator == ColorIndicator.Warning))
        {
            cell.ColorIndicator = ColorIndicator.Error;
        };

        RowsGuessed.Insert(0, cells);
        NbGuessed++;
        bool lastAttempt = NbGuessed >= NbGuess;
        win = cells.All(c => c.ColorIndicator == ColorIndicator.Success);
        return lastAttempt || win;
    }
}