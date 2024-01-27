using TMPro;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text consoleOutput;
    
    public void AddConsoleLine(string line, string start = "")
    {
        consoleOutput.text += $"\r\n\r\n{start}{line}";
    }
}
