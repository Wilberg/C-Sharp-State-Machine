using UnityEngine;

namespace Movement.States
{
    public class FallingState : MovementState
    {
        public FallingState(MovementBehaviour movement) : base(movement)
        {
        }


        public override void OnAction(MovementAction action)
        {
            if (action == MovementAction.Jump)
            {
                Movement.rigidbody.velocity += Vector3.down * 10.0f;
            }
        }

        public override void OnEnter()
        {
            Debug.Log("Entered Falling");
        }
        
        public override void OnExit()
        {
            Debug.Log("Exited Falling");
        }
    }
}