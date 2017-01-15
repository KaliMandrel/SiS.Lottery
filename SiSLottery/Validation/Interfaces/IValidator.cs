using Models.Interfaces;

namespace Validation.Interfaces
{
    public interface IValidator
    {
        bool HasError { get; }
        string ErrorMessage { get; }
        void Validate(ILotteryResult lotteryResult, ILotteryDraw lotteryDraw);
    }
}
