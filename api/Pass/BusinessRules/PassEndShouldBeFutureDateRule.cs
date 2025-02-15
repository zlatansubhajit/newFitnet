using newFitnet.Common.BusinessRulesEngine;

namespace newFitnet.Pass.BusinessRules
{
    internal sealed class PassEndShouldBeFutureDateRule : IBusinessRule
    {
        public string Error => "End Date of Pass needs to be greater than today!";
        private DateTimeOffset _endDate;
        internal PassEndShouldBeFutureDateRule(DateTimeOffset endDate)
        {
            _endDate = endDate;
        }

        public bool IsMet() => _endDate.Date > DateTimeOffset.UtcNow.Date;
    }
}
