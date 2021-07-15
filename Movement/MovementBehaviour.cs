using Movement.States;
using UnityEngine;

namespace Movement
{
    public class MovementBehaviour : MonoBehaviour
    {
        public new Rigidbody rigidbody;
        
        private MovementStateMachine _machine;
     
        public bool IsGrounded { get; private set; }
        
        private void Start()
        {
            _machine = MovementStateMachine.Builder()
                .RegisterState(new IdleState(this))
                .RegisterState(new FallingState(this))
                .RegisterState(new JumpingState(this))
                .SetInitialState<IdleState>()
                    .RegisterTransition<IdleState, JumpingState>(IsJumping)
                    .RegisterTransition<FallingState, IdleState>(IsIdle)
                    .RegisterAnyTransition<FallingState>(IsFalling)
                .Build();
        }

        private void Update()
        {
            _machine.Update();
        }

        private void FixedUpdate()
        {
        }

        private void OnCollisionEnter(Collision other)
        {
            IsGrounded = true;
        }

        private void OnCollisionExit(Collision other)
        {
            IsGrounded = false;
        }

        public void Jump()
        {
            _machine.Action(MovementAction.Jump);
        }

        public void Run()
        {
            _machine.Action(MovementAction.Run);
        }

        public void Crouch()
        {
            _machine.Action(MovementAction.Crouch);
        }

        private bool IsIdle()
        {
            return IsGrounded && rigidbody.velocity.sqrMagnitude < 1.0f;
        }
        
        private bool IsJumping()
        {
            return !IsGrounded && rigidbody.velocity.y > 0.0f;
        }
        
        private bool IsFalling()
        {
            return !IsGrounded && rigidbody.velocity.y < 0.0f;
        }
    }
}