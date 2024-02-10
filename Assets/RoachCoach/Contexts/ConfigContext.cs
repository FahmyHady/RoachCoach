using Entitas;

namespace RoachCoach
{
    partial class ConfigContext : IContext
    {
        public static ConfigContext Instance
        {
            get => _instance ??= new ConfigContext();
            set => _instance = value;
        }

        static ConfigContext _instance;
    }

}
