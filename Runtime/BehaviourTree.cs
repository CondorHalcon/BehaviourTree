using System;
namespace CondorHalcon.BehaviourTree
{
    public interface BehaviourTree
    {
        public Blackboard blackboard { get; set; }
        public RootNode rootNode { get; set; }
    }
}
