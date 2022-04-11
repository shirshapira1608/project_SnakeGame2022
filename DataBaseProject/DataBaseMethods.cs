using DataBaseProject.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace DataBaseProject
{
    public static class DataBaseMethods
    {
        private static string dbPath = ApplicationData.Current.LocalFolder.Path;
        private static string connectionString = "Filename=" + dbPath + "\\DBGame.db";
        public static User GetUser(string userName, string userPassword)
        {
            string query = $"SELECT * FROM [Users] WHERE UserName='{userName}'  AND Password='{userPassword}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        CurrentCharacter = reader.GetInt32(3),
                        CurrentBackground = reader.GetInt32(4),
                        Coins = reader.GetInt32(5),
                        MaxScore = reader.GetInt32(6),
                        Mail = reader.GetString(7),
                        CurrentFood = reader.GetInt32(8)
                    };
                    return user;
                }
            }
            return null;
        }
        public static User ForgotPassword(string userName, string mail)
        {
            string query = $"SELECT * FROM [Users] WHERE UserName='{userName}'  AND Mail='{mail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Password = reader.GetString(2),
                        CurrentCharacter = reader.GetInt32(3),
                        CurrentBackground = reader.GetInt32(4),
                        Coins = reader.GetInt32(5),
                        MaxScore = reader.GetInt32(6),
                        Mail = reader.GetString(7),
                        CurrentFood = reader.GetInt32(8)
                    };
                    return user;
                }
            }
            return null;
        }
        public static User AddUser(string name, string password, string mail)
        {
            User user = GetUser(name, password);
            if (user != null)
                return null;
            string query = $"INSERT INTO [Users] (UserName, Password, CurrentCharacter, CurrentBackground, Coins, MaxScore, Mail, CurrentFood) VALUES ('{name}', '{password}', {0}, {0}, {0}, {0}, '{mail}', {0})";
            Execute(query);
            user = GetUser(name, password);
            return (user);
        }
        public static List<Purchases> GetPurchases(int userId)
        {
            List<Purchases> purchases = new List<Purchases>();
            string query = $"SELECT * FROM [Purchases] WHERE UserId = {userId}";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Purchases purchase = new Purchases(userId, reader.GetString(1), reader.GetInt32(2));
                        purchases.Add(purchase);
                    }
                }
            }
            return purchases;
        }
        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public static User UpdateMaxScore(User user, int currentScore)
        {
            string query;
            int maxScore = user.MaxScore;
            int coins = user.Coins;
            if (currentScore > maxScore)
            {
                query = $"UPDATE [Users] SET MaxScore = {currentScore}, Coins = {coins + currentScore} WHERE UserName = '{user.UserName}'";
                Execute(query);
            }
            else
            {
                query = $"UPDATE [Users] SET Coins = {coins + currentScore} WHERE UserName = '{user.UserName}'";
                Execute(query);
            }
            return GetUser(user.UserName, user.Password);
        }
        public static User BuyProduct(User user, int price, Purchases purchase)
        {
            string query;
            if (price < user.Coins)
            {
                query = $"UPDATE [Users] SET Coins = {user.Coins - price} WHERE UserName = '{user.UserName}'";
                Execute(query);
                query = $"INSERT INTO [Purchases] (UserId, Product, ProductSerialNumber) VALUES ({user.Id}, '{purchase.Product}', {purchase.ProductSerialNumber})";
                Execute(query);
            }
            return GetUser(user.UserName, user.Password);
        }
        public static User UseProduct(User user, Purchases purchase)
        {
            string query = "";
            switch (purchase.Product)
            {
                case "background":
                    query = $"UPDATE [Users] SET CurrentBackground = {purchase.ProductSerialNumber} WHERE UserName = '{user.UserName}'";
                    break;
                case "food":
                    query = $"UPDATE [Users] SET CurrentFood = {purchase.ProductSerialNumber} WHERE UserName = '{user.UserName}'";
                    break;
                case "head":
                    query = $"UPDATE [Users] SET CurrentCharacter = {purchase.ProductSerialNumber} WHERE UserName = '{user.UserName}'";
                    break;
            }
            Execute(query);
            return GetUser(user.UserName, user.Password);
        }
        public static List<User> RecordTable(User user)
        {
            List<User> users = new List<User>();
            string query = $"SELECT * FROM [Users]";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User a = new User
                        {
                            Id = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            CurrentCharacter = reader.GetInt32(3),
                            CurrentBackground = reader.GetInt32(4),
                            Coins = reader.GetInt32(5),
                            MaxScore = reader.GetInt32(6),
                            Mail = reader.GetString(7),
                            CurrentFood = reader.GetInt32(8)
                        };
                        users.Add(a);
                    }
                }
            }
            List<User> topUsers = new List<User>();
            topUsers.Add(user);
            User user1 = new User();
            user1.MaxScore = 0;
            for (int i = 0; i < 3; i++)
            {
                foreach (User a in users)
                {
                    if (a.MaxScore >= user1.MaxScore)
                        user1 = a;
                }
                users.Remove(user1);
                topUsers.Add(user1);
                user1 = new User();
                user1.MaxScore = 0;
            }
            return (topUsers);
        }
        public static User ChangePassword(string userName, string newPass)
        {
            string query = $"UPDATE [Users] SET Password = '{newPass}' WHERE UserName = '{userName}'";
            Execute(query);
            return GetUser(userName, newPass);
        }
    }
}
