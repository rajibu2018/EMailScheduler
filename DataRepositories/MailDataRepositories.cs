using MailSchedulerModels;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataRepositories {
    public class MailDataRepositories : IMailDataRepositories {

        public string ConnectionString { get; set; }
        public MailDataRepositories() {
            ConnectionString = @"Data Source=DELL-PC\SQLEXPRESS;Initial Catalog=MailServer;Integrated Security=True;Pooling=False";
        }

        public List<User> GetUsersToSendPrimaryMail() {
            var queryString = "SELECT Id, Name, MailId, SentPrimaryMail FROM Users WHERE SentPrimaryMail=1";
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    users.Add(new User {
                        Id = reader["Id"] != null ? Convert.ToInt32(reader["Id"]) : 0,
                        Name = reader["Name"] != null ? reader["Name"].ToString() : string.Empty,
                        MailId = reader["MailId"] != null ? reader["MailId"].ToString() : string.Empty,
                        SentPrimaryMail = reader["SentPrimaryMail"] != null ? (bool)reader["SentPrimaryMail"] : false
                    });
                }

                reader.Close();
                connection.Close();
            }
            return users;
        }

        public void SaveEmailSentHistory(User user, Guid guid) {
            var queryString = "INSERT INTO MailSentHistory ([UserId],[LinkUID]) VALUES (" + user.Id + ",'" + guid + "')";
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<UserEmailServiceModel> GetUsersToSendFollowUpMail() {
            var queryString = "SELECT  U.Name, U.MailId, M.LinkUID FROM Users U JOIN MailSentHistory M ON U.Id=M.UserId WHERE M.LinkOpned=0 AND DATEDIFF(DAY, M.SentDate,GETDATE()) <= 3";
            var users = new List<UserEmailServiceModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    users.Add(new UserEmailServiceModel {
                        Name = reader["Name"] != null ? reader["Name"].ToString() : string.Empty,
                        MailId = reader["MailId"] != null ? reader["MailId"].ToString() : string.Empty,
                        LinkUID = reader["LinkUID"] != null ? (Guid)reader["LinkUID"] : new Guid()
                    });
                }

                reader.Close();
                connection.Close();
            }
            return users;
        }
    }
}
