using System;
using System.Collections.Generic;
using System.Linq;

namespace States.Machine
{
    public class StateMachine
    {
        public List<Transition> Transitions;
        public List<AnyTransition> AnyTransitions;
        public IState CurrentState;
        
        public static StateMachineBuilder<StateMachine> Builder()
        {
            return new StateMachineBuilder<StateMachine>();
        }

        public virtual void Update()
        {
            var activeAnyTransition = AnyTransitions.FirstOrDefault(transition => transition.Condition.Invoke());
            if (!(activeAnyTransition is null))
            {
                if (activeAnyTransition.State != CurrentState) SetState(activeAnyTransition.State);
                return;
            }

            var activeTransition = Transitions
                .FirstOrDefault(transition => transition.From == CurrentState && transition.Condition.Invoke());

            if (activeTransition is null || activeTransition.To == CurrentState) return;

            SetState(activeTransition.To);
        }

        public void SetState(IState state)
        {
            if (state is null) return;
            
            CurrentState?.OnExit();
            
            CurrentState = state;
            CurrentState.OnEnter();
        }
    }
}