using System;
using System.Collections.Generic;

namespace Models.Interfaces
{
    public interface ILotteryDraw
    {
        string Name { get; set; }
        string Description { get; set; }
        DateTime DrawDate { get; set; }
        int PrimaryNumberCount { get; set; }
        int PrimaryNumberLower { get; set; }
        int PrimaryNumberUpper { get; set; }
        int SecondaryNumberCount { get; set; }
        int SecondaryNumberLower { get; set; }
        int SecondaryNumberUpper { get; set; }

        IEnumerable<int> WinningPrimaryNumbers { get; set; }
        IEnumerable<int> WinningSecondaryNumbers { get; set; }
    }
}
