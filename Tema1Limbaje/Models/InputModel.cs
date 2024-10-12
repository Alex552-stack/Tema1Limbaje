namespace Tema1Limbaje.Models;

public class InputModel
{
    public Dictionary<string, List<string>> Productions { get; set; } = new();
    public HashSet<string> NonTerminals { get; set; } = new();
    public HashSet<string> Terminals { get; set; } = new();
    public string StartSymbol { get; set; } = "";
}