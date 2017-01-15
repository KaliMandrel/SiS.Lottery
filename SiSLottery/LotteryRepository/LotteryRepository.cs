using LotteryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Interfaces;
using Validation.Interfaces;
using Logger.Interfaces;

namespace LotteryRepository
{
    public class LotteryRepository : ILotteryRepository
    {
        IList<ILotteryDraw> _draws;
        private ILogger _logger;
        private IValidator _validator;

        public LotteryRepository(IValidator validator, ILogger logger)
        {
            _draws = new List<ILotteryDraw>();
            _validator = validator;
            _logger = logger;
        }

        public IEnumerable<ILotteryDraw> Draws => _draws;

        public bool Add(ILotteryDraw draw)
        {
            if (Draws.Any(d => d?.Name == draw.Name)) {
                _logger.Log($"{nameof(draw.Name)} must be unique");
                return false;
            }

            _draws.Add(draw);
            return true;
        }

        public bool Update(ILotteryResult lotteryResult)
        {
            var lotteryDraw = Draws.FirstOrDefault(d => d.Name == lotteryResult.DrawName);

            if (lotteryResult == null) {
                _logger.Log($"Lottery draw - {lotteryDraw.Name} already exists");
                return false;
            }

            _validator.Validate(lotteryResult, lotteryDraw);

            if (_validator.HasError) {
                _logger.Log(_validator.ErrorMessage);
                return false;
            }

            lotteryDraw.WinningPrimaryNumbers = lotteryResult.PrimaryNumbers;
            lotteryDraw.WinningSecondaryNumbers = lotteryResult.SecondaryNumbers;

            return true;
        }

        public IEnumerable<ILotteryDraw> RetrieveByDate(DateTime date)
        {
            return Draws.Where(d => d.DrawDate.Date == date.Date).ToList();
        }

        public IEnumerable<ILotteryDraw> Retrieve()
        {
            return _draws;
        }
    }
}
