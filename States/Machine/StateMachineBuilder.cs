using System;
using System.Collections.Generic;

namespace States.Machine
{
    public sealed class StateMachineBuilder<T> where T : StateMachine, new()
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private readonly List<Transition> _anyTransition = new List<Transition>();
        private IState _initialState;

        public StateMachineBuilder<T> RegisterState(IState state)
        {
            var type = state.GetType();
            
            _states[type] = state;
            _transitions[type] = new List<Transition>();
            
            return this;
        }

        public StateMachineBuilder<T> RegisterTransition<TFrom, TTo>(Func<bool> condition)
        {
            var state = _states[typeof(TTo)];

            if (!_transitions.ContainsKey(typeof(TFrom)))
            {
                // Todo: Throw warning.
                return this;
            }


            _transitions[typeof(TFrom)].Add(new Transition
            {
                State = state,
                Condition = condition
            });

            return this;
        }

        public StateMachineBuilder<T> RegisterAnyTransition<TState>(Func<bool> condition)
        {
            var state = _states[typeof(TState)];
            
            _anyTransition.Add(new Transition
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