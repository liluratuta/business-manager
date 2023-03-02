namespace Scripts.Services
{
    public interface IUpdater : IService
    {
        void Register(IUpdateable updateable);
        void Unregister(IUpdateable updateable);
    }
}