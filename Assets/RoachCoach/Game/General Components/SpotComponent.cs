using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;


namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class SpotComponent : IComponent
    {

    }
    [Context(typeof(GameContext))]
    public sealed class RelatedSpotComponent : IComponent
    {
        public Game.Entity RelatedSpot;
    }
}