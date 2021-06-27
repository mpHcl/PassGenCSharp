using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using PassGenCSharp.App.Security;

namespace PassGenCSharp.App.Data
{
    public class Data {
        SQLiteConnection connection;
        StringAES aes = new StringAES();
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
                                $" '{aes.BytesArrayAsString(aes.EncryptStringToBytes(model.Password))}'," +
                                $" '{model.Description}', '{ model.Nickname }' )";
            var command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            connection.Close();
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
                item.Password = aes.DecryptBytesToString(aes.StringAsByteArray(reader.GetString(3)));
                item.Description = reader.GetString(5);
                item.Nickname = reader.GetString(4);


                results.Add(item);
            }

            connection.Close();
            return results;
        }

        public void updateDetails(Model model) {
            connection.Open();
            string commandText = $"UPDATE Passwords " +
                                 $"SET Platform = '{model.Platform}'," +
                                 $"Password = '{aes.BytesArrayAsString(aes.EncryptStringToBytes(model.Password))}', " +
                                 $"Email = '{model.Email}'," +
                                 $"Description = '{model.Description}', " +
                                 $"Nickname = '{model.Nickname}' " +
                                 $"WHERE ID = '{model.ID}';";
            new SQLiteCommand(commandText, connection).ExecuteNonQuery();
            connection.Close();
        }

        public void deleteRecord(Model model) {
            connection.Open();
            string commandText = $"DELETE FROM Passwords WHERE ID = {model.ID}";
            new SQLiteCommand(commandText, connection).ExecuteNonQuery();


            connection.Close();
        }
    }
}
