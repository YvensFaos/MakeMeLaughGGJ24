using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

public class ReceptorController : MonoBehaviour
{
    [SerializeField]
    private List<KeyCodeDirectionPair> commandKeys;
    [SerializeField]
    private float force = 20;
    [SerializeField] 
    private Rigidbody body;
    [SerializeField] 
    private List<CommandNotifier> notifiers;

    private void Awake()
    {
        AssessUtils.CheckRequirement(ref body, this);
    }

    private void Start()
    {
        MainFrame.GetSingleton().AddNewReceptor(this);
    }

    public void Initialize(List<KeyCode> keys)
    {
        commandKeys = new List<KeyCodeDirectionPair>();
        keys.ForEach(key =>
        {
            commandKeys.Add(new KeyCodeDirectionPair(key, RandomPointUtils.GenerateRandomDirection2Din3D()));
        });
    }

    private void Update()
    {
        commandKeys.ForEach(keyDirection =>
        {
            if (!Input.GetKeyDown(keyDirection.One)) return;
            body.AddForce(keyDirection.Two * force);
            notifiers.ForEach(n => n.NotifyMeDown(gameObject));
        });
        commandKeys.ForEach(keyDirection =>
        {
            if (!Input.GetKeyUp(keyDirection.One)) return;
            notifiers.ForEach(n => n.NotifyMeUp(gameObject));
        });
    }

    public override string ToString()
    {
        var commands = "";
        commandKeys.ForEach(commandKey =>
        {
            commands += $"{commandKey.One.ToString()}  | ";
        });
        commands = commands[..^4];
        return commands;
    }
}