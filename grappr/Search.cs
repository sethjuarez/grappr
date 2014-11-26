using System;
using System.Linq;
using System.Collections.Generic;

namespace grappr
{
    public abstract class Search
    {
        public event EventHandler<StateExpansionEventArgs> SuccessorExpanded;
        protected virtual void OnSuccessorExpanded(object sender, StateExpansionEventArgs e)
        {
            EventHandler<StateExpansionEventArgs> handler = SuccessorExpanded;
            if (handler != null)
                handler(sender, e);
        }
    }

    public abstract class AdversarialSearch : Search
    {
        public abstract ISuccessor Find(IAdversarialState state);
    }

}