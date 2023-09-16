namespace BlazorMasterMind.Modeles;
using System.ComponentModel;


public enum ColorIndicator
{
    [Description("green")]
    Success,

    [Description("orange")]
    Warning,

    [Description("red")]
    Error
}
