namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public sealed class RootNode : Node
    {
        public Node child;

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            if (child != null)
            {
                return child.Update();
            }
            return NodeState.Failure;
        }
    }
}