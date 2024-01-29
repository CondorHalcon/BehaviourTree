namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public abstract class BlackboardKey
    {
        public string name;
        public System.Type type;
        
        public BlackboardKey(System.Type t)
        {
            this.type = t;
        }

        public abstract bool Equals(BlackboardKey key);
    }

    [System.Serializable]
    public class BlackboardKey<T> : BlackboardKey
    {
        public T value;

        public BlackboardKey(string name) : base(typeof(T))
        {
            this.name = name;
        }
        public BlackboardKey(string name, T value) : base(typeof(T))
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{name} : {value}";
        }

        public override bool Equals(BlackboardKey key)
        {
            if (key.type == type)
            {
                BlackboardKey<T> other = key as BlackboardKey<T>;
                return this.value.Equals(other.value);
            }
            return false;
        }
    }
}
