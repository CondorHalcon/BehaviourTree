using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Animal : Lifeform, IBehaviourTree
    {
        [Header("Physical")]
        [Range(1, 8)] public float speed = 3;

        [Header("Behaviour")]
        public Species species;
        public SenseStats senseStats = new SenseStats();
        public PhysicalStats physicalStats = new PhysicalStats();

        protected Blackboard blackboard;
        protected RootNode rootNode;
        public Blackboard Blackboard { get { return blackboard; } }
        public RootNode RootNode { get { return rootNode; } set { rootNode = value; } }

        public void BehaviourTree()
        {
            // Blackboard
            blackboard = new Blackboard();
            BlackboardKey<Vector3> k_homePosition = new BlackboardKey<Vector3>("HomePosition", transform.position);
            BlackboardKey<Vector3> k_targetLocation = new BlackboardKey<Vector3>("TargetLocation", Vector3.zero);
            blackboard.Add(k_homePosition);
            blackboard.Add(k_targetLocation);
            BlackboardKey<Species> k_species = new BlackboardKey<Species>("Diet", species);
            BlackboardKey<SenseStats> k_senseStats = new BlackboardKey<SenseStats>("SenseStats", senseStats);
            BlackboardKey<PhysicalStats> k_physcalStats = new BlackboardKey<PhysicalStats>("PhysicalStats", physicalStats);
            blackboard.Add(k_species);
            blackboard.Add(k_senseStats);
            blackboard.Add(k_physcalStats);
            BlackboardKey<List<Lifeform>> k_sensed = new BlackboardKey<List<Lifeform>>("Sensed");
            blackboard.Add(k_sensed);

            // BehaviourTree
            this.rootNode = new RootNode();
            SequenceNode mainSequence = new SequenceNode();
            rootNode.child = mainSequence;
            Senses senses = new Senses(this, k_senseStats);
            GoToLocation goToLocation = new GoToLocation(this, k_targetLocation, k_physcalStats);
            mainSequence.Add(senses);
            mainSequence.Add(goToLocation);
        }

        private void Start()
        {
            BehaviourTree();
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
