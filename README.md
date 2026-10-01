# RoachCoach

**An Entitas-based food-service simulation exploring how a gameplay loop can be expressed as cooperating ECS systems.**

Customers arrive, place orders, and wait while chefs collect requests, prepare food or drinks at machines, and deliver the finished items. Orders connect customers, workers, production resources, and payment through explicit entity relationships.

Built in 2024 with **Unity 2022.3.0f1**, C#, and Entitas 2.0. This is an architecture prototype and source-reading case study.

## Engineering focus

- **Gameplay state as data.** Components describe waiting, taking an order, travelling to a machine, preparation, and delivery. Reactive systems respond to relevant state changes; movement and delays are processed by execution systems.
- **Explicit orchestration.** `GameSystems` composes initialization, creation, the service loop, movement, generated events, and cleanup. The execution order is visible in one place.
- **A connected simulation model.** Customers, chefs, orders, commodities, machines, and service spots are separate entities linked by relationship components.
- **Configuration boundaries.** Game, Config, and Input contexts separate concerns. Shop and prefab configuration bridge scene authoring into the simulation; Config, Contexts, and Game have separate assembly definitions.

## Follow the service loop

```text
Customer waits → chef takes request → order entity is created
    → available chef selects work → machine prepares commodity
    → chef delivers → order is fulfilled → wallet and cleanup update
```

| Start here | What to look for |
| --- | --- |
| [GameController](Assets/RoachCoach/Game/GameController.cs) | Context setup, scene configuration, and the systems lifecycle |
| [GameSystems](Assets/RoachCoach/Game/GameSystems.cs) | Composition and ordering of the simulation |
| [CoreLoopSystems](Assets/RoachCoach/Game/Core%20Loop%20Systems/CoreLoopSystems.cs) | The complete sequence of service-loop systems |
| [Taco order assignment](Assets/RoachCoach/Game/Core%20Loop%20Systems/Systems/FreeChefsPickupFreeTacoOrdersAndGoToMachineSystem.cs) | Matching available workers, orders, and production resources |
| [MoveToTargetSystem](Assets/RoachCoach/Game/Movement/MoveToTargetSystem.cs) | Movement expressed through entity components |
| [ProcessFulfilledOrdersSystem](Assets/RoachCoach/Game/Core%20Loop%20Systems/Systems/ProcessFulfilledOrdersSystem.cs) | Payment and order lifecycle completion |

## Open the project

1. Clone this repository and add its root directory to Unity Hub.
2. Use **Unity 2022.3.0f1**, as recorded in [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt).
3. Allow Unity to restore the dependencies in [Packages/manifest.json](Packages/manifest.json). The repository also includes Entitas packages and its generator assembly.
4. Open [Food Cart.unity](Assets/RoachCoach/Food%20Cart.unity) and inspect the scene's shop and prefab configuration before entering Play mode.

The current README was checked against the source and project configuration. A fresh Unity import and runtime session have not been verified as part of this documentation update.

## Tradeoffs and next steps

Small systems make transitions inspectable, but spread the service loop across many files. Relationship traversal is sometimes deeply chained, and some resource lookups assume a match exists. A further iteration would centralize relationship queries, make resource-unavailable behavior explicit, and add scenario tests for competing orders and worker allocation.

This repository demonstrates gameplay modelling and system composition; it does not include performance measurements or claim production readiness.

## Project lineage and assets

The repository originated from an Entitas Match One example setup. The portfolio focus is the food-service simulation under [Assets/RoachCoach](Assets/RoachCoach), rather than the original Match One example. Entitas, generated infrastructure, and bundled third-party visual assets are separate from that gameplay work.

See the root [LICENSE](LICENSE) and the notices accompanying bundled dependencies and assets. This README does not change their licensing.
