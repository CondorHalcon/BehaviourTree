using System.Collections;
using System.Collections.Generic;

namespace CondorHalcon.BehaviourTree
{
    public class BranchNode : NodeComposite
    {
        public BlackboardKey<bool> condition;

        public BranchNode(BlackboardKey<bool> condition, List<Node> children) : base(children)
        {
            this.condition = condition;
            this.maxChildren = 2;
        }
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override NodeState OnUpdate()
        {
            if (condition.value) { return children[0].Update(); }
            else if (children.Count > 1) { return children[1].Update(); }
            return NodeState.Failure;
        }
    }
}
