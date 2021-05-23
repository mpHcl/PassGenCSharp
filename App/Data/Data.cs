using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PassGenCSharp.App.Data
{
    class Data {
        SQLiteConnection connection;
        public Data() {
            string dataPath = @"URI=file:C:\Programowanie\Projekty\PassGenCSharp\App\Data\data.db";
            connection = new SQLiteConnection(dataPath);
        }

        public Data(string path) {
            Console.WriteLine(path);
            path += "\\data.db";
            if (!File.Exists(path)) {
                var file = File.Create(path);
                file.Close();
                Console.WriteLine(@"URI=flie:" + path);
                connection = new SQLiteConnection("URI=file:" + path);
                connection.Open();
                var commandText = "CREATE TABLE Passwords (" +
                                  "\"ID\"	INTEGER NOT NULL UNIQUE, " +
                                  "\"Platform\"  TEXT NOT NULL, " +
                                  "\"Email\" TEXT, " +
                                  "\"Password\"  TEXT NOT NULL, " +
                                  "\"Nickname\"  TEXT, " +
                                  "\"Description\"   TEXT, " +
                                  "PRIMARY KEY(\"ID\" AUTOINCREMENT) " +
                                  ")";
                var command = new SQLiteCommand(commandText, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            else {
                connection = new SQLiteConnection("URI=file:" + path);
            }
            
        }

        public List<Model> getData() {
            List<Model> results = new List<Model>();
            connection.Open();
            string commandText = "SELECT * FROM Passwords";
            var command = new SQLiteCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read()) {
                Model item = new Model();
                item.Platform = reader.GetString(1);
                item.Password = reader.GetString(3);
                item.ID = reader.GetInt32(0);
                results.Add(item);
            }

            connection.Close();

            return results;
        }
        
        public List<String> getPlatforms() {
            List<string> results = new List<string>();
            connection.Open();
            string commandText = "SELECT DISTINCT Platform FROM Passwords ORDER BY Platform ASC";
            var command = new SQLiteCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read()) {
                string platform = reader.GetString(0);
                results.Add(platform);
            }

            connection.Close();

            return results;
        }

        public void AddRecord(Model model) {
            connection.Open();
            string commandText = "INSERT INTO Passwords " +
                                 "(Platform, Email, Password, Description, Nickname) " +
                                $"VALUES ('{ model.Platform }', '{model.Email}'," +
                                $" '{model.Password}', '{model.Description}', '{ model.Nickname }' )";
            var command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Model getDetails(int ID) {
            Model result = new Model();
            
            connection.Open();
            string commandText = $"SELECT * FROM Passwords WHERE ID = {ID}";
            var command = new SQLiteCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read()) {
                result.ID = reader.GetInt32(0);
                result.Platform = reader.GetString(1);
                result.Email = reader.GetString(2);
                result.Password = reader.GetString(3);
                result.Description = reader.GetString(4);
                result.Nickname = reader.GetString(5);

                break;
            }
            connection.Close();
            return result;
        }

        public List<Model> getDetails(string title) {
            List<Model> results = new List<Model>();
            connection.Open();
            string commandText = $"SELECT * FROM Passwords WHERE Platform ='{title}'";
            var command = new SQLiteCommand(commandText, connection);
            var reader = command.ExecuteReader();

            
            while (reader.Read()) {
                Model item = new Model();
                item.ID = reader.GetInt32(0);
                item.Platform = reader.GetString(1);
                item.Email = reader.GetString(2);
                item.Password = reader.GetString(3);
                item.Description = reader.GetString(4);
                item.Nickname = reader.GetString(5);

                results.Add(item);
            }

            connection.Close();
            return results;
        }

        public void updateDetails(Model model) {
            connection.Open();
            string commandText = $"UPDATE Passwords " +
                                 $"SET Platform = '{model.Platform}'," +
                                 $"Password = '{model.Password}', " +
                                 $"Email = '{model.Email}'," +
                                 $"Description = '{model.Description}', " +
                                 $"Nickname = '{model.Nickname}' " +
                                 $"WHERE ID = '{model.ID}'";
            var command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
