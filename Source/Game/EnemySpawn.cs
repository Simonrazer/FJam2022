using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// EnemySpawn Script.
    /// </summary>
    public enum enemyKind{
        Basic, Door
    }
    
    public class EnemySpawn : Script
    {
        public float amount = 1;
        public float intervall = 1;
        public enemyKind kind;

        public List<Prefab> enemies;
        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            
        }
        
        /// <inheritdoc/>
        System.Timers.Timer myTimer;
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
            myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(spawnEmeny);
            myTimer.Interval = 1000; // 1000 ms is one second
            myTimer.Start();
        }
        int enemiesSpawned = 0;
        public List<Actor> enms = new List<Actor>();
        public bool isDone = false;
        private void spawnEmeny(object source, System.Timers.ElapsedEventArgs e){
            if(enemiesSpawned >= amount) {
                isDone = true;
                return;
            }
            Actor ee = null;
            switch (kind){
                case enemyKind.Basic:
                    ee = PrefabManager.SpawnPrefab(enemies[0], Actor.Position);
                    break;
                case enemyKind.Door:
                    ee = PrefabManager.SpawnPrefab(enemies[1], Actor.Position);
                    break;
            }
            ee.Position = Actor.Position;
            enemiesSpawned++;
            enms.Add(ee);
            Debug.Log("AA");
            ee.GetChild("CharacterController").GetScript<EnemyBasics>().spawnedBy = Actor;
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
            myTimer.Dispose();
            //foreach(Actor e in enms){
            //    Destroy(e);
           // }
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }
    }
}
