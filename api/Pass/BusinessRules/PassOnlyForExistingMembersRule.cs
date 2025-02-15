using newFitnet.Common.BusinessRulesEngine;
using newFitnet.Member.Data;
using newFitnet.Member.Data.Database;
using System.Threading;

namespace newFitnet.Pass.BusinessRules
{
    internal sealed class PassOnlyForExistingMembersRule : IBusinessRule
    {
        private Guid _memberId;
        private MembersPersistence _membersPersistence;
        public string Error => $"Member with Id {_memberId} not found.";

        internal PassOnlyForExistingMembersRule(Guid memberId, MembersPersistence membersPersistence)
        {
            _memberId = memberId;
            _membersPersistence = membersPersistence;
        }

        public bool IsMet()
        {
            var memberexists = _membersPersistence.Members.Any(member => member.Id == _memberId);
            return memberexists;
        }
    }
}
