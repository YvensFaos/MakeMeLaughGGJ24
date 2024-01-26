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

    private void Awake()
    {
        AssessUtils.CheckRequirement(ref body, this);
    }

    private void Start()
    {
        body.velocity = direction * velocity;
    }
    
    protected override void Solve(GameObject collidedWith)
    {
        if (LayerHelper.CheckLayer(collidedWith.layer, receptorLayers)) return;
        MainFrame.GetSingleton().CollectData(this);
        Destroy(gameObject);
    }
}
