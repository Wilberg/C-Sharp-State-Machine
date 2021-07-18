using Movement.States;
using States.Machine;
using UnityEngine;

namespace Movement
{
    public class MovementBehaviour : MonoBehaviour
    {
        public new Rigidbody rigidbody;
        public float strength;
        public float dampening;
        
        private MovementStateMachine _machine;
     
        public bool IsGrounded { get; private set; }
        
        private void Start()
        {
            var builder = StateMachine.Builder()
                .RegisterState(new IdleState(this))
                .RegisterState(new FallingState(this))
                .RegisterState(new JumpingState(this))
                .SetInitialState<IdleState>()
                .RegisterTransition<IdleState, JumpingState>(IsJumping)
                .RegisterTransition<FallingState, IdleState>(IsIdle)
                .RegisterAnyTransition<FallingState>(IsFalling);

            _machine = builder.Build<MovementStateMachine>();
        }

        private void Update()
        {
            _machine.Update();
        }

        private void FixedUpdate()
        {
            _machine.FixedUpdate();

            SnapToGround();
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

        private void SnapToGround()
        {
            var ray = new Ray(transform.position, Vector3.down);

            var padding = 1.5f;
            
            Debug.DrawRay(ray.origin, ray.direction * padding, Color.blue);
            Debug.DrawRay(ray.origin + ray.direction * padding, ray.direction * padding / 2, Color.red);
            
            if (!Physics.Raycast(ray, out var hit, padding + padding / 2)) return;

            var distance = hit.distance;

            var spring = (distance - padding) * strength;
            var damp = Vector3.Dot(rigidbody.velocity, Vector3.down) * dampening;

            rigidbody.velocity += Vector3.up * ((spring - damp) * Time.deltaTime);
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