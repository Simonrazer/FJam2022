using System;
using System.Collections.Generic;
using FlaxEngine;

namespace ExamplePlugin
{
    /// <summary>
    /// CrowdSystem Script.
    /// </summary>
    /// <summary>
/// Navigation agents crowd system using <see cref="FlaxEngine.NavCrowd"/>.
/// </summary>
public class MyPlugin : GamePlugin
{
    private NavCrowd _crowd;
    private CrowdTaskGraphSystem _system;

    /// <summary>
    /// The maximum amount of crowd agents (at once).
    /// </summary>
    public int MaxAgents = 25;

    internal void AddAgent(Agent007 agent)
    {
        if (_crowd == null)
        {
            // Lazy init
            _crowd = new NavCrowd();
            if (_crowd.Init(100, MaxAgents, agent.Scene.FindActor("NavMesh.Magic").As<NavMesh>()))
                throw new Exception("Failed to initialize crowd");
            if (_system == null)
                Engine.UpdateGraph.AddSystem(_system = new CrowdTaskGraphSystem { System = this });
        }

        // Add agent to the crowd
        agent.ID = _crowd.AddAgent(agent.Actor.Position, agent.Properties);
        if (agent.ID == -1)
            throw new Exception("Failed to add agent to the crowd");
        agent.Crowd = _crowd;
    }

    internal void RemoveAgent(Agent007 agent)
    {
        // Remove agent from the crowd
        _crowd.RemoveAgent(agent.ID);
        agent.Crowd = null;
        agent.ID = -1;
    }

    /// <inheritdoc />
    public override void Deinitialize()
    {
        // Cleanup
        Engine.UpdateGraph.RemoveSystem(_system);
        FlaxEngine.Object.Destroy(ref _system);
        FlaxEngine.Object.Destroy(ref _crowd);

        base.Deinitialize();
    }

    /// <summary>
    /// Custom Task Graph System that updates crowd durign async job.
    /// </summary>
    private sealed class CrowdTaskGraphSystem : TaskGraphSystem
    {
        internal MyPlugin System;

        /// <inheritdoc />
        public override void Execute(TaskGraph graph)
        {
            // Schedule async job to update crowd
            graph.DispatchJob(UpdateJob);
        }

        private void UpdateJob(int i)
        {
            // Update crowd simulation
            System._crowd.Update(Time.DeltaTime);
        }
    }
}
}
