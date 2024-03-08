using System.Collections.Generic;

namespace CondorHalcon.BehaviourTree
{
    public class SequenceNode : NodeComposite
    {
        private int currentIndex = 0;
        private bool stopOnFail = true;

        public SequenceNode(List<Node> children) : base(children)
        {
            this.currentIndex = 0;
            this.stopOnFail = true;
        }
        public SequenceNode(bool stopOnFail, List<Node> children) : this(children)
        {
            this.stopOnFail = stopOnFail;
        }

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
                case NodeState.Failure:
                    if (stopOnFail) { return NodeState.Failure; }
                    break;
                default:
                    break;
            }
            // current child done, iterate to next child
            currentIndex++;
            if (currentIndex >= children.Count) { return NodeState.Success; }
            return NodeState.Running;
        }
    }
}
