using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree
{
    public class DebugNode : NodeAction
    {
        public enum DebugType { Log, LogWarning, LogError }

        private string message;
        private BlackboardKey<string> messageKey;
        private Object context; 
        private DebugType type;

        #region Constructors
        internal DebugNode(DebugType type = DebugType.Log)
        {
            this.type = type;
            this.message = "DebugNode";
            this.messageKey = null;
            this.context = null;
        }
        public DebugNode(string message, DebugType type = DebugType.Log) : this(type)
        {
            this.message = message;
        }
        public DebugNode(string message, Object context, DebugType type = DebugType.Log) : this(message, type)
        {
            this.context = context;
        }
        public DebugNode(BlackboardKey<string> message, DebugType type = DebugType.Log) : this(type)
        {
            this.messageKey = message;
        }
        public DebugNode(BlackboardKey<string> message, Object context, DebugType type = DebugType.Log) : this(message, type)
        {
            this.context = context;
        }
        #endregion

        protected override void OnStart()
        {
            string msg = (messageKey != null) ? messageKey.value : message;
            switch (type)
            {
                case DebugType.LogWarning:
                    Debug.LogWarning(msg, context); break;
                case DebugType.LogError:
                    Debug.LogError(msg, context); break;
                default:
                    Debug.Log(msg, context); break;
            }
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate() { return NodeState.Success; }
    }
}
