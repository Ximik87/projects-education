using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

namespace TweetStats
{
    /// <summary>
    /// Сбор статистики
    /// </summary>
    class Statistic
    {
        /// <summary>
        /// Словарь букв
        /// </summary>
        SortedDictionary<String, Int32> dict = new SortedDictionary<String, Int32>();

        /// <summary>
        /// Общее количество букв
        /// </summary>
        Double countLetters = 0;

        /// <summary>
        /// Статистика
        /// </summary>
        SortedDictionary<String, Double> statistics = new SortedDictionary<String, Double>();

        /// <summary>
        /// Признак существования пользователя
        /// </summary>
        public Boolean UserIsExist { get; private set; }

        /// <summary>
        /// Признак наличия твитов
        /// </summary>
        public Boolean TweetsIsExist { get; private set; }

        /// <summary>
        /// Анализирует текст сообщения
        /// </summary>
        /// <param name="text">текст сообщения</param>
        private void AnalyzeTweet(String text)
        {
            MatchCollection letterMatches = Regex.Matches(text, "[a-zA-Zа-яА-Я]");
            
            foreach (Match letterMatch in letterMatches)
            {
                String letter = letterMatch.Value.ToLower();
                if (dict.ContainsKey(letter))
                {
                    dict[letter]++;
                }
                else
                {
                    dict.Add(letter, 1);
                }
            }
            countLetters += (Double)text.Length;
            
            foreach (KeyValuePair<String, Int32> item in dict)
            {
                Double round = Math.Round(((Double)item.Value / countLetters), 4);
                if (statistics.ContainsKey(item.Key))
                {
                    statistics[item.Key] = round;
                }
                else
                {
                    statistics.Add(item.Key, round);
                }
            }
        }

        /// <summary>
        /// Получение статистики в сериализованом виде
        /// </summary>
        /// <returns></returns>
        public String GetStats()
        {
            String output = JsonConvert.SerializeObject(statistics);
            return output;
        }

        /// <summary>
        /// Получает и анализирует статистику твитов
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        public void GetTweets(String username)
        {
            UserIsExist = true;
            TweetsIsExist = true;

            TwitterUser user = new TwitterUser(username);
            if (!user.Exist())
            {
                UserIsExist = false;
                Console.WriteLine("User {0} not found!", username);
                return;
            }

            List<String> tweets = user.GetTweets();
            if (tweets.Count == 0)
            {
                TweetsIsExist = false;
                Console.WriteLine("User {0} tweets not found!", username);

            }

            foreach (String tweet in tweets)
            {
                // Здесь можно распараллелить анализ твитов, каждый в своем потоке
                AnalyzeTweet(tweet);
            }
        }


    }
}
