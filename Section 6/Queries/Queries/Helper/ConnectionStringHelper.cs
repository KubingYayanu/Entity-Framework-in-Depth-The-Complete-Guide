using Queries.Encryption;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Helper
{
    public static class ConnectionStringHelper
    {
        public static string GetSqlConnection(string connectionStringName = "DefaultConnection")
        {
            // optionally defaults to "DefaultConnection" if no connection string name is inputted
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            // decrypt password
            string password = GetPhraseAfterWord(connectionString, "password=", ";");
            connectionString = connectionString.Replace(password, StringCipher.DecryptAES(password));
            return connectionString;
        }

        public static string GetPhraseAfterWord(string searchStringIn, string wordBeforeIn, string wordAfterIn)
        {
            int myStartPos = 0;
            string myWorkString = "";

            // get position where phrase "word_before_in" ends

            if (!string.IsNullOrEmpty(wordBeforeIn))
            {
                myStartPos = searchStringIn.ToLower().IndexOf(wordBeforeIn) + wordBeforeIn.Length;

                // extract remaining text
                myWorkString = searchStringIn.Substring(myStartPos, searchStringIn.Length - myStartPos).Trim();

                if (!string.IsNullOrEmpty(wordAfterIn))
                {
                    // get position where phrase starts in the working string
                    myWorkString = myWorkString.Substring(0, myWorkString.IndexOf(wordAfterIn)).Trim();

                }
            }
            else
            {
                myWorkString = string.Empty;
            }
            return myWorkString.Trim();
        }
    }
}
