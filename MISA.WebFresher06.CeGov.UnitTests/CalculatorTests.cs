using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov
{
    public class CalculatorTests
    {
        /// <summary>
        /// Hàm test cộng thành công 2 số nguyên
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(-1, 7, 6)]
        [TestCase(int.MaxValue, int.MaxValue, int.MaxValue * (long)2)]

        public void Add_ValidInput_SumTwoNumber(int x, int y, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(x, y);
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        /// <summary>
        /// Hàm test trừ thành công 2 số nguyên
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [TestCase(1, 2, -1)]
        [TestCase(2, 3, -1)]
        [TestCase(-1, 7, -8)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)2 + 1)]

        public void Sub_ValidInput_SumTwoNumber(int x, int y, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Sub(x, y);
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        /// <summary>
        /// Hàm test nhân thành công 2 số nguyên
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [TestCase(1, 2, 2)]
        [TestCase(2, 3, 6)]
        [TestCase(-1, 7, -7)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)int.MinValue)]

        public void Mul_ValidInput_SumTwoNumber(int x, int y, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Mul(x, y);
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        /// <summary>
        /// Hàm test chia 2 số nguyên, xét trường hợp chia cho 0.
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [Test]
        public void Div_ZeroInput_ThrowException()
        {
            //Arrange
            var x = 1;
            var y = 0;
            var expecedMessage = "Không chia được cho 0";
            //Act & Assert
            var exception = Assert.Throws<Exception>(() => Calculator.Div(x, y));
            Assert.That(exception.Message, Is.EqualTo(expecedMessage));

        }

        /// <summary>
        /// Hàm test chia 2 số nguyên, xét trường hợp còn lại.
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [TestCase(1, 2, 0.5f)]
        [TestCase(1, 3, 1 / (float)3)]
        [TestCase(int.MaxValue, 3, int.MaxValue / (float)3)]
        public void Div_ValidInput_Success(int x, int y, float expectedResult)
        {
            //Act
            var actualResult = Calculator.Div(x, y);
            //Assert
            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(10e-3));
        }

        /// <summary>
        /// Hàm test tính tổng giá trị của một chuỗi số với số không âm
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2,3", 6)]
        [TestCase("1, 2, 3", 6)]
        public void Add_RegularNumbers_SumOfStringNumbers(string input, int expectedResult)
        {
            //Act
            int actualResult = Calculator.Add(input);
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test tính tổng giá trị của một chuỗi số với số âm
        /// </summary>
        /// CreatedBy: ddVang(14/8/2023)
        [TestCase("1,-2,-3", "Không chấp nhận toán tử âm: -2, -3")]
        [TestCase("2,-5,-8", "Không chấp nhận toán tử âm: -5, -8")]
        public void CalculateSumOfStringNumbers_NegativeNumbers_ErrorMessage(string input, string expectedErrorMessage)
        {
            //Act
            var exception = Assert.Throws<ArgumentException>(() => Calculator.Add(input));
            //Assert
            Assert.That(exception.Message, Is.EqualTo(expectedErrorMessage));
        }
    }
}
