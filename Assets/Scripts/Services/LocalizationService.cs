namespace Scripts.Services
{
    public class LocalizationService : IService
    {
        public string Localize(string key)
        {
            return $"[{key}]";
        }
    }
}