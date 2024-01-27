using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Collider))]
    public class Box3DGizmoViewer : MonoBehaviour
    {
        [SerializeField]
        private Collider selfCollider;
        [SerializeField]
        private Color colliderColor = Color.white;
        private void Awake()
        {
            AssessUtils.CheckRequirement(ref selfCollider, this);
        }

        private void OnDrawGizmos()
        {
            if (selfCollider == null) return;
    
            Gizmos.color = colliderColor;
            
            var position = new Vector3(selfCollider.bounds.center.x, selfCollider.bounds.center.y, 0);
            Gizmos.DrawWireCube(position, selfCollider.bounds.size);
        }
    }
}
