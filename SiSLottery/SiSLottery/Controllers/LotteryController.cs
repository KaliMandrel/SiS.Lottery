using LotteryRepository.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiSLottery.Controllers
{
    [RoutePrefix("api/lottery")]
    public class LotteryController : ApiController
    {
        private ILotteryRepository _lotteryRepository;

        public LotteryController(ILotteryRepository lotteryRepository)
        {
            _lotteryRepository = lotteryRepository;
        }

        [HttpPost]
        [Route("CreateDraw")]
        public IHttpActionResult CreateDraw([FromBody] LotteryDraw lotteryDraw)
        {
            if(_lotteryRepository.Add(lotteryDraw))
                return Ok();

            return Content(HttpStatusCode.InternalServerError, $"Draw named {lotteryDraw.Name} already exists");
        }

        [HttpPut]
        [Route("UpdateDraw")]
        public IHttpActionResult UpdateWinningNumbers([FromBody] LotteryResult lotteryResult)
        {
            _lotteryRepository.Update(lotteryResult);

            return Ok();
        }

        [Route("RetrieveDraws/{date}")]
        public IHttpActionResult RetrieveDrawsByDate(DateTime date)
        {
            var draws = _lotteryRepository.RetrieveByDate(date);
            
            return Content(HttpStatusCode.OK, draws);
        }

        [HttpGet]
        [Route("RetrieveDraws")]
        public IHttpActionResult RetrieveDraws()
        {
            var draws = _lotteryRepository.Retrieve();

            return Content(HttpStatusCode.OK, draws);
        }
    }
}
