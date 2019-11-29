namespace SmartHome.DomainCore.Data
{
    public class PagingArgs
    {
        public PagingArgs(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public int Skip { get; }
        public int Take { get; }
    }
}