using System;


namespace TweetStats
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Statistic stats = new Statistic();
                Console.WriteLine("\nEnter username:");
                String username = Console.ReadLine();

                if (username == string.Empty)
                {
                    break;
                }

                if (username.StartsWith("@"))
                    username = username.Substring(1);
                stats.GetTweets(username);

                if (stats.UserIsExist && stats.TweetsIsExist)
                {
                    AccessTwitter.PublishTweet(String.Format("@{0}, статистика для последних 5 твитов: {1}", username, stats.GetStats()));
                }
            }
        }
    }
}
