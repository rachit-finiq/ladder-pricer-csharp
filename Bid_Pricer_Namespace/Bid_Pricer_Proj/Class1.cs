using System.Text.RegularExpressions;

namespace Bid_Pricer_Proj
{
    public class Bid_Pricer
    {
        private String fix;  // raw fix
        private int maxQuote; // the maximum requested amount till now.
        private List<double> dp; // array to store all the prices. dp[i] denotes the best price for i million size
        private Double[] finalDenoms; // available sizes
        private Double[] finalPrices; // corresponding prices

        // parse the fix and get finalDenoms and finalPrices
        public void parseFix(String f)
        {
            fix = f;
            String[] fixAr = fix.Split( "\u0001" , StringSplitOptions.None);
            Console.WriteLine(fixAr[0]);
            List<Double> denomList = new List<Double>();
            List<Double> priceList = new List<Double>();

            // iterate over fix and get the denoms and prices
            for (int i = 1; i < fixAr.Length; i++)
            {
                string pattern = "[\\p{Cc}\\p{Cf}\\p{Co}\\p{Cn}]";
                fixAr[i] = Regex.Replace(fixAr[i], pattern, "");
                String[] y = fixAr[i].Split("=");
                if (String.Equals(fixAr[i], "135") && Convert.ToDouble(y[1]) % 1000000 == 0)
                {
                    denomList.Add(Convert.ToDouble(y[1]) / 1000000);
                    priceList.Add(Convert.ToDouble(fixAr[i - 2].Split("=")[1]));
                }
            }
            finalDenoms = denomList.ToArray();
            finalPrices = priceList.ToArray();
            // finalPrices = priceList.ToArray(new Double[1]);

            // initialise the prices. They are initialised to the corresponding prices in the fix
            dp = new List<Double>(finalDenoms.Length);
            // dp = new List(finalDenoms.Length);
            dp.Add(finalPrices[0]);
            dp.Add(finalPrices[0]);
            for (int i = 1; i < finalDenoms.Length; i++)
            {
                for (double j = finalDenoms[i - 1] + 1; j <= finalDenoms[i]; j++)
                {
                    dp.Add(finalPrices[i]);
                }
            }

            maxQuote = 2;
        }

        // get the Best Price for the requested size
        public double getBestPrice(double notional)
        {
            if (notional < maxQuote)
            { // if already calculated, return the value only
                int x = (int)Math.Ceiling(notional);
                return dp[x];
            }
            else
            {
                int Size = (int)Math.Ceiling(notional);
                dp.EnsureCapacity(Size + 1);
                // use dynamic programming to get the best price
                for (int i = maxQuote; i <= Size; i++)
                {
                    if (i >= dp.Count)
                        dp.Add(Double.MaxValue);
                    for (int j = 1; j <= finalDenoms[finalDenoms.Length - 1]; j++)
                    {
                        if (i < j) continue;
                        double val = (dp[i - j] * (i - j) + dp[j] * j) / i;
                        dp[i] = Math.Min(dp[i], val);
                    }
                }

                maxQuote = Size;
                return dp[Size];
            }
        }
        public static void main(String[] args)
        {
        }
    }
}