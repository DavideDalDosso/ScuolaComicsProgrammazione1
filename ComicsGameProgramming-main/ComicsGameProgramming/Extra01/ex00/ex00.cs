using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static ex00.ScoreManager;

class ex00
{
    public static void Main(string[] args) {
    
        ScoreManager scoreManager = new ScoreManager();
        scoreManager.AddMessage("Partita equilibrata", 5);
        scoreManager.AddMessage("Partita interessante", 7.5f);
        scoreManager.AddMessage("Partita intensa", 10);

        scoreManager.Add("Blu");
        scoreManager.Add("Blu", "Marco");
        scoreManager.SetScore("Marco", 20);
        scoreManager.Add("Blu", "Mirko");
        scoreManager.SetScore("Mirko", 24);
        scoreManager.Add("Blu", "Mario");
        scoreManager.SetScore("Mario", 17);
        scoreManager.Add("Blu", "Maria");
        scoreManager.SetScore("Maria", 10);

        scoreManager.Add("Rossi");
        scoreManager.Add("Rossi", "Gianluca");
        scoreManager.SetScore("Gianluca", 17);
        scoreManager.Add("Rossi", "Giangianni");
        scoreManager.SetScore("Giangianni", 9);
        scoreManager.Add("Rossi", "Giangiacomo");
        scoreManager.SetScore("Giangiacomo", 2);
        scoreManager.Add("Rossi", "Giancosimo");
        scoreManager.SetScore("Giancosimo", 27);
        scoreManager.Add("Rossi", "Gianspazzatura");
        scoreManager.SetScore("Gianspazzatura", -10);

        Console.WriteLine(scoreManager.GetMessage());
    }

    public class ScoreManager
    {
        public class Player
        {
            public float score;
            public string name;
            public Team team;
        }
        public class Team { 
            public List<Player> players = new List<Player>();
        }
        public class Message : IComparable<Message>
        {
            public float difference;
            public string message = "";

            public int CompareTo(Message? other)
            {
            return difference.CompareTo(other.difference);
            }
        }

        private Dictionary<string, Team> teams = new Dictionary<string, Team>();
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        private List<Message> messages = new List<Message>();

        public void Add(string team)
        {
            Team teamInstance = new Team();
            teams.Add(team, teamInstance);
        }
        public void Add(string team, string player)
        {
            if (!teams.ContainsKey(team)) Add(team);
            Team teamInstance = teams[team];

            Player playerInstance = new Player();
            playerInstance.name = player;
            playerInstance.team = teamInstance;

            teams[team].players.Add(playerInstance);
            players.Add(player, playerInstance);
        }
        public void SetScore(string player,  float score)
        {
            players[player].score = score;
        }
        public void Remove(string player)
        {
            Player playerInstance = players[player];
            players.Remove(player);
            playerInstance.team.players.Remove(playerInstance);
        }
        public Player[] GetPlayers()
        {
            return players.Values.ToArray();
        }
        public Player[] GetTeamPlayers(string team)
        {
            return teams[team].players.ToArray();
        }
        public void AddMessage(string message, float scoreDifference)
        {
            Message messageInstance = new Message();
            messageInstance.message = message;
            messageInstance.difference = scoreDifference;
            messages.Add(messageInstance);
            messages.Sort();
        }
        public string GetMessage()
        {
            float[] teamScore = new float[teams.Count];
            Team[] teamArray = teams.Values.ToArray();
            float teamAverage = 0;
            float averageDeviation;

            float smallestValue = float.MaxValue;

            for (int i = 0; i < teams.Count; i++)
            {
                for(int j = 0; j < teamArray[i].players.Count; j++)
                {
                    teamScore[i] += teamArray[i].players[j].score;
                }
                teamScore[i] /= teamArray[i].players.Count;
                teamAverage += teamScore[i];

                if (teamScore[i] < smallestValue)
                {
                    smallestValue = teamScore[i];
                }
            }
            teamAverage /= teams.Count;

            averageDeviation = teamAverage - smallestValue;

            for(int i=0; i < messages.Count; i++)
            {
                if (averageDeviation < messages[i].difference)
                {
                    return messages[i].message;
                }
            }
            return messages[messages.Count - 1].message;
        }
    }
}
