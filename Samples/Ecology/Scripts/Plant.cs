using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class Plant : Lifeform
    {
        [Space(10f)]
        [Range(1, 100), SerializeField] protected float regrowTime = 10;
        [SerializeField] protected GameObject[] fruits;

        public bool Harvest()
        {
            for (int i = 0; i < fruits.Length; i++)
            {
                if (fruits[i].activeInHierarchy == true)
                {
                    StartCoroutine(Regrow(fruits[i]));
                    return true;
                }
            }
            return false;
        }
        IEnumerator Regrow(GameObject fruit)
        {
            if (fruit == null) { yield break; }
            
            fruit.SetActive(false);
            yield return new WaitForSeconds(regrowTime);
            fruit.SetActive(true);
        }
    }
}
