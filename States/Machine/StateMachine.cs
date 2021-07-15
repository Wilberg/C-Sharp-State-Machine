using System;
using System.Collections.Generic;
using System.Linq;

namespace States.Machine
{
    public class StateMachine
    {
        public Dictionary<Type, Transition> Transitions;
        protected Transition Active;
        
        public static StateMachineBuilder<StateMachine> Builder()
        {
            return new StateMachineBuilder<StateMachine>();
        }

        public virtual void Update()
        {
            var active = Transitions
                .Select(pair => pair.Value)
                .FirstOrDefault(transition => transition.Condition.Invoke());

            if (active is null) return;

            if (Active != active) Active?.State.OnExit();
            else return;
            
            Active = active;
            Active.State.OnEnter();
        }
    }
}