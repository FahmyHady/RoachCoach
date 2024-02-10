using Entitas;

namespace RoachCoach
{
    partial class GameContext : IContext
    {
        public static GameContext Instance
        {
            get => _instance ??= new GameContext();
            set => _instance = value;
        }

        static GameContext _instance;
    }
}
