namespace newFitnet.Common.BusinessRulesEngine
{
    internal interface IBusinessRule
    {
        bool IsMet();
        string Error { get; }
    }
}
