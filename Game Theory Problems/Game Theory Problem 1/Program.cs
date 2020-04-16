/* Game Theory Problem 1
 * A simple program used to simulate game results for game theory problem 1.
 * 
 * Evan Parry 
 * 
 */


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

      
        //Runs a game with the given strategies
        public static int runGame(int aStrat, int bStrat)
        {
            //Makes the given strategies into valid array index coordinates
            aStrat--;
            bStrat--;

            //Grabs the result from the array
            return gameArray[aStrat, bStrat];
        }

        //Generates a random strategy for A following the behavior described in the problem.
        public static int randomAStrat()
        {
            Random random = new Random();
            int result = random.Next(1, 3); //Generates a 1 or a 2
            if(result == 2) //Turns any 2s into 3s, because A will never randomly pick strategy 2.
            {
                result = 3;
            }
            return result;
        }

        //Generates a random strategy for B following the behavior described in the problem.
        public static int randomBStrat()
        {
            Random random = new Random();
            return random.Next(1, 5);
        }

        //Runs N games using random strategies for both A and B
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


        //Runs N games with a fixed strategy for A and a random strategy for B
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

        //Runs N games with a random strategy for A and a fixed strategy for B
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

        //Runs all possible A strats with random B strats and outputs the resulting averages to the console
        public static void runAllAStrats(int numGames)
        {
            double averagePayout;

            for (int i = 1; i < 4; i++)
            {
                averagePayout = runNGamesFixedAStrat(numGames, i);
                Console.WriteLine("Average Payout, with A Strategy " + i + " and Random B Strategy: " + averagePayout);
            }

        }

        //Runs all possible B strats with random A strats and outputs the resulting averages to the console
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
