using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;

namespace UnitTests
{
    [TestFixture]
    public class ValidatorTests
    {
        private Validator _target;

        [SetUp]
        public void Setup()
        {
            _target = new Validator();
        }

        [Test]
        public void CheckDefaultState()
        {
            Assert.That(string.IsNullOrEmpty(_target.ErrorMessage), Is.True);
            Assert.That(_target.HasError, Is.False);
        }

        [Test]
        public void NoPrimaryNumbersFailsValidation()
        {
            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };

            var lotteryResult = new LotteryResult() { SecondaryNumbers = secondaryNumbers };
            _target.Validate(lotteryResult, null);
            Assert.That(_target.HasError, Is.True);
            Assert.That(_target.ErrorMessage, Is.EqualTo("No Primary or Secondary numbers provided"));
        }

        [Test]
        public void NoSecondaryNumbersFailsValidation()
        {
            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };

            var lotteryResult = new LotteryResult() { PrimaryNumbers = primaryNumbers };
            _target.Validate(lotteryResult, null);
            Assert.That(_target.HasError, Is.True);
            Assert.That(_target.ErrorMessage, Is.EqualTo("No Primary or Secondary numbers provided"));
        }

        [Test]
        public void IncorrectNumberOfPrimaryNumbersFails()
        {
            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };

            var lotteryResult = new LotteryResult() { PrimaryNumbers = primaryNumbers, SecondaryNumbers = secondaryNumbers };

            var lotteryDraw = new LotteryDraw()
            {
                PrimaryNumberCount = 4
            };

            _target.Validate(lotteryResult, lotteryDraw);
            Assert.That(_target.HasError, Is.True);
            Assert.That(_target.ErrorMessage, Is.EqualTo("Incorrect number of Primary numbers provided"));
        }

        [Test]
        public void IncorrectNumberOfSecondaryNumbersFails()
        {
            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };

            var lotteryResult = new LotteryResult() { PrimaryNumbers = primaryNumbers, SecondaryNumbers = secondaryNumbers };

            var lotteryDraw = new LotteryDraw()
            {
                PrimaryNumberCount = 5,
                SecondaryNumberCount = 3
            };

            _target.Validate(lotteryResult, lotteryDraw);
            Assert.That(_target.HasError, Is.True);
            Assert.That(_target.ErrorMessage, Is.EqualTo("Incorrect number of Secondary numbers provided"));
        }

        [Test]
        public void PrimaryNumbersOutOfRangeFailsValidation()
        {
            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };

            var lotteryResult = new LotteryResult() { PrimaryNumbers = primaryNumbers, SecondaryNumbers = secondaryNumbers };

            var lotteryDraw = new LotteryDraw()
            {
                PrimaryNumberCount = 5,
                PrimaryNumberLower = 2,
                PrimaryNumberUpper = 7,
                SecondaryNumberCount = 2,
                SecondaryNumberLower = 2,
                SecondaryNumberUpper = 3
            };

            _target.Validate(lotteryResult, lotteryDraw);
            Assert.That(_target.HasError, Is.True);
            Assert.That(_target.ErrorMessage, Is.EqualTo($"Primary numbers (1) out of range {lotteryDraw.PrimaryNumberLower} - {lotteryDraw.PrimaryNumberUpper}"));
        }

        [Test]
        public void SecondaryNumbersOutOfRangeFailsValidation()
        {
            var primaryNumbers = new List<int> { 1, 2, 3, 4, 5 };
            var secondaryNumbers = new List<int> { 6, 7 };

            var lotteryResult = new LotteryResult() { PrimaryNumbers = primaryNumbers, SecondaryNumbers = secondaryNumbers };

            var lotteryDraw = new LotteryDraw()
            {
                PrimaryNumberCount = 5,
                PrimaryNumberLower = 1,
                PrimaryNumberUpper = 7,
                SecondaryNumberCount = 2,
                SecondaryNumberLower = 2,
                SecondaryNumberUpper = 3
            };

            _target.Validate(lotteryResult, lotteryDraw);
            Assert.That(_target.HasError, Is.True);
            Assert.That(_target.ErrorMessage, Is.EqualTo($"Secondary numbers (6,7) out of range {lotteryDraw.SecondaryNumberLower} - {lotteryDraw.SecondaryNumberUpper}"));
        }
    }
}
