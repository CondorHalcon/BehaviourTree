using System;
using System.Collections.Generic;

namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public abstract class NodeComposite : Node
    {
        protected List<Node> children;
        protected uint minChildren = 1;
        protected uint maxChildren = 4294967295;

        public Node this[int index] { get { return children[index]; } set { children[index] = value; } }

        public NodeComposite(List<Node> children)
        {
            this.children = (children == null) ? new List<Node>() : children;
            this.minChildren = 1;
            this.maxChildren = int.MaxValue;
        }
        internal override bool IsValid() => children.Count >= minChildren && children.Count <= maxChildren;
        public void Add(Node node) 
        {
            children.Add(node);
            if (children.Count + 1 > maxChildren) { throw new TooManyChildrenException(this); }
        }
        public void Remove(Node node)
        {
            children.Remove(node);
            if (children.Count - 1 < minChildren) { throw new TooFewChildrenException(this); }
        }

        public override void DrawGizmos()
        {
            foreach (Node child in children)
            {
                child.DrawGizmos();
            }
        }

        #region Exceptions
        [Serializable]
        internal sealed class TooManyChildrenException : Exception
        {
            internal TooManyChildrenException(NodeComposite node) : 
                base($"Too many children added to this node. \n Expected: ({node.maxChildren}); Actual: ({node.children.Count})") { }
        }
        [Serializable]
        internal sealed class TooFewChildrenException : Exception
        {
            internal TooFewChildrenException(NodeComposite node) : 
                base($"Too few children added to this node. \n Expected: ({node.minChildren}); Actual: ({node.children.Count})") { }
        }
        #endregion
    }
}
