namespace CondorHalcon.BehaviourTree
{
    public class SelectorNode : NodeComposite
    {
        public BlackboardKey<bool> condition;

        public SelectorNode(BlackboardKey<bool> condition)
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
            // fail if there are no children
            if (children.Count == 0) { return NodeState.Failure; }
            // fail if there are more than 2 children
            else if (children.Count > 2) { return NodeState.Failure; }
            // update child if there is only one
            else if (children.Count == 1 && condition.value) { return children[0].Update(); }
            // select which child to update
            else
            {
                if (condition.value) { return children[0].Update(); }
                else { return children[1].Update(); }
            }
        }
    }
}
