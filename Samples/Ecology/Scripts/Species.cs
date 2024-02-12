namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    #region Enums
    public enum Diet { Herbivore, Carnivore, Omnivore }
    public enum Reproduction { Eggs, Birth }
    #endregion

    [System.Serializable]
    public struct Species
    {
        public Diet diet;
        public Reproduction reproduction;

        public static bool operator ==(Species lhs, Species rhs)
        {
            return (lhs.diet == rhs.diet) && (lhs.reproduction == rhs.reproduction);
        }
        public static bool operator !=(Species lhs, Species rhs) { return !(lhs == rhs); }
    }
}
