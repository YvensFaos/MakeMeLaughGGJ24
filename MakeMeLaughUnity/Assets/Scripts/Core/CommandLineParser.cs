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

        switch (command)
        {
            case "-g":
                //Open Gate
                AddConsoleLine("Try to open gate.");
                var gateOpened = MainFrame.GetSingleton().ConvertRandomReceptorToGate();
                if (!gateOpened)
                {
                    AddConsoleLine("Not enough receptors.", "!");
                }
                break;
            case "-h":
                MainFrame.GetSingleton().ToggleOverlayPanel();
                break;
            default:
            {
                if (command.StartsWith("-c"))
                {
                    //Spawn Receptors
                    var regex = new Regex(@"\d+");
                    var match = regex.Match(command);
                    if (match.Success)
                    {
                        var count = int.Parse(match.Value);
                        AddConsoleLine($"Spawn {count} receptors.","");
                        var success = MainFrame.GetSingleton().TrySpawnReceptor(count, out var generated);
                
                        if(success) AddConsoleLine($"return 0;","$");
                        else AddConsoleLine($"return -{Random.Range(0, 6000)};","!");
                    }
                    else
                    {
                        AddConsoleLine($"Invalid command.");
                    }
                }
                else
                {
                    AddConsoleLine($"Invalid command.");
                }

                break;
            }
        }
    }

    private void AddConsoleLine(string line, string start = "")
    {
        console.AddConsoleLine(line, start);
    }
}