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
        protected Node rootNode;
        public Blackboard Blackboard { get { return blackboard; } }
        public Node RootNode { get { return rootNode; } set { rootNode = value; } }

        public void BehaviourTree()
        {
            // Blackboard
            blackboard = new Blackboard(new List<BlackboardKey>
            {
                new BlackboardKey<Vector3>("HomePosition", transform.position),
                new BlackboardKey<Vector3>("TargetLocation", Vector3.zero),
                new BlackboardKey<Species>("Diet", species),
                new BlackboardKey<SenseStats>("SenseStats", senseStats),
                new BlackboardKey<PhysicalStats>("PhysicalStats", physicalStats),
                new BlackboardKey<List<Lifeform>>("Sensed")
            });

            // BehaviourTree
            this.rootNode = new SequenceNode(new List<Node>
            {
                new Senses(this, blackboard.Find<SenseStats>("SenseStats")),
                new GoToLocation(this, blackboard.Find<Vector3>("TargetLocation"), blackboard.Find<PhysicalStats>("PhysicalStats"))
            });
        }

        private void Start()
        {
            BehaviourTree();
            Debug.Log(blackboard.ToString());
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
