
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    [System.Serializable]
    public struct SenseStats
    {
        [Range(1, 20)] public float sightDistance;
        [Range(35, 180)] public float sightAngle;
        [Range(1, 20)] public float smellDistance;

        public SenseStats(float sightDistance = 10, float sightAngle = 60, float smellDistance = 5)
        {
            this.sightDistance = sightDistance;
            this.sightAngle = sightAngle;
            this.smellDistance = smellDistance;
        }
    }
}
