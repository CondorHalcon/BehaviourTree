using System;
namespace CondorHalcon.BehaviourTree
{
    public interface IBehaviourTree
    {
        public Blackboard Blackboard { get; }
        public Node RootNode { get; set; }

        public void BehaviourTree();
    }
}
