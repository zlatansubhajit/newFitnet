using Microsoft.EntityFrameworkCore;
using newFitnet.Common.BusinessRulesEngine;
using newFitnet.Pass.Data.Database;

namespace newFitnet.Pass.BusinessRules
{
    internal sealed class OneActivePassPerMemberRule : IBusinessRule
    {
        private Guid _memberId;
        private PassPersistence _passPersistence;
        public string Error => "Active Pass for member already Exists!";

        internal OneActivePassPerMemberRule(Guid memberId,PassPersistence passPersistence)
        {
            _memberId = memberId;
            _passPersistence = passPersistence;
        }

        public bool IsMet()
        {
            var existingpass = _passPersistence.Passes.Any(pass => pass.MemberId == _memberId 
                                                                    && pass.Start <= DateTimeOffset.UtcNow
                                                                    && pass.End >= DateTimeOffset.UtcNow);
            return !existingpass;
        }
    }
}
