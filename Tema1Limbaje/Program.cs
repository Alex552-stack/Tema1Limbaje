using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tema1Limbaje.Logic;
using Tema1Limbaje.Models;

System.IO.StreamReader file = new System.IO.StreamReader("input.json");

var content = file.ReadToEnd();
InputModel? input = JsonSerializer.Deserialize<InputModel>(content);
if (input is null)
{
    throw new Exception("Invalid input file format!");
}

ContextFreeGrammer grammer = new ContextFreeGrammer(input!, true);
grammer.Derive();