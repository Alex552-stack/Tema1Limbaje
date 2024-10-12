using Tema1Limbaje.Models;

namespace Tema1Limbaje.Logic;

public class ContextFreeGrammer
{
    public ContextFreeGrammer(InputModel data, bool DeriveLeft)
    {
        Data = data;
        LeftDerivation = DeriveLeft;
    }

    public InputModel Data { get; }
    public bool LeftDerivation { get; }

    private readonly Random random = new();
    private readonly int maxNrOfDerivations = 30;

    public void Derive()
    {
        if (!IsContextFreeGrammer())
            throw new Exception("Inputul nu este corect");
        Console.Write(Data.StartSymbol);
        string rule = "";
        string currentString = Data.StartSymbol;
        int index = GetNextNonTerminal(currentString);
        int ct = 0;
        while (index != -1 && ct < maxNrOfDerivations)
        {
            PrintWithHighlightAt(index, currentString);
            rule = GetProductionFor($"{currentString[index]}");
            if (index != 0)
                currentString = $"{currentString.Substring(0, index)}{rule}{currentString.Substring(index + 1)}";
            else
                currentString = $"{rule}{currentString.Substring(1)}";

            index = GetNextNonTerminal(currentString);
            ct++;
            //PrintWithHighlightAt(index,currentString);
        }
        if(index != -1)
        {
            Console.Write('\n');
            Derive();
        }
        else if(ct != maxNrOfDerivations) Console.Write("<-" + currentString);
    }

    public string GetProductionFor(string nonTerminal)
    {
        int nrOfRules = Data.Productions[nonTerminal].Count();
        if(nrOfRules == 0)
            Console.Write("You fucked up");
        return Data.Productions[nonTerminal][random.Next(nrOfRules)];
    }
    public void PrintWithHighlightAt(int index, string output)
    {
        
            Console.Write("<-");
            if(index != 0)
                Console.Write(output.Substring(0, index));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(output.Substring(index, 1));
            Console.ResetColor();
            Console.Write(output.Substring(index + 1));
        
        
    }

    public int GetNextNonTerminal(string currentString)
    {
        for (int i = 0; i < currentString.Length; i++)
        {
            if (Data.NonTerminals.Contains($"{currentString[i]}"))
                return i;
        }

        return -1;
    }
    
    
    public bool IsContextFreeGrammer()
    {
        return Data.Productions.All(p => p.Key.Length == 1 && Data.NonTerminals.Contains(p.Key));
    }
}