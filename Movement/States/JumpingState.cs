using UnityEngine;

namespace Movement.States
{
    public class JumpingState : MovementState
    {
        private int _jumps = 0;
        
        public JumpingState(MovementBehaviour movement) : base(movement)
        {
        }

        public override void OnAction(MovementAction action)
        {
            if (action == MovementAction.Jump)
            {
                _jumps++;
                if (_jumps <= 1)
                {
                    Movement.rigidbody.velocity += Vector3.up * 10.0f;   
                }
            }
        }

        public override void OnEnter()
        {
            Debug.Log("Entered Jumping");
        }

        public override void OnExit()
        {
            _jumps = 0;
            Debug.Log("Exited Jumping");
        }
    }
}