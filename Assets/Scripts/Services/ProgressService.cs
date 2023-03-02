using Scripts.ProgressData;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Services
{
    public class ProgressService : IService
    {
        private readonly SaveLoadService _saveLoadService;
        private readonly StaticDataService _staticDataService;
        private PlayerProgress _progress;

        public ProgressService(SaveLoadService saveLoadService, StaticDataService staticDataService)
        {
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void LoadProgress()
        {
            _progress = _saveLoadService.LoadProgress();

            if (_progress == null)
                _progress = NewProgress();
        }

        public void SaveProgress() => 
            _saveLoadService.SaveProgress(_progress);

        public double WalletAmount() =>
            _progress.WalletAmount;

        public BusinessProgress ForBusinessID(BusinessID businessID)
        {
            var businessProgress = _progress.Businesses.FirstOrDefault(x => x.BusinessID == businessID);

            if (businessProgress != null)
                return businessProgress;

            businessProgress = NewBusinessProgress(businessID);
            _progress.Businesses.Add(businessProgress);
            return businessProgress;
        }

        public void UpdateWalletAmount(double amount) => 
            _progress.WalletAmount = amount;

        private BusinessProgress NewBusinessProgress(BusinessID businessID)
        {
            return new BusinessProgress
            {
                BusinessID = businessID,
                Level = 0,
                TimerProgress = 0,
                PurchasedImprovements = new List<int>()
            };
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress()
            {
                WalletAmount = _staticDataService.GameStartData().StartBalance,
                Businesses = new List<BusinessProgress>()
            };

            var firstBusiness = NewBusinessProgress(BusinessID.Business1);
            firstBusiness.Level = 1;
            progress.Businesses.Add(firstBusiness);

            return progress;
        }
    }
}