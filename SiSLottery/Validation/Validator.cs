using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using Validation.Interfaces;

namespace Validation
{
    public class Validator : IValidator
    {
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; private set; }

        public void Validate(ILotteryResult lotteryResult, ILotteryDraw lotteryDraw)
        {
            ErrorMessage = string.Empty;

            if (lotteryResult?.PrimaryNumbers == null || lotteryResult?.SecondaryNumbers == null) {
                ErrorMessage = $"No Primary or Secondary numbers provided";
                return;
            }

            if (lotteryResult.PrimaryNumbers.Count() != lotteryDraw?.PrimaryNumberCount) {
                ErrorMessage = $"Incorrect number of Primary numbers provided";
                return;
            }

            if (lotteryResult.SecondaryNumbers.Count() != lotteryDraw?.SecondaryNumberCount) {
                ErrorMessage = $"Incorrect number of Secondary numbers provided";
                return;
            }

            var invalidNmbers = new List<int>();
            foreach (var number in lotteryResult.PrimaryNumbers) {
                if (number < lotteryDraw.PrimaryNumberLower || number > lotteryDraw.PrimaryNumberUpper) {
                    invalidNmbers.Add(number);
                }
            }
            if (invalidNmbers.Any()) {
                ErrorMessage = $"Primary numbers ({string.Join(",", invalidNmbers)}) out of range {lotteryDraw.PrimaryNumberLower} - {lotteryDraw.PrimaryNumberUpper}";
                return;
            }

            foreach (var number in lotteryResult.SecondaryNumbers) {
                if (number < lotteryDraw.SecondaryNumberLower || number > lotteryDraw.SecondaryNumberUpper) {
                    invalidNmbers.Add(number);
                }
            }
            if (invalidNmbers.Any()) {
                ErrorMessage = $"Secondary numbers ({string.Join(",", invalidNmbers)}) out of range {lotteryDraw.SecondaryNumberLower} - {lotteryDraw.SecondaryNumberUpper}";
                return;
            }
        }
    }
}
