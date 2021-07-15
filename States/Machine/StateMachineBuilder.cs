using System;
using System.Collections.Generic;

namespace States.Machine
{
    public sealed class StateMachineBuilder<T> where T : StateMachine, new()
    {
        private readonly Dictionary<Type, Transition> _transitions = new Dictionary<Type, Transition>();
        
        public StateMachineBuilder<T> RegisterTransition<TState>(Func<bool> condition) where TState : IState, new()
        {
            _transitions[typeof(TState)] = new Transition
            {
                Condition = condition,
                State = new TState()
            };

            return this;
        }

        public StateMachineBuilder<T> RegisterTransition(IState state, Func<bool> condition)
        {
            _transitions[state.GetType()] = new Transition
            {
                Condition = condition,
                State = state
            };

            return this;
        }
        
        public T Build()
        {
            return new T
            {
                Transitions = _transitions
            };
        }
    }
}