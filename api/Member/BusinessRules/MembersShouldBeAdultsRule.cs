using newFitnet.Common.BusinessRulesEngine;

namespace newFitnet.Member.BusinessRules
{
    internal sealed class MembersShouldBeAdultsRule : IBusinessRule
    {
        private readonly int _age;
        public string Error => "Only Adults can join our Gym!";

        internal MembersShouldBeAdultsRule(int age) => _age = age;
        public bool IsMet() => _age >= 18;
    }
}
