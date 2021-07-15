using States;

namespace Movement
{
    public abstract class MovementState : IState
    {
        protected MovementBehaviour Movement;
        
        public MovementState(MovementBehaviour movement)
        {
            Movement = movement;
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnAction(MovementAction action)
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }
    }
}