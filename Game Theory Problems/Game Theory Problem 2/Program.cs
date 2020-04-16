/*
Two clothing stores in a shopping center compete for the weekend trade. On a clear day, the larger
store gets 60% of the business and on a rainy day the larger store gets 80% of the business. 

Either or both stores may hold a sidewalk sale on a given weekend, but the decision must be made a week in
advance and in ignorance of the competitor’s plans. 
If both have a sidewalk sale, each gets 50% of the
business. 

If, however, one holds the sale and the other doesn’t, the one conducting the sale gets 90% of
the business on a clear day and 10% on a rainy day. 
It rains 40% of the time. How frequently should
each retailer conduct sales?
*/

//What if we know that it is going to rain? What then?
//What if we don't know?

//We are going to assum a weekend sale is 1 day, and that the chance of rain in a day is 40%

using System;

namespace Game_Theory_Problem_2
{
    static class Program
    {
        //it rains 40% of the time
        const double RAIN_PERCENT = .4;

        //If both have a sidewalk sale, each gets 50% of the business.
        const double DOUBLE_SALE_BUSINESS_PERCENT = .5;

        //This is the percent of business a shop will get if they are the only one doing a sale, and it depends on the weather.
        static double singleSaleBusinessPercent;


        // These are what we want to calculate. This is the percentage of weekends that each will hold a sidewalk sale 
        static double largeCenterSalePercent;
        static double smallCenterSalePercent;


        static void SetBusinessPercents(bool isRainy, out double lc_ns_bp, out double sc_ns_bp, out double ss_bp)
        {
            if (isRainy)
            {

                lc_ns_bp = .8;
                sc_ns_bp = .2;
                ss_bp = .1;
            }
            else
            {
                lc_ns_bp = .6;
                sc_ns_bp = .4;
                ss_bp = .9;
            }
        }

        static Random s_Random = new Random(Guid.NewGuid().GetHashCode());

        static bool EventOccurred(double prob)
        {
            //percent will range from 0 to 99 (values will be less than maximum 100)
            int perCent = s_Random.Next(0, 100);
            if (perCent < prob * 100)
            {
                return true;
            }
            else
                return false;
        }

        static void GetBusinessPercentBySales(bool sc_hasSale, bool lc_hasSale, double sc_ns_bp, double lc_ns_bp, double ss_bp, double ds_bpm, out double sc_bp, out double lc_bp)
        {
            if (sc_hasSale && sc_hasSale)
            {
                lc_bp = DOUBLE_SALE_BUSINESS_PERCENT;
                sc_bp = DOUBLE_SALE_BUSINESS_PERCENT;
            }
            else if (sc_hasSale)
            {
                sc_bp = ss_bp;
                lc_bp = 1 - sc_bp;
            }
            else if (lc_hasSale)
            {
                lc_bp = ss_bp;
                sc_bp = 1 - lc_bp;
            }
            else
            {
                lc_bp = lc_ns_bp;
                sc_bp = sc_ns_bp;
            }
        }


        static void GetTotalBusinessPercentForSalePercents(double sc_sp, double lc_sp, int totalWeeks, out double sc_tbp, out double lc_tbp, out double actualRainPercent)
        {
            // These are the percentages of business each shop gets when neither hold a sale, and they depend on the weather.
            //large and small center _ no sale _ business percent
            double lc_ns_bp;
            double sc_ns_bp;

            //Percent of business a shop will get if it is the only one holding a sale;
            //single sale _ business percent
            double ss_bp;

            //small and large center _ total business percent
            sc_tbp = 0;
            lc_tbp = 0;

            actualRainPercent = 0;

            //weekends passed
            for (int wp = 1; wp <= totalWeeks; wp++)
            {
                var isRainy = EventOccurred(RAIN_PERCENT);
                if (isRainy) actualRainPercent++;
                bool lc_hasSale = EventOccurred(lc_sp);
                bool sc_hasSale = EventOccurred(sc_sp);

                SetBusinessPercents(isRainy, out lc_ns_bp, out sc_ns_bp, out ss_bp);
                GetBusinessPercentBySales(sc_hasSale, lc_hasSale, sc_ns_bp, lc_ns_bp, ss_bp, DOUBLE_SALE_BUSINESS_PERCENT, out double sc_bp, out double lc_bp);
                sc_tbp += sc_bp;
                lc_tbp += lc_bp;

            }
            //average the percentage
            sc_tbp /= (double)totalWeeks;
            lc_tbp /= (double)totalWeeks;
            actualRainPercent /= (double)totalWeeks;
        }

        static string GetBestsString(string name, Tuple<double, double, double, double, double> bestForTuple)
        {
            return $"Best For {name}: \n" +
                $"  Small Center Sale Percent:{ bestForTuple.Item1.ToString("#0.000")}\n" +
                $"  Large Center Sale Percent:{ bestForTuple.Item2.ToString("#0.000")}\n" +
                $"  Small Center Business Share Percent: {bestForTuple.Item3.ToString("#0.000")}\n" +
                $"  Large Center Business Share Percent: {bestForTuple.Item4.ToString("#0.000")}\n" +
                $"  Actual Rain Percent (Expected is .4): {bestForTuple.Item5.ToString("#0.000")}\n";
        }


        static void CalculateBestSalePercents()
        {
            const int TOTAL_WEEKS = 100_000;

            //Tupl<sc_sp, lc_sp, sc_tbp, lc_tbp, actual rain percent>
            Tuple<double, double, double, double, double> bestForSmallCenter = new Tuple<double, double, double, double, double>(0, 0, 0, 0, 0);
            Tuple<double, double, double, double, double> bestForLargeCenter = new Tuple<double, double, double, double, double>(0, 0, 0, 0, 0);
            Tuple<double, double, double, double, double> bestForBoth = new Tuple<double, double, double, double, double>(0, 0, 0, 0, 0);

            for (double lc_sp = 0; lc_sp <= 1; lc_sp += .01)
            {
                for (double sc_sp = 0; sc_sp <= 1; sc_sp += .01)
                {
                    GetTotalBusinessPercentForSalePercents(sc_sp, sc_sp, TOTAL_WEEKS, out double sc_tbp, out double lc_tbp, out double actualRainPercent);
                    if (bestForSmallCenter.Item3 < sc_tbp)
                    {
                        bestForSmallCenter = new Tuple<double, double, double, double, double>(sc_sp, lc_sp, sc_tbp, lc_tbp, actualRainPercent);
                    }
                    if (bestForLargeCenter.Item4 < sc_tbp)
                    {
                        bestForLargeCenter = new Tuple<double, double, double, double, double>(sc_sp, lc_sp, sc_tbp, lc_tbp, actualRainPercent);
                    }
                    Console.WriteLine($"Trying large sale prob {lc_sp.ToString("#0.000")} and small sale prob {sc_sp.ToString("#0.000")}");

                }
            }
            Console.WriteLine($"Final Results:\n" +
                $"Weeks per trial: {TOTAL_WEEKS}\n" +
                $"{GetBestsString("Small Center", bestForSmallCenter)}" +
                $"{GetBestsString("Large Center", bestForLargeCenter)}"
            );
            //Console.WriteLine($"Small Center Business Percent: {sc_tbp}\n" +
            //    $"Large Center Business Percent: {lc_tbp}\n" +
            //    $"Total Business (Should be very close to 1): {sc_tbp + lc_tbp}");

        }


        static void Main(string[] args)
        {
            CalculateBestSalePercents();



        }
    }
}
