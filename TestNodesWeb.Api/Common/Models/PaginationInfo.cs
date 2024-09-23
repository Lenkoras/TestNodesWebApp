namespace TestNodesWeb.Api.Common.Models
{
    public readonly struct PaginationInfo
    {
        public int SkipCount { get; }
        public int TakeCount { get; }

        public PaginationInfo(int skipCount, int takeCount)
        {
            SkipCount = skipCount;
            TakeCount = takeCount;
        }
    }
}
