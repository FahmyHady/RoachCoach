using Entitas;
using RoachCoach.Game;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using static RoachCoach.RoachCoachGameMachineMatcher;
using static RoachCoach.RoachCoachGameVisualReferenceMatcher;
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;
namespace RoachCoach
{
    //This intializes Machine spots, then any related spots to further created Machines
    public class MachineIntializationSystem : ReactiveSystem<Game.Entity>, IInitializeSystem
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public MachineIntializationSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        public void Initialize()
        {
            var machineData = configContext.GetShopConfig().Value.GetMachineCreationData();
            for (int i = 0; i < machineData.Length; i++)
            {
                var item = machineData[i];
                CreateMachineStandSpot(item.type, item.posOfMachineStand, item.rotationOfMachineStand, i + 1);

                var machineSpot = CreateMachineSpot(item.type, item.posOfMachine, item.rotationOfMachine, i + 1);
                if (i == 0) machineSpot.AddCreate();  //Create the starting machine
            }
        }



        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var Machine in entities)
            {
                CreateMachineRelatedSpots(Machine);
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasMachine() && entity.HasVisualReference();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AllOf(Machine, VisualReference));
        }

        Game.Entity CreateMachineStandSpot(CommodityType type, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand, int id)
        {
            var machineStandSpot = gameContext.CreateEntity()
          .AddFree()
          .AddMachineStand()
          .AddSpot()
          .AddId(id)
          .AddTransform(posOfMachineStand, rotationOfMachineStand);

            switch (type)
            {
                case CommodityType.Taco:
                    machineStandSpot.AddTaco(1);
                    break;
                case CommodityType.Soda:
                    machineStandSpot.AddSoda(1);
                    break;
                default:
                    break;
            }
            return machineStandSpot;
        }
        Game.Entity CreateMachineSpot(CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, int id)
        {
            var machineSpot = gameContext.CreateEntity()
       .AddFree()
       .AddMachine()
       .AddSpot()
       .AddId(id)
       .AddTransform(posOfMachine, rotationOfMachine);//Should add visual representation of creator and wait for player click
                                                      // .AddVisualRepresentation(VisualType.Machine);
            switch (type)
            {
                case CommodityType.Taco:
                    machineSpot.AddTaco(1);
                    break;
                case CommodityType.Soda:
                    machineSpot.AddSoda(1);
                    break;
                default:
                    break;
            }
            return machineSpot;
        }

        void CreateMachineRelatedSpots(Game.Entity machineEntity)
        {
            var machinePreprationSpotLocation = ((IMachineVisual)machineEntity.GetVisualReference().visualInterface).GetPreparationSpotLocation();
            int id = machineEntity.GetId().Value;
            var relatedPreprationSlot = gameContext.CreateSpot(machinePreprationSpotLocation, Quaternion.identity/* item.chefSpot.rotation*/).AddChef().AddMachine().AddId(id);
            machineEntity.AddRelatedSpot(relatedPreprationSlot);
        }
    }
}
