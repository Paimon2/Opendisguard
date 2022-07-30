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
                                UserId BIGINT NOT NULL,
                                CaptchaCode VARCHAR(8) NOT NULL);";
        command.ExecuteNonQuery();

        command.CommandText = @"CREATE TABLE IF NOT EXISTS ServerSpecificSettings(
                                JoinMessage VARCHAR(512),
                                CaptchaDifficulty INTEGER NOT NULL,
                                RoleID BIGINT NOT NULL";
        command.ExecuteNonQuery();
        

    }

    public static String GetVerificationCode(ulong UserID)
    {
        var command = DbConnection.CreateCommand();


        command.CommandText = @"SELECT CaptchaCode FROM PendingVerifications
                               WHERE UserId = $uid;";

        command.Parameters.AddWithValue("$uid", UserID);


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


    public static void AddVerificationCode(ulong UserID, String CaptchaCode)
    {

        var command = DbConnection.CreateCommand();


        command.CommandText = @"INSERT INTO PendingVerifications(UserID,, CaptchaCode) 
                               VALUES($uid, $code);";

        command.Parameters.AddWithValue("$uid", UserID);
        command.Parameters.AddWithValue("$code", CaptchaCode);

        command.ExecuteNonQuery();
    }
    }


