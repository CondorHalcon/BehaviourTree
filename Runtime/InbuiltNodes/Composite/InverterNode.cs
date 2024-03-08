using System.Collections.Generic;

namespace CondorHalcon.BehaviourTree
{
    public class InverterNode : NodeComposite
    {
        public InverterNode(List<Node> children) : base(children)
        {
            this.maxChildren = 1;
        }

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            switch (children[0].Update())
            {
                case NodeState.Success:
                    return NodeState.Failure;
                case NodeState.Failure:
                    return NodeState.Success;
                case NodeState.Running: 
                    return NodeState.Running;
                default: 
                    return NodeState.Failure;
            }
        }
    }
}
