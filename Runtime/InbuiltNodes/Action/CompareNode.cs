namespace CondorHalcon.BehaviourTree
{
    public class CompareNode : NodeAction
    {
        public enum Comparison { Equal, NotEqual, Greater, Less, GreaterOrEqual, LessOrEqual }

        public Comparison comparison;
        public BlackboardKey<bool> output;
        public BlackboardKey<object> input1;
        public BlackboardKey<object> input2;

        public CompareNode(BlackboardKey<bool> output, BlackboardKey<object> input1, BlackboardKey<object> input2, Comparison comparison = Comparison.Equal)
        {
            this.output = output;
            this.input1 = input1;
            this.input2 = input2;
            this.comparison = comparison;
        }

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            if (output == null || input1 == null || input2 == null) { return NodeState.Failure; }
            
            output.value = input1.value == input2.value;
            return NodeState.Success;
        }
    }
}