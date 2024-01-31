using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class EcologyManager : MonoBehaviour
    {
        public Transform plantlifeT;

        [Header("Plants")]
        [Range(10, 50)] public int plantCount = 25;
        [Range(.1f, 1f)] public float treeChance = .1f;
        public GameObject bushPrefab;
        public GameObject treePrefab;

        [ContextMenu("Generate Plants")]
        public void GeneratePlates()
        {
            if (plantlifeT == null) { plantlifeT = new GameObject("Plants").transform; }

            for (int i = 0; i < plantCount; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-25, 25), 0, Random.Range(-25, 25));
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
