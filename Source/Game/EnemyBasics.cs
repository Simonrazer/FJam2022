using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// EnemyBasics Script.
    /// </summary>
    
    public class EnemyBasics : Script
    {
        /// <inheritdoc/>
        
        public float health = 100;
        public Actor spawnedBy;
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
            if (health <= 0){
                spawnedBy.GetScript<EnemySpawn>().enms.Remove(Actor);
                Destroy(Actor);
            }
        }
    }
}
