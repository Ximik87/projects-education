using System;
using System.Collections.Generic;
using System.Linq;

namespace TweetStats
{
    
    /// <summary>
    /// Пользователь Twitter
    /// </summary>
    class TwitterUser
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Твиты пользователя
        /// </summary>
        List<String> tweets;

        /// <summary>
        /// Получает коллекцию твитов
        /// </summary>
        /// <returns></returns>
        public List<String> GetTweets()
        {
            tweets = AccessTwitter.GetTweets(Name).ToList();

            return tweets;
        }

        /// <summary>
        /// Проверка существует ли пользователь
        /// </summary>
        /// <returns></returns>
        public Boolean Exist()
        {
            return AccessTwitter.UserIsExist(Name);
        }

        /// <summary>
        /// Пользователь Twitter
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        public TwitterUser(String username)
        {
            Name = username;
            tweets = new List<String>();
        }

    }
}
