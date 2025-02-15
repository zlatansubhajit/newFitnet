using newFitnet.Common.BusinessRulesEngine;
using newFitnet.Member.BusinessRules;

namespace newFitnet.Member.Data
{
    internal sealed class Member
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }


        private Member(Guid id, string name, string phone, string? email, string? address) 
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }

        internal static Member CreateOrUpdate(string Name, string Phone, string? Email, string? Address, bool Duplicate = false) 
        {
            BusinessRuleValidator.Validate(new NoDuplicateMemberRule(Duplicate));
            return new Member
            (
                Guid.NewGuid(),
                Name,
                Phone,
                Email,
                Address
            );
        }

    }
}
