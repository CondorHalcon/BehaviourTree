namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    [System.Serializable]
    public struct PhysicalStats
    {
        public float speed;

        public PhysicalStats(float speed = 3)
        {
            this.speed = speed;
        }
    }
}
