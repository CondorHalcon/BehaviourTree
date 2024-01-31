using System;
namespace CondorHalcon.BehaviourTree
{
    public interface IBehaviourTree
    {
        public Blackboard blackboard { get; set; }
        public RootNode rootNode { get; set; }
    }
}
