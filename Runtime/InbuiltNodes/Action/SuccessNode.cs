namespace CondorHalcon.BehaviourTree
{
    public class SuccessNode : NodeAction
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            return NodeState.Success;
        }
    }
}