using UnityEngine;

namespace Movement.States
{
    public class IdleState : MovementState
    {
        public IdleState(MovementBehaviour movement) : base(movement)
        {
        }

        public override void OnAction(MovementAction action)
        {
            if (action == MovementAction.Jump)
            {
                Movement.rigidbody.velocity += Vector3.up * Mathf.Sqrt(2.0f * Physics.gravity.magnitude);
            }
        }

        public override void OnEnter()
        {
            Debug.Log("Entered Idle");
        }

        public override void OnExit()
        {
            Debug.Log("Exited Idle");
        }
    }
}