using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class Senses : NodeAction
    {
        protected Transform transform;
        protected BlackboardKey<float> sightDistance;
        protected BlackboardKey<float> sightAngle;
        protected BlackboardKey<float> smellDistance;
        protected BlackboardKey<List<Lifeform>> sensed;
        Lifeform[] lifeforms;
        int index = 0;

        public Senses(Transform transform, BlackboardKey<float> sightDistance, BlackboardKey<float> sightAngle, BlackboardKey<float> smellDistance)
        {
            this.transform = transform;
            this.sightDistance = sightDistance;
            this.sightAngle = sightAngle;
            this.smellDistance = smellDistance;
        }
        private bool Chance(float percent)
        {
            float num = Random.Range(0f, 1f);
            return num < percent;
        }
        protected override void OnStart()
        {
            lifeforms = Object.FindObjectsOfType<Lifeform>();
            index = 0;
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            // would prefer a while loop, but Unity will crash
            for (int i = index; i < lifeforms.Length; i++)
            {
                if (lifeforms[i].transform == transform) { continue; }
                if (Vector3.Distance(lifeforms[i].transform.position, transform.position) <= smellDistance.value)
                {
                    if (Chance(.7f * Time.deltaTime)) // 30% per second chance they are not detected
                    {
                        sensed.value.Add(lifeforms[i]);
                        continue; // has already been sensed, no need to check sight
                    }
                }
                if (Vector3.Distance(lifeforms[i].transform.position, transform.position) <= sightDistance.value)
                {
                    Vector3 vector = lifeforms[i].transform.position - transform.position;
                    float dot = Vector3.Dot(transform.forward, vector.normalized);
                    if (dot <= sightAngle.value)
                    {
                        sensed.value.Add(lifeforms[i]);
                    }
                }
                index++;
                return NodeState.Running;
            }
            return NodeState.Success;
        }
    }
}