using System;
using System.Collections.Generic;
using System.Linq;

using Tweetinvi;
using Tweetinvi.Models;

namespace TweetStats
{
    /// <summary>
    /// Обеспечивает доступ к API Twitter
    /// </summary>
    class AccessTwitter
    {
        // Test Account https://twitter.com/Dmitry91166463
        private static string _consumerKey = "EKcn30PmdeRxIhcQu2hbiYV6L";
        private static string _consumerSecret = "SsrmrpIPO1XqbxeuwXiXkmru281hI8a29QM3Hmsx0EPabJfD85";
        private static string _accessToken = "902769099280961537-CBeZwZDfwVlvTZGBIcMpxe35Ln89kWI";
        private static string _accessTokenSecret = "OWE8Doizq5zkqJvN3sIMYeq28Qav0Bgy7Fif8JRy3sQhl";

        private static Int32 countTweets = 5;

        /// <summary>
        /// Получает коллекцию твитов пользователя 
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns></returns>
        public static IEnumerable<String> GetTweets(String username)
        {
            List<String> tweets = new List<String>();
            IEnumerable<ITweet> fullTweets;
            try
            {
                Auth.SetUserCredentials(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
                fullTweets = Timeline.GetUserTimeline(username, countTweets);
                                
                if (fullTweets == null)
                    return Enumerable.Empty<String>();

                return fullTweets.Select(t => t.Text).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error extracting tweets");
            }

            return Enumerable.Empty<String>();
        }

        /// <summary>
        /// Публикует твит пользователя
        /// </summary>
        /// <param name="text">текст твита</param>
        public static void PublishTweet(String text)
        {
            String message;
            if (text.Length >= 140)
            {
                message = text.Substring(0, 140);
            }
            else
            {
                message = text;
            }

            try
            {
                Auth.SetUserCredentials(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
                Tweet.PublishTweet(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error publish tweet");
            }
        }

        /// <summary>
        /// Проверяет существует ли пользователь.
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <returns></returns>
        public static Boolean UserIsExist(String name)
        {
            Auth.SetUserCredentials(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
            IUser user = User.GetUserFromScreenName(name);

            return user != null ? true : false;
        }

    }
}
