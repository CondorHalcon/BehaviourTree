using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class GoToLocation : NodeAction
    {
        protected Transform transform;
        protected BlackboardKey<Vector3> location;
        protected BlackboardKey<float> speed;

        public GoToLocation(Transform transform, BlackboardKey<Vector3> location, BlackboardKey<float> speed)
        {
            this.transform = transform;
            this.location = location;
            this.speed = speed;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            if (transform == null || location == null) { return NodeState.Failure; }


            transform.LookAt(location.value);
            transform.position += transform.TransformVector(Vector3.forward * speed.value * Time.deltaTime);

            if (Vector3.Distance(location.value, transform.position) < 1) { return NodeState.Success; }
            else { return NodeState.Running; }
        }
    }
}
