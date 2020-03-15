using UnityEngine;

namespace VisualEffects
{

    public sealed class Unit : MonoBehaviour
    {
        private Rigidbody Rigidbody;
        private Animator animator;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        /// <summary>
        /// Enable gravity and animator component
        /// </summary>
        public void Activate()
        {
            Rigidbody.useGravity = true;
            animator.enabled = true;
        }

    }
}
