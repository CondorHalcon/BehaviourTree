namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public abstract class Node
    {
        public enum NodeState { Running, Success, Failure }
        protected NodeState state { get; private set; }
        protected bool hasStarted = false;

        /// <summary>
        /// Call to run the node.
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

            if (state != NodeState.Running) { Terminate(); }

            return state;
        }

        /// <summary>
        /// Call to cleanly stop running the node
        /// </summary>
        public void Terminate()
        {
            OnStop();
            hasStarted = false;
        }

        /// <summary>
        /// Called when the node runs for the first time after it stopped
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Called when the node stops (Fail, Success, and Terminate)
        /// </summary>
        protected abstract void OnStop();

        /// <summary>
        /// Called while the node is running. [DO NOT call this directly, use `Update()` instead!!!]
        /// </summary>
        /// <returns></returns>
        protected abstract NodeState OnUpdate();
    }
}
