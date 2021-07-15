using UnityEngine;

namespace Movement.States
{
    public class JumpingState : MovementState
    {
        public JumpingState(MovementBehaviour movement) : base(movement)
        {
        }

        public override void OnAction(MovementAction action)
        {
            if (action == MovementAction.Jump)
            {
                Movement.rigidbody.velocity += Vector3.down * 5.0f;
            }
        }

        public override void OnEnter()
        {
            Debug.Log("Entered Jumping");
        }

        public override void OnExit()
        {
            Debug.Log("Exited Jumping");
        }
    }
}