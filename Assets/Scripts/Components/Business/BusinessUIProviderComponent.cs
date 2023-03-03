using System.Collections.Generic;
using Scripts.Views;

namespace Scripts.Components.Business
{
    public struct BusinessUIProviderComponent
    {
        public TimerView TimerView;
        public BusinessNameView NameView;
        public LevelView LevelView;
        public LevelUpButtonView LevelUpButtonView;
        public IncomeView IncomeView;
        public List<ImprovementButtonView> ImprovementButtons;
    }
}