namespace CondorHalcon.BehaviourTree
{
    public class FailureNode : NodeAction
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            return NodeState.Failure;
        }
    }
}