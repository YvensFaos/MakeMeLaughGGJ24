using UnityEngine;
using Utils;

public class DataPackageController : AgnosticCollisionSolver
{
    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float velocity;
    [SerializeField] 
    private LayerMask receptorLayers;
    [SerializeField] 
    private int dataValue;

    private void Awake()
    {
        AssessUtils.CheckRequirement(ref body, this);
    }

    private void Start()
    {
        body.velocity = direction * velocity;
    }

    public void Initialize(Vector3 newDirection, float newVelocity, int newDataValue = 1)
    {
        direction = newDirection;
        velocity = newVelocity;
        dataValue = newDataValue;
    }
    
    protected override void Solve(GameObject collidedWith)
    {
        if (LayerHelper.CheckLayer(collidedWith.layer, receptorLayers)) return;
        MainFrame.GetSingleton().CollectData(this);
        Destroy(gameObject);
    }

    public int DataValue() => dataValue;
}
