using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Animal : Lifeform, IBehaviourTree
    {
        #region Enums
        public enum Diet { Herbivore, Carnivore, Omnivore }
        public enum Reproduction { Eggs, Birth }
        #endregion

        #region Fields
        [Header("Senses")]
        [Range(1, 20)] public float sightDistance = 5;
        [Range(35, 180)] public float sightAngle = 60;
        [Range(1, 20)] public float smellDistance = 10;

        [Header("Physical")]
        [Range(1, 8)] public float speed = 3;

        [Header("Behaviour")]
        public Diet diet = Diet.Herbivore;

        protected Blackboard blackboard;
        protected RootNode rootNode;
        public Blackboard Blackboard { get { return blackboard; } }
        public RootNode RootNode { get { return rootNode; } set { rootNode = value; } }
        #endregion

        private void Start()
        {
            // Blackboard
            BlackboardKey<float> k_sightDistance = new BlackboardKey<float>("SightDistance", sightDistance);
            BlackboardKey<float> k_sightAngle = new BlackboardKey<float>("SightAngle", sightAngle);
            BlackboardKey<float> k_smellDistance = new BlackboardKey<float>("SmellDistance", smellDistance);
            blackboard.Add(k_sightDistance);
            blackboard.Add(k_sightAngle);
            blackboard.Add(k_smellDistance);
            BlackboardKey<float> k_speed = new BlackboardKey<float>("Speed", speed);
            blackboard.Add(k_speed);
            BlackboardKey<Diet> k_diet = new BlackboardKey<Diet>("Diet", diet);
            blackboard.Add(k_diet);
            BlackboardKey<Vector3> k_homePosition = new BlackboardKey<Vector3>("HomePosition", transform.position);
            BlackboardKey<Vector3> k_targetLocation = new BlackboardKey<Vector3>("TargetLocation", Vector3.zero);
            blackboard.Add(k_homePosition);

            // BehaviourTree
            this.rootNode = new RootNode();
            SequenceNode mainSequence = new SequenceNode();
            rootNode.child = mainSequence;
            GoToLocation goL = new GoToLocation(transform, k_targetLocation, k_speed);
            mainSequence.Add(goL);
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