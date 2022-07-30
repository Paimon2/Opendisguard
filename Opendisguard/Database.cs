using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

internal class Database
    {

    private static SqliteConnection DbConnection;

    public static void CheckCreateTables()
    {
        DbConnection = new SqliteConnection("Data Source=opendisguard.db");
        DbConnection.Open();

        var command = DbConnection.CreateCommand();


        command.CommandText = @"CREATE TABLE IF NOT EXISTS PendingVerifications(
                                GuildId INTEGER NOT NULL,
                                UserId INTEGER NOT NULL,
                                CaptchaCode VARCHAR(8) NOT NULL);";
        command.ExecuteNonQuery();

        command.CommandText = @"CREATE TABLE IF NOT EXISTS ServerSpecificSettings(
                                JoinMessage VARCHAR(512),
                                CaptchaDifficulty INTEGER NOT NULL,
                                RoleID INTEGER NOT NULL";
        command.ExecuteNonQuery();


    }

    public static String GetVerificationCode(int UserID, int GuildID)
    {
        var command = DbConnection.CreateCommand();


        command.CommandText = @"SELECT CaptchaCode FROM PendingVerifications
                               WHERE UserId = $uid AND GuildID = $gid";

        command.Parameters.AddWithValue("$uid", UserID);
        command.Parameters.AddWithValue("gid", GuildID);



        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var code = reader.GetString(0);

                return code;
            }
        }

        return null;

    }

    }

