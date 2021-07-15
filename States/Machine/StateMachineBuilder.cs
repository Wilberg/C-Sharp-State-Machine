using System;
using System.Collections.Generic;

namespace States.Machine
{
    public sealed class StateMachineBuilder<T> where T : StateMachine, new()
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private readonly List<Transition> _transitions = new List<Transition>();
        private readonly List<AnyTransition> _anyTransition = new List<AnyTransition>();
        private IState _initialState;

        public StateMachineBuilder<T> RegisterState(IState state)
        {
            _states[state.GetType()] = state;
            
            return this;
        }

        public StateMachineBuilder<T> RegisterTransition<TFrom, TTo>(Func<bool> condition)
        {
            var from = _states[typeof(TFrom)];
            var to = _states[typeof(TTo)];
            
            _transitions.Add(new Transition
            {
                From = from,
                To = to,
                Condition = condition
            });

            return this;
        }

        public StateMachineBuilder<T> RegisterAnyTransition<TState>(Func<bool> condition)
        {
            var state = _states[typeof(TState)];
            
            _anyTransition.Add(new AnyTransition
            {
                State = state,
                Condition = condition
            });

            return this;
        }

        public StateMachineBuilder<T> SetInitialState<TState>()
        {
            _initialState = _states[typeof(TState)];

            return this;
        }
        
        public T Build()
        {
            var machine = new T
            {
                Transitions = _transitions,
                AnyTransitions = _anyTransition
            };
            
            machine.SetState(_initialState);

            return machine;
        }
    }
}