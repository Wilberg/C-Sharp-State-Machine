using States.Machine;

namespace Movement
{
    public class MovementStateMachine : StateMachine
    {
        public new static StateMachineBuilder<MovementStateMachine> Builder()
        {
            return new StateMachineBuilder<MovementStateMachine>();
        }

        public void Action(MovementAction action)
        {
            var state = CurrentState as MovementState;
            state?.OnAction(action);
        }
    }
}