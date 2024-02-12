using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CondorHalcon.BehaviourTree.Samples.Ecology
{
    public class Senses : NodeAction
    {
        protected Animal self;
        protected BlackboardKey<SenseStats> stats;
        protected BlackboardKey<List<Plant>> plants;
        protected BlackboardKey<Animal> threat;
        protected BlackboardKey<Animal> kin;
        Lifeform[] lifeforms;
        int index = 0;

        public Senses(Animal self, BlackboardKey<SenseStats> stats)
        {
            this.self = self;
            this.stats = stats;
        }
        private bool Chance(float percent)
        {
            float num = Random.Range(0f, 1f);
            return num < percent;
        }
        private void Evaluate(Lifeform lifeform)
        {
            if (lifeform.GetType() == typeof(Animal))
            {
                Animal animal = (Animal)lifeform;
                if (animal.species == self.species && kin.value)
                {
                    if (!kin.value) { kin.value = animal; return; }
                    kin.value = Vector3.Distance(kin.value.transform.position, self.transform.position) <= Vector3.Distance(animal.transform.position, self.transform.position) ? kin.value : animal;
                }
                else if (animal.species.diet != Diet.Herbivore)
                {
                    if (!threat.value) { threat.value = animal; return; }
                    threat.value = Vector3.Distance(threat.value.transform.position, self.transform.position) <= Vector3.Distance(animal.transform.position, self.transform.position) ? threat.value : animal;
                }
            }
            else if (lifeform.GetType() == typeof(Plant))
            {
                plants.value.Add((Plant)lifeform);
            }
        }

        protected override void OnStart()
        {
            lifeforms = Object.FindObjectsOfType<Lifeform>();
            index = 0;
        }
        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            // would prefer a while loop, but Unity will crash
            for (int i = index; i < lifeforms.Length; i++)
            {
                if (lifeforms[i].transform == self.transform) { continue; }
                if (Vector3.Distance(lifeforms[i].transform.position, self.transform.position) <= stats.value.smellDistance)
                {
                    if (Chance(.7f * Time.deltaTime)) // 30% per second chance they are not detected
                    {
                        Evaluate(lifeforms[i]);
                        continue; // has already been sensed, no need to check sight
                    }
                }
                if (Vector3.Distance(lifeforms[i].transform.position, self.transform.position) <= stats.value.sightDistance)
                {
                    Vector3 vector = lifeforms[i].transform.position - self.transform.position;
                    float dot = Vector3.Dot(self.transform.forward, vector.normalized);
                    if (dot <= stats.value.sightAngle)
                    {
                        Evaluate(lifeforms[i]);
                    }
                }
                index++;
                return NodeState.Running;
            }
            return NodeState.Success;
        }
    }
}