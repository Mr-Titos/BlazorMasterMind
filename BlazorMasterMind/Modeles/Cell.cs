namespace BlazorMasterMind.Modeles;

public class Cell
{
    public ColorIndicator ColorIndicator { get; set; }

    public ColorMasterMind ColorMasterMind { get; set; }

    public Cell(ColorMasterMind colorM, ColorIndicator colorI)
    {
        ColorIndicator = colorI;
        ColorMasterMind = colorM;
    }

    public Cell(ColorMasterMind colorM) => ColorMasterMind = colorM;
}

