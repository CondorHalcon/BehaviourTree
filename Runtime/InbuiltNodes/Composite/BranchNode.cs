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
            if (state == NodeState.Running)
            {
                foreach (Node child in children) { child.Terminate(); }
            }
        }

        protected override NodeState OnUpdate()
        {
            if (condition.value) 
            { 
                if (children.Count > 1) { children[1].Terminate(); }
                return children[0].Update(); 
            }
            else if (children.Count > 1) 
            {
                children[0].Terminate();
                return children[1].Update(); 
            }
            return NodeState.Failure;
        }
        public override void DrawGizmos()
        {
            if (condition.value && children.Count > 0 && children[0] != null) { children[0].DrawGizmos(); }
            else if (children.Count > 1 && children[1] != null) { children[1].DrawGizmos(); }
        }
    }
}
