namespace CondorHalcon.BehaviourTree
{
    public class ArrayNode : NodeComposite
    {
        public bool stopOnFail = true;

        public ArrayNode()
        {
            this.stopOnFail = true;
        }
        public ArrayNode(bool stopOnFail) : this()
        {
            this.stopOnFail = stopOnFail;
        }

        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override NodeState OnUpdate()
        {
            // fail if there are no children
            if (children.Count < 1) { return NodeState.Failure; }

            for (int i = 0; i < children.Count; i++)
            {
                switch (children[i].Update())
                {
                    case NodeState.Running:
                        return NodeState.Running;
                    case NodeState.Failure:
                        if (stopOnFail) { return NodeState.Failure; }
                        break;
                    case NodeState.Success:
                        break; // keep looping
                    default:
                        return NodeState.Failure;
                }
            }
            return NodeState.Success;
        }
    }
}
