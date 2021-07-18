using States.Machine;

namespace Movement
{
    public class MovementStateMachine : StateMachine
    {
        public void Action(MovementAction action)
        {
            (CurrentState as MovementState)?.OnAction(action);
        }

        public void FixedUpdate()
        {
            (CurrentState as MovementState)?.OnFixedUpdate();
        }
    }
}