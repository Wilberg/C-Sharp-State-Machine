using System;
using System.Collections.Generic;
using System.Linq;

namespace States.Machine
{
    public class StateMachine
    {
        public Dictionary<Type, List<Transition>> Transitions;
        public List<Transition> AnyTransitions;
        public IState CurrentState;
        
        public static StateMachineBuilder Builder()
        {
            return new StateMachineBuilder();
        }

        public virtual void Update()
        {
            var transition = GetActiveTransition();
            if (transition is null || transition.State == CurrentState) return;
            
            SetState(transition.State);
        }

        public void SetState(IState state)
        {
            if (state is null || state == CurrentState) return;
            
            CurrentState?.OnExit();
            
            CurrentState = state;
            CurrentState.OnEnter();
        }

        private Transition GetActiveTransition()
        {
            foreach (var transition in AnyTransitions)
            {
                if (transition.Condition.Invoke()) return transition;
            }

            var key = CurrentState.GetType();
            if (!Transitions.ContainsKey(key)) return default;
            
            foreach (var transition in Transitions[key])
            {
                if (transition.Condition.Invoke()) return transition;
            }

            return default;
        }
    }
}