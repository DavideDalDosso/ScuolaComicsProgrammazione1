using System;
using System.Collections.Generic;

class Conditions00
{
    static void Main(string[] args) //TEST
    {

        //Setting up the instance managing our ranking
        ScoreRanks ranks = new ScoreRanks();
        ranks.AddRank("Insufficiente", 0 , 49);
        ranks.AddRank("Sufficiente", 50, 69);
        ranks.AddRank("Buono", 70, 89);
        ranks.AddRank("Eccellente", 90, 100);
        ranks.AddRank("Over 9000", 9000, 9999);

        //Continuous reading of inputs
        while (true)
        {
            QueryScore(ranks);
        }

        
    }
    //Separated this block of code in it's own function block for readability sake
    public static void QueryScore(ScoreRanks ranks)
    {
        //The flag dictating the loop of the DO WHILE
        bool invalid;
        //The outputs from the DO WHILE
        int score = 0;
        string rank = "";
        do
        {
            //A buffer to be able to explain in the exception
            //when int.Parse fails
            string buffer = "";
            try
            {
                //Resetting the flag in case the query has previously failed
                invalid = false;

                Console.WriteLine("Please insert score: ");

                buffer = Console.ReadLine();
                //This function may fail and throw our exception
                score = int.Parse(buffer);
                //Get a mapped rank from our custom collection
                rank = ranks.Rank(score);

                //It's not necessary but it would be nice to not give blank
                //outputs in the scenario an unmapped number is put
                if (rank == "")
                {
                    Console.WriteLine("The score of '" + buffer + "' is not in valid number score range");
                    invalid = true;
                }

            }
            catch (FormatException e)
            { //Uh Oh, int.parse failed
                Console.WriteLine("The input of '" + buffer + "' is not a valid number");
                invalid = true;
            }
        } while (invalid);
        //We simply loop till the flag says ok

        //Output to the user
        Console.WriteLine("The score of '" + score + "' is: " + rank);
    }

    public class ScoreRanks
    {
        //Tuples O' Tuples
        //It may look ugly but just remember the meaning of the three fields
        //... not like it matters since it's hidden outside the class
        //A list containing tuples for storing MORE fried data per list address
        private List<Tuple<string, int, int>> ranks = new List<Tuple<string, int, int>>();

        public void AddRank(string rank, int floor, int ceiling)
        { //Apparently in order to create a Tuple you need to invoke it's static method
            ranks.Add( Tuple.Create(rank, floor, ceiling) );
        }
        public string Rank(int score)
        { //we're just looking all the fields of all ranks till we see a valid one
            foreach( var rank in ranks) 
            {
                if( score >= rank.Item2 && score <= rank.Item3)
                {
                    return rank.Item1;
                }
            }
            //Uh Oh nothing found, it will be handled outside the class
            return "";
        }
    }
}