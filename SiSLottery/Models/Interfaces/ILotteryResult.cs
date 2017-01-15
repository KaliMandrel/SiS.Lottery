using System.Collections.Generic;

namespace Models.Interfaces
{
    public interface ILotteryResult
    {
        string DrawName { get; set; }
        IEnumerable<int> PrimaryNumbers { get; set; }
        IEnumerable<int> SecondaryNumbers { get; set; }
    }
}