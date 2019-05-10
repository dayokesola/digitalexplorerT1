namespace BizNest.Core.Domain.Entity
{
    public class SearchItem
    {
        public string Word { get; set; } 
        public float MatchPercentage { get; set; }

        public string IsProhibited { get; set; }
        public string Advice { get; set; }
    }
}