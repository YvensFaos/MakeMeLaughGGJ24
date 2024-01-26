using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

public class ReceptorController : MonoBehaviour
{
    [SerializeField]
    private List<KeyCodeDirectionPair> commandKeys;
    [SerializeField]
    private float force = 10;
    [SerializeField] 
    private Rigidbody body;
    [SerializeField] 
    private List<CommandNotifier> notifiers;

    private void Awake()
    {
        AssessUtils.CheckRequirement(ref body, this);
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
}
