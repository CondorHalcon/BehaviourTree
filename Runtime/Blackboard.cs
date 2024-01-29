using System;
using System.Collections.Generic;

namespace CondorHalcon.BehaviourTree
{
    [System.Serializable]
    public sealed class Blackboard
    {
        public List<BlackboardKey> keys = new List<BlackboardKey>();

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
                Console.WriteLine($"Failed to find blackboard key, invalid keyname:{keyName}");
                return null;
            }

            if (foundKey.type != typeof(T)) {
                Console.WriteLine($"Failed to find blackboard key, invalid keytype:{typeof(T)}, Expected:{foundKey.type}");
                return null;
            }

            var foundKeyTyped = foundKey as BlackboardKey<T>;
            if (foundKeyTyped == null) {
                Console.WriteLine($"Failed to find blackboard key, casting failed:{typeof(T)}, Expected:{foundKey.type}");
                return null;
            }
            return foundKeyTyped;
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
            }
        }

        /// <summary>
        /// Tries to get a key value using the type specified.
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
    }
}
