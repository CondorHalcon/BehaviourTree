using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public sealed class Blackboard
    {
        public static Blackboard globalBlackboard = new Blackboard();
        internal List<BlackboardKey> keys;

        public Blackboard()
        {
            keys = new List<BlackboardKey>();
        }
        public Blackboard(List<BlackboardKey> keys)
        {
            this.keys = keys;
        }

        public BlackboardKey this[int i] => keys[i];
        /// <summary>
        /// Adds a key to the blackboard
        /// WARNING: Unsafe, use `SetValue<T>(string key, T value)` when possible
        /// </summary>
        /// <param name="key"></param>
        public void Add(BlackboardKey key) => keys.Add(key);
        public void Remove(BlackboardKey key) => keys.Remove(key);
        public void Remove(string keyName)
        {
            BlackboardKey key = Find(keyName);
            if (key != null)
            {
                Remove(key);
            }
        }
        public void Remove<T>(string keyName)
        {
            BlackboardKey key = Find<T>(keyName);
            if (key != null)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// Finds the first key in the blackboard which matches keyName
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public BlackboardKey Find(string keyName) {
            return keys.Find((key) => {
                return key.name == keyName;
            });
        }

        /// <summary>
        /// Finds a key that matches keyName with the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public BlackboardKey<T> Find<T>(string keyName) {
            var foundKey = Find(keyName);

            if (foundKey == null) {
                Debug.LogError($"Failed to find blackboard key, invalid keyname:{keyName}");
                return null;
            }

            if (foundKey.type is T) {
                Debug.LogError($"Failed to find blackboard key, invalid keytype:{typeof(T)}, Expected:{foundKey.type}");
                return null;
            }

            var foundKeyTyped = foundKey as BlackboardKey<T>;
            if (foundKeyTyped == null) {
                Debug.LogError($"Failed to find blackboard key, casting failed:{typeof(T)}, Expected:{foundKey.type}");
                return null;
            }
            return foundKeyTyped;
        }
        /// <summary>
        /// Finds a key that matches keyName with the type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <param name="foundKey"></param>
        public BlackboardKey<T> Find<T>(string keyName, out BlackboardKey<T> foundKey)
        {
            foundKey = Find<T>(keyName);
            return foundKey;
        }

        /// <summary>
        /// Tries to set a key to a value using the type specified.
        /// NOTE: This may fail if the key with the matching name has a different type to the one specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public void SetValue<T>(string keyName, T value) {
            BlackboardKey<T> key = Find<T>(keyName);
            if (key != null) {
                key.value = value;
            } else
            {
                keys.Add(new BlackboardKey<T>(keyName, value));
            }
        }

        /// <summary>
        /// Tries to get a key value using the type specified, if the key doesn't exist, it will return the default value of the specified type.
        /// NOTE: This may fail if the key with the matching name has a different type to the one specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public T GetValue<T>(string keyName) {
            BlackboardKey<T> key = Find<T>(keyName);
            if (key != null) {
                return key.value;
            }
            return default(T);
        }
        /// <summary>
        /// Tries to get a key value using the type specified, if the key doesn't exist, it will return the default value of the specified type.
        /// NOTE: This may fail if the key with the matching name has a different type to the one specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public T GetValue<T>(string keyName, out T value)
        {
            value = GetValue<T>(keyName);
            return value;
        }

        public void Log()
        {
            Debug.Log($"Blackboard : {ToString(true)}");
        }
        public override string ToString()
        {
            if (keys == null) { return "{ null }"; }
            string s =  "{ " + keys[0].ToString();
            for (int i = 1; i < keys.Count; i++)
            {
                s += $", {keys[i].ToString()}";
            }
            return s + " }";
        }
        public string ToString(bool pretty)
        {
            if (!pretty) { return ToString(); }
            if (keys == null) { return "{ null }"; }
            string s = "{\n    " + keys[0].ToString();
            for (int i = 1; i < keys.Count; i++)
            {
                s += $",\n    {keys[i].ToString()}";
            }
            return s + "\n}";
        }
    }
}
