using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CommandLineParser : MonoBehaviour
{
    [SerializeField]
    private ConsoleController console;
    [SerializeField] 
    private TMP_Text consoleInput;
    
    public void GetCommand(string command)
    {
        console.gameObject.SetActive(true);
        AddConsoleLine(command,">");
        ParseCommand(command);
        consoleInput.text = "";
    }

    private void ParseCommand(string command)
    {
        command = command.Trim();
        
        if (command.Equals("-g"))
        {
            //Open Gate
            AddConsoleLine("Try to open gate.","");
        }
        else if (command.StartsWith("-c"))
        {
            //Spawn Receptors
            var regex = new Regex(@"\d+");
            var match = regex.Match(command);
            if (match.Success)
            {
                var count = int.Parse(match.Value);
                AddConsoleLine($"Spawn {count} receptors.","");
            }
            else
            {
                AddConsoleLine($"Invalid command.","");
            }
        }
        else
        {
            AddConsoleLine($"Invalid command.","");
        }
    }

    private void AddConsoleLine(string line, string start = "")
    {
        console.AddConsoleLine(line, start);
    }
}