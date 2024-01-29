namespace CondorHalcon.BehaviourTree
{
    public class InverterNode : NodeComposite
    {
        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
        {
            // fail if there are no children
            if (children.Count == 0 || children.Count > 1) { return NodeState.Failure; }
            
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
