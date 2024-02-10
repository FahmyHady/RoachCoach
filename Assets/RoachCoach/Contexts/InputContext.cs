using Entitas;

namespace RoachCoach
{
    partial class InputContext : IContext
    {
        public static InputContext Instance
        {
            get => _instance ??= new InputContext();
            set => _instance = value;
        }

        static InputContext _instance;
    }
}
