using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class EcologyManager : MonoBehaviour
    {
        [Range(25, 100)] public float worldSize = 50;

        [Header("Plants")]
        public Transform plantlifeT;
        public GameObject bushPrefab;
        public GameObject treePrefab;
        [Range(10, 50)] public int plantCount = 25;
        [Range(.1f, 1f)] public float treeChance = .1f;

        public void Start ()
        {
            GeneratePlants();
        }

        public void GeneratePlants()
        {
            if (plantlifeT == null) { plantlifeT = new GameObject("Plants").transform; }

            for (int i = 0; i < plantCount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-worldSize, worldSize), 0, Random.Range(-worldSize, worldSize));
                if (Chance(treeChance)) { Instantiate(treePrefab, pos, Quaternion.identity, plantlifeT); }
                else { Instantiate(bushPrefab, pos, Quaternion.identity, plantlifeT); }
            }
        }

        private bool Chance(float percent)
        {
            float num = Random.Range(0f, 1f);
            return num < percent;
        }
    }
}
