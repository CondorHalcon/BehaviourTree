using System.Collections.Generic;

namespace CondorHalcon.BehaviourTree
{
    public class SelectorNode : NodeComposite
    {
        private int currentIndex = 0;

        public SelectorNode(List<Node> children) : base(children) { }

        protected override void OnStart()
        {
            currentIndex = 0;
        }

        protected override void OnStop()
        {
            
        }

        protected override NodeState OnUpdate()
        {
            // fail if there are no children
            if (children.Count < 1) { return NodeState.Failure; }

            switch (children[currentIndex].Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    return NodeState.Success;
                default:
                    break;
            }
            // current child failed, iterate to next child
            currentIndex++;
            if (currentIndex >= children.Count) { return NodeState.Failure; }
            return NodeState.Running;
        }
    }
}
