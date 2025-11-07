
namespace BlockInfrastructure.Startup
{
    public interface IStartupModule
    {
        void Initialize<T>(T initialize) where T : IInitializeModule;
        void ConfigureCompositionRoot<T>(T composition) where T : IConfigureCompositionRoot;
    }
}
