using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// MagicZone Script.
    /// </summary>

    
    public class MagicZone : Script
    {
        /// <inheritdoc/>
        public List<Actor> spawners;
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
            bool won = true;

            foreach (Actor s in spawners){
                var r = s.GetScript<EnemySpawn>();
                //Debug.Log(r.enms.Count);
                if((r.enms.Count != 0 || !r.isDone)){
                    won = false;
                }
            }
            if(won) hasWon();
        }

        public void hasWon(){
            Debug.Log("Won a thig");
            //foreach (Actor s in spawners){
            //    spawners.Remove(s);
           //     Destroy(s);
           // }
        }
    }
}
