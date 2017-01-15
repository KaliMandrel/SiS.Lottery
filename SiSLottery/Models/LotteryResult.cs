using Models.Interfaces;
using System.Collections.Generic;

namespace Models
{
    public class LotteryResult : ILotteryResult
    {
        public string DrawName { get; set; }
        public IEnumerable<int> PrimaryNumbers { get; set; }
        public IEnumerable<int> SecondaryNumbers { get; set; }
    }
}
