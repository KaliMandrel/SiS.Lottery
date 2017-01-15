using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotteryRepository;
using NSubstitute;
using Logger;
using Logger.Interfaces;
using Validation.Interfaces;
using Models;

namespace UnitTests
{
    [TestFixture]
    public class LotterRepositoryTests
    {
        private LotteryRepository.LotteryRepository _target;
        private ILogger _logger;
        private IValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger>();
            _validator = Substitute.For<IValidator>();

            _target = new LotteryRepository.LotteryRepository(_validator, _logger);
        }

        [Test]
        public void InitialRepositoryIsNotNull()
        {
            Assert.IsNotNull(_target.Draws);
        }

        [Test]
        public void RepositoryDoesAdd()
        {
            var lotteryDraw = new LotteryDraw();
            lotteryDraw.Name = "Test";

            Assert.That(_target.Draws, Is.Empty);
            Assert.That(_target.Add(lotteryDraw), Is.EqualTo(true));
            Assert.That(_target.Draws.Count(), Is.EqualTo(1));
        }

        [Test]
        public void RepositoryDoesNotAddDuplicateName()
        {
            var lotteryDraw = new LotteryDraw();
            lotteryDraw.Name = "Test";

            Assert.That(_target.Add(lotteryDraw), Is.EqualTo(true));
            Assert.That(_target.Draws.Count(), Is.EqualTo(1));
            Assert.That(_target.Add(lotteryDraw), Is.EqualTo(false));

            _logger.Received(1).Log(Arg.Any<string>());

        }

        [Test]
        public void RepositoryDoesUpdateValidResults()
        {
            var lotteryDraw = new LotteryDraw();
            lotteryDraw.Name = "Test";

            Assert.That(_target.Add(lotteryDraw), Is.EqualTo(true));
            Assert.That(_target.Draws.Count(), Is.EqualTo(1));

            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };
            var lotteryResults = new LotteryResult()
            {
                DrawName = "Test",
                PrimaryNumbers = primaryNumbers,
                SecondaryNumbers = secondaryNumbers
            };

            Assert.That(_target.Update(lotteryResults), Is.EqualTo(true));
        }

        [Test]
        public void RepositoryDoesNotUpdateInvalidResults()
        {
            var lotteryDraw = new LotteryDraw();
            lotteryDraw.Name = "Test";

            Assert.That(_target.Add(lotteryDraw), Is.EqualTo(true));
            Assert.That(_target.Draws.Count(), Is.EqualTo(1));

            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };
            var lotteryResults = new LotteryResult()
            {
                DrawName = "Test",
                PrimaryNumbers = primaryNumbers,
                SecondaryNumbers = secondaryNumbers
            };

            _validator.HasError.Returns(true);
            Assert.That(_target.Update(lotteryResults), Is.EqualTo(false));
            _validator.Received(1).Validate(lotteryResults, Arg.Any<LotteryDraw>());
        }
    }
}
