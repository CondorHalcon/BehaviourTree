namespace CondorHalcon.BehaviourTree
{
    public class SequenceNode : NodeComposite
    {
        private int currentIndex = 0;
        public bool stopOnFail = true;

        public SequenceNode()
        {
            this.currentIndex = 0;
            this.stopOnFail = true;
        }
        public SequenceNode(bool stopOnFail) : this()
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
            }
            // current child done, iterate to next child
            currentIndex++;
            if (currentIndex >= children.Count) { return NodeState.Success; }
            return NodeState.Running;
        }
    }
}
