using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Models
{
    public class LotteryDraw : ILotteryDraw
    {
        public string Description { get; set; }

        public DateTime DrawDate { get; set; }

        public string Name { get; set; }

        public int PrimaryNumberCount { get; set; }

        public int PrimaryNumberLower { get; set; }

        public int PrimaryNumberUpper { get; set; }

        public int SecondaryNumberCount { get; set; }

        public int SecondaryNumberLower { get; set; }

        public int SecondaryNumberUpper { get; set; }

        public IEnumerable<int> WinningPrimaryNumbers { get; set; }

        public IEnumerable<int> WinningSecondaryNumbers { get; set; }
    }
}
