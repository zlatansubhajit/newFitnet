using newFitnet.Common.BusinessRulesEngine;
using newFitnet.Member.Data.Database;
using newFitnet.Pass.BusinessRules;
using newFitnet.Pass.Data.Database;
using System.Text.Json.Serialization;

namespace newFitnet.Pass.Data
{
    internal sealed class Pass
    {
        public Guid Id { get; init; }
        public Guid MemberId { get; init; }
        public DateTimeOffset Start { get; init; }
        public DateTimeOffset End { get; private set; }
        public string Location { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PassType Type { get; private set; } // add type update functionality later


        private Pass(Guid id, Guid memberId, DateTimeOffset start, DateTimeOffset end
            , string location, PassType type)
        {
            Id = id; MemberId = memberId; Start = start; End = end; Location = location; Type = type;
        }

        internal static Pass Register(Guid memberId, DateTimeOffset start
            , DateTimeOffset end,string location,
            PassType type,PassPersistence passPersistence, MembersPersistence membersPersistence)
        {
            BusinessRuleValidator.Validate(new PassEndShouldBeFutureDateRule(end));
            BusinessRuleValidator.Validate(new PassOnlyForExistingMembersRule(memberId, membersPersistence));
            BusinessRuleValidator.Validate(new OneActivePassPerMemberRule(memberId,passPersistence));
            return new Pass(
                Guid.NewGuid(),
                memberId,
                start,
                end,
                location, 
                type
                );
        }

        internal void Extend(DateTimeOffset newEnd)
        {
            End = newEnd;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PassType 
    {
        GymOnly,
        GymWithPT,
        Yoga
    }
}
