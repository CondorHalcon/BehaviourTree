using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree
{
    public class ParallelNode : NodeComposite
    {
        private bool stopOnFail;
        private bool stopOnSuccess;
        private List<Node> childrenLeftToRun;

        public ParallelNode(List<Node> children) : base(children)
        {
            this.stopOnFail = true;
            this.stopOnSuccess = false;
            this.childrenLeftToRun = new List<Node>();
        }
        public ParallelNode(bool stopOnFail, List<Node> children) : this(children)
        {
            this.stopOnFail = stopOnFail;
        }
        public ParallelNode(bool stopOnFail, bool stopOnSuccess, List<Node> children) : this(stopOnFail, children)
        {
            this.stopOnSuccess = stopOnSuccess;
        }

        protected override void OnStart()
        {
            foreach (Node child in children)
            {
                childrenLeftToRun.Add(child);
            }
        }

        protected override void OnStop()
        {
            if (state != NodeState.Success)
            {
                foreach (Node child in childrenLeftToRun) { child.Terminate(); }
            }
            childrenLeftToRun.Clear();
        }

        protected override NodeState OnUpdate()
        {
            for (int i = childrenLeftToRun.Count - 1; i >= 0; i--)
            {
                switch (childrenLeftToRun[i].Update())
                {
                    case NodeState.Running:
                        continue;
                    case NodeState.Success:
                        childrenLeftToRun.RemoveAt(i);
                        if (stopOnSuccess) { return NodeState.Success; }
                        continue;
                    default:
                        childrenLeftToRun.RemoveAt(i);
                        if (stopOnFail) { return NodeState.Failure; }
                        continue;
                }
            }
            if (childrenLeftToRun.Count == 0) { return NodeState.Success; }
            else { return NodeState.Running; }
        }

        public override void DrawGizmos()
        {
            foreach (Node child in childrenLeftToRun)
            {
                child.DrawGizmos();
            }
        }
    }
}
