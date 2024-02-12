using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class GoToLocation : NodeAction
    {
        protected Animal self;
        protected BlackboardKey<Vector3> location;
        protected BlackboardKey<PhysicalStats> physicalStats;

        public GoToLocation(Animal self, BlackboardKey<Vector3> location, BlackboardKey<PhysicalStats> physicalStats)
        {
            this.self = self;
            this.location = location;
            this.physicalStats = physicalStats;
        }

        protected override void OnStart()
        {
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            if (self.transform == null || location == null) { return NodeState.Failure; }


            self.transform.LookAt(location.value);
            self.transform.position += self.transform.TransformVector(Vector3.forward * physicalStats.value.speed * Time.deltaTime);

            if (Vector3.Distance(location.value, self.transform.position) < 1) { return NodeState.Success; }
            else { return NodeState.Running; }
        }
    }
}
