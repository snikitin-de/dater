using System;
using System.Linq;

namespace dater
{
    class Generator
    {
        private Random rand = new Random();

        public Guid generateGUID()
        {
            return Guid.NewGuid();
        }

        public bool generateBoolean()
        {
            int randValue = rand.Next(0, 2);

            bool generatedValue;

            if (randValue == 1) {
                generatedValue = true;
            } else
            {
                generatedValue = false;
            }

            return generatedValue;
        }

        public int generateInteger(int minValue = 0, int maxValue = 1000000)
        {
            int randValue = rand.Next(minValue, maxValue + 1);

            return randValue;
        }

        public double generateFloat(int minValue = 0, int maxValue = 1000000, int roundValue = 2)
        {
            double randValue = rand.NextDouble() * (maxValue - minValue) + minValue;

            return Math.Round(randValue, roundValue);
        }

        public string generateString(string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", int length = 10)
        {
            string randValue = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[rand.Next(s.Length)]).ToArray());

            return randValue;
        }

        public DateTime generateDatetime(DateTime? startDate = null, DateTime? endDate = null)
        {
            startDate = startDate ?? DateTime.MinValue.AddYears(1900);
            endDate = endDate ?? DateTime.MinValue.AddYears(2050);

            TimeSpan timeSpan = (TimeSpan)(endDate - startDate);
            TimeSpan newSpan = new TimeSpan(0, rand.Next(0, (int)timeSpan.TotalMinutes), 0);

            DateTime generatedDateTime = DateTime.Now;

            generatedDateTime = (DateTime)startDate + newSpan;

            return generatedDateTime;
        }

        public string generateVariant()
        {
            int randValue = generateInteger(0, 4);

            string generatedValue = "";

            switch (randValue)
            {
                case 0:
                    generatedValue = generateString();
                    break;
                case 1:
                    generatedValue = generateInteger().ToString();
                    break;
                case 2:
                    generatedValue = generateFloat().ToString();
                    break;
                case 3:
                    generatedValue = generateDatetime().ToString();
                    break;
                case 4:
                    generatedValue = generateBoolean().ToString();
                    break;
            }

            return generatedValue;
        }
    }
}
