namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    [System.Serializable]
    public struct SenseStats
    {
        public float sightDistance;
        public float sightAngle;
        public float smellDistance;

        public SenseStats(float sightDistance = 15, float sightAngle = 60, float smellDistance = 10)
        {
            this.sightDistance = sightDistance;
            this.sightAngle = sightAngle;
            this.smellDistance = smellDistance;
        }
    }
}
