using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Collider))]
    public abstract class AgnosticCollisionSolver : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Solve(other.gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            Solve(other.gameObject);
        }

        protected abstract void Solve(GameObject collidedWith);

        private void OnTriggerExit(Collider other)
        {
            SolveExit(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            SolveExit(other.gameObject);
        }

        protected virtual void SolveExit(GameObject exitWith) { }
    }
}