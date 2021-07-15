using States.Machine;

namespace Movement
{
    public class MovementStateMachine : StateMachine
    {
        public new static StateMachineBuilder<MovementStateMachine> Builder()
        {
            return new StateMachineBuilder<MovementStateMachine>();
        }

        public override void Update()
        {
            base.Update();

            var state = Active?.State as MovementState;
            state?.OnUpdate();
        }

        public void FixedUpdate()
        {
            var state = Active?.State as MovementState;
            state?.OnFixedUpdate();
        }

        public void Action(MovementAction action)
        {
            var state = Active?.State as MovementState;
            state?.OnAction(action);
        }
    }
}