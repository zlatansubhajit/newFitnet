using newFitnet.Common.BusinessRulesEngine;

namespace newFitnet.Member.BusinessRules
{
    internal sealed class NoDuplicateMemberRule : IBusinessRule
    {
        private readonly bool _exists;
        public string Error => "Member with same Phone number or Email already exists!";

        internal NoDuplicateMemberRule(bool exists) => _exists = exists;
        public bool IsMet() => !_exists;
    }
}
