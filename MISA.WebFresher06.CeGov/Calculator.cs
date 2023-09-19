namespace MISA.WebFresher06.CeGov
{
    public class Calculator
    {
        /// <summary>
        /// Hàm cộng hai số nguyên
        /// </summary>
        /// <param name="x">Số hạng 1</param>
        /// <param name="y">Số hạng 2</param>
        /// <returns>Tổng hai số nguyên</returns>
        /// CreatedBy: ddVang(14/8/2023)
        public static long Add(int x, int y)
        {
            return x + (long)y;
        }
        /// <summary>
        /// Hàm Trừ hai số nguyên
        /// </summary>
        /// <param name="x">Số hạng 1</param>
        /// <param name="y">Số hạng 2</param>
        /// <returns>Hiệu hai số nguyên</returns>
        /// CreatedBy: ddVang(14/8/2023)
        public static long Sub(int x, int y)
        {
            return x - (long)y;

        }
        /// <summary>
        /// Nhân hai số nguyên
        /// </summary>
        /// <param name="x">Số hạng 1</param>
        /// <param name="y">Số hạng 2</param>
        /// <returns>Phép nhân hai số nguyên</returns>
        /// CreatedBy: ddVang(14/8/2023)
        public static long Mul(int x, int y)
        {
            return x * (long) y;
        }
        /// <summary>
        /// Hàm chia hai số nguyên
        /// </summary>
        /// <param name="x">Số hạng 1</param>
        /// <param name="y">Số hạng 2</param>
        /// <returns>phép hai số nguyên</returns>
        /// CreatedBy: ddVang(14/8/2023)
        public static float Div(int x, int y)
        {
            if(y == 0)
            {
                throw new Exception("Không chia được cho 0");
            }

            return x / (float)y;
        }

        /// <summary>
        ///  Hàm tính tổng giá giá trị của một chuỗi số đươc ngăn cách bởi dấy phẩy
        /// </summary>
        /// <param name="input"></param>
        /// <returns>tổng giá giá trị của một chuỗi số</returns>
        /// <exception cref="ArgumentException"></exception>
        ///  /// CreatedBy: ddVang(14/8/2023)
        public static int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            string[] numbers = input.Split(',');
            int sum = 0;
            string negativeNumbers = "";

            foreach (string number in numbers)
            {
                int value = int.Parse(number.Trim());

                if (value < 0)
                {
                    if (!string.IsNullOrEmpty(negativeNumbers))
                        negativeNumbers += ", ";
                    negativeNumbers += number;
                }

                sum += value;
            }

            if (!string.IsNullOrEmpty(negativeNumbers))
                throw new ArgumentException($"Không chấp nhận toán tử âm: {negativeNumbers}");

            return sum;
        }
    }
}
