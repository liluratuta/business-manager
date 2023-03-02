namespace Scripts.Services
{
    public class LocalizationService
    {
        public string Localize(string key)
        {
            return $"[{key}]";
        }
    }
}