namespace BizNest.Core.Domain.Entity
{
    public class SearchItem
    {
        public long Id { get; set; }
        public string Word { get; set; } 
        public float MatchPercentage { get; set; }

        public bool IsProhibited { get; set; }
        public string Advice { get; set; }
    }
}