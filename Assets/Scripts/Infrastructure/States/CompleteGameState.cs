using Scripts.Services;

namespace Scripts.Infrastructure.States
{
    public class CompleteGameState : IState
    {
        private readonly AllServices _services;

        public CompleteGameState(AllServices services)
        {
            _services = services;
        }

        public void Enter()
        {
            var systems = _services.Single<EcsSystemsProvider>().Systems;
            var world = systems.GetWorld();

            if (world.GetEntitiesCount() == 0)
                return;
            
            _services.Single<EntitiesSaveService>().SendSaveRequest();
            
            systems.Destroy();
            world.Destroy();
            
            _services.Single<ProgressService>().SaveProgress();
        }

        public void Exit()
        {
        }
    }
}