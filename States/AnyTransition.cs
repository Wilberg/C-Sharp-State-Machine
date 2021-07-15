using System;

namespace States
{
    public sealed class AnyTransition
    {
        public IState State;
        public Func<bool> Condition;
    }
}