using System;
namespace CondorHalcon.BehaviourTree
{
    public interface IBehaviourTree
    {
        public Blackboard Blackboard { get; }
        public RootNode RootNode { get; set; }
    }
}
