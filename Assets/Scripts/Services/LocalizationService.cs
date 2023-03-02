namespace Scripts.Services
{
    public class LocalizationService : IService
    {
        private readonly StaticDataService _staticDataService;

        public LocalizationService(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public string Localize(string key) => 
            _staticDataService.LocaleFor(key) ?? $"[{key}]";
    }
}