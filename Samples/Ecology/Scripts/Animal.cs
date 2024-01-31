using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class Animal : Lifeform, IBehaviourTree
    {
        #region Enums
        public enum Diet { Herbivore, Carnivore, Omnivore }
        public enum Reproduction { Eggs, Live }
        #endregion

        #region Fields
        public Blackboard blackboard { get; set; }
        public RootNode rootNode { get; set; }
        #endregion

        private void Start()
        {
            // Blackboard
            BlackboardKey<Vector3> homePosition = new BlackboardKey<Vector3>("homePosition", Vector3.zero);

            // BehaviourTree
            rootNode = new RootNode();
        }

        private void Update()
        {
            rootNode.Update();
        }

        private void OnDrawGizmosSelected()
        {
            
        }
    }
}
