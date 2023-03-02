using UnityEngine;

namespace Scripts.Views
{
    public class BusinessView : MonoBehaviour
    {
        private BusinessID _businessID;

        public void SetID(BusinessID businessID)
        {
            _businessID = businessID;
        }
    }
}