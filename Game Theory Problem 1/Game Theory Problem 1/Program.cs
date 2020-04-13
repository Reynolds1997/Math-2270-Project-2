using System;

namespace Game_Theory_Problem_1
{
    class Program
    {


        //This array stores the possible results shown in the table for the game.
        public static int[,] gameArray = new int[3, 4] {
            {-4, 6, -4, 1} ,  
            {5, -7, 3, 8} ,  
            {-8, 0, 6, -2}   
        };

        static void Main(string[] args)
        {
            int gamesToRun = 100000;
            Console.WriteLine("Running " + gamesToRun + " Games");

            double averagePayout = runNGamesRandStrats(gamesToRun);

            Console.WriteLine("Average Payout, Random Strategies: " + averagePayout);

            runAllAStrats(gamesToRun);
            runAllBStrats(gamesToRun);
          
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

        public static double runNGamesRandStrats(int numOfGames)
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


        public static double runNGamesFixedAStrat(int numOfGames, int aStrategy)
        {
            double averagePayout = 0.0f;
            for (int i = 0; i < numOfGames; i++)
            {
                int aStrat = aStrategy;
                int bStrat = randomBStrat();
                averagePayout += runGame(aStrat, bStrat);
            }

            averagePayout = averagePayout / numOfGames;
            return averagePayout;

        }

        public static double runNGamesFixedBStrat(int numOfGames, int bStrategy)
        {
            double averagePayout = 0.0f;
            for (int i = 0; i < numOfGames; i++)
            {
                int aStrat = randomAStrat();
                int bStrat = bStrategy;
                averagePayout += runGame(aStrat, bStrat);
            }

            averagePayout = averagePayout / numOfGames;
            return averagePayout;

        }

        public static void runAllAStrats(int numGames)
        {
            double averagePayout;

            for (int i = 1; i < 4; i++)
            {
                averagePayout = runNGamesFixedAStrat(numGames, i);
                Console.WriteLine("Average Payout, with A Strategy " + i + " and Random B Strategy: " + averagePayout);
            }

        }

        public static void runAllBStrats(int numGames)
        {
            double averagePayout;

            for (int i = 1; i < 5; i++)
            {
                averagePayout = runNGamesFixedBStrat(numGames, i);
                Console.WriteLine("Average Payout, with Random A Strategy and B Strategy " + i + ": " + averagePayout);
            }

        }


    }

    
}
