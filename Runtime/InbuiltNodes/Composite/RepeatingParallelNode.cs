using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree
{
	public class RepeatingParallelNode : NodeComposite
	{
		private bool stopOnFail;

		/// <summary>
		/// WARNING: this is an infinitly running node
		/// </summary>
		/// <param name="children"></param>
		public RepeatingParallelNode(List<Node> children) : base(children)
		{
			this.stopOnFail = true;
		}
		/// <summary>
		/// WARNING: this is an infinitly running node
		/// </summary>
		/// <param name="stopOnFail"></param>
		/// <param name="children"></param>
		public RepeatingParallelNode(bool stopOnFail, List<Node> children) : this(children)
		{
			this.stopOnFail = stopOnFail;
		}

		protected override void OnStart() { }

		protected override void OnStop()
		{
            if (state != NodeState.Success)
            {
                foreach (Node child in children) { child.Terminate(); }
            }
        }

		protected override NodeState OnUpdate()
		{
			foreach (Node child in children)
			{
				switch (child.Update())
				{
					case NodeState.Running:
						continue;
					case NodeState.Failure:
						if (stopOnFail) { return NodeState.Failure; }
						continue;
					default:
						continue;
				}
			}
			return NodeState.Running;
		}
	}
}
