using UnityEngine;
using Utils;

public class AntivirusController : AgnosticCollisionSolver
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
    private int threatValue;

    private void Awake()
    {
        AssessUtils.CheckRequirement(ref body, this);
    }

    private void Start()
    {
        body.velocity = direction * velocity;
    }

    public void Initialize(Vector3 newDirection, float newVelocity)
    {
        direction = newDirection;
        velocity = newVelocity;
    }
    
    protected override void Solve(GameObject collidedWith)
    {
        if (!LayerHelper.CheckLayer(receptorLayers, collidedWith.layer)) return;
        MainFrame.GetSingleton().DestroyReceptor(collidedWith.GetComponent<ReceptorController>(), threatValue);
        Destroy(gameObject);
    }
}
