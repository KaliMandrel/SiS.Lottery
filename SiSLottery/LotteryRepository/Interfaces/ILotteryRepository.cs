using Models.Interfaces;
using System;
using System.Collections.Generic;

namespace LotteryRepository.Interfaces
{
    public interface ILotteryRepository
    {
        IEnumerable<ILotteryDraw> Draws { get; }
        bool Add(ILotteryDraw daw);
        bool Update(ILotteryResult result);
        IEnumerable<ILotteryDraw> RetrieveByDate(DateTime date);
        IEnumerable<ILotteryDraw> Retrieve();
    }
}
