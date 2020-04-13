using System;

namespace Game_Theory_Problem_1
{
    class Program
    {



        public static int[,] gameArray = new int[3, 4] {
            {-4, 6, -4, 1} ,   /*  initializers for row indexed by 0 */
            {5, -7, 3, 8} ,   /*  initializers for row indexed by 1 */
            {-8, 0, 6, -2}   /*  initializers for row indexed by 2 */
        };

        static void Main(string[] args)
        {
            int gamesToRun = 1000;
            Console.WriteLine("Running " + gamesToRun + " Games");


            double averagePayout = runNGames(gamesToRun);

            Console.Write("Average Payout: " + averagePayout);
            /*
            int result = runGame(aStrat, bStrat);

            Console.WriteLine("A Strategy: " + aStrat + " B Strategy: " + bStrat);
            Console.WriteLine("Game result: " + result);
            */
        }


        public static int runGame(int aStrat, int bStrat)
        {
            aStrat--;
            bStrat--;
            return gameArray[aStrat, bStrat];
        }

        public static int randomAStrat()
        {
            Random random = new Random();
            int result = random.Next(1, 3);
            if(result == 2)
            {
                result = 3;
            }
            return result;
        }

        public static int randomBStrat()
        {
            Random random = new Random();
            return random.Next(1, 5);
        }

        public static double runNGames(int numOfGames)
        {
            double averagePayout = 0.0f;
            for(int i = 0; i < numOfGames; i++)
            {
                int aStrat = randomAStrat();
                int bStrat = randomBStrat();

                averagePayout += runGame(aStrat, bStrat);
            }

            averagePayout = averagePayout/numOfGames;
            return averagePayout;
            
            
        }

        
    }

    
}
