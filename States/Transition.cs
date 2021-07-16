using System;

namespace States
{
    public sealed class Transition
    {
        public IState State;
        public Func<bool> Condition;
    }
}