namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public abstract class Node
    {
        public enum NodeState { Running, Success, Failure }
        protected NodeState state { get; private set; }
        protected bool hasStarted = false;

        /// <summary>
        /// Called to run the node.
        /// </summary>
        /// <returns></returns>
        public NodeState Update()
        {
            if (!hasStarted)
            {
                hasStarted = true;
                OnStart();
            }

            state = OnUpdate();

            if (state != NodeState.Running)
            {
                OnStop();
                hasStarted = false;
            }

            return state;
        }

        /// <summary>
        /// Called when the node runs for the first time after it stopped
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Called when the node stops (Fail or Success)
        /// </summary>
        protected abstract void OnStop();

        /// <summary>
        /// Called while the node is running. [DO NOT call this directly, use `Update()` instead!!!]
        /// </summary>
        /// <returns></returns>
        protected abstract NodeState OnUpdate();
    }
}
