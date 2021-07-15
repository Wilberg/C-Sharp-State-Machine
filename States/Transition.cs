using System;

namespace States
{
    public sealed class Transition
    {
        public IState From;
        public IState To;
        public Func<bool> Condition;
    }
}