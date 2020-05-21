using System;
using SharedCoreWebApp.ContextFactory;

namespace SharedCoreWebApp.GlobalHelpers
{
    public class MyGlobal
    {
        public static string RecursiveExecptionMsg(Exception e)
        {
            string msg = null;
            Exception e2 = e;
            while (e2 != null)
            {
                msg += e2.Message;
                e2 = e2.InnerException;
            }

            return msg;
        }

        public static string ExtractUniqueNameForHandler(string callbackQueryData)
        {
            return callbackQueryData.Split('_')[0];
        }

        public static string ExtractValueInlineQuery(string idstr)
        {
            return idstr.Split('_')[1];
        }
        public static DateTime CreateDateFromTime(int year, int month, int day, DateTime time)
        {
            return new DateTime(year, month, day, time.Hour, time.Minute, 0);
        }

        public static int ValidateHash(string hash)
        {
            string userIdstr = EncryptionHelper.Decrypt(hash);

            userIdstr = userIdstr.Split('_')[0];

            int userId = int.Parse(userIdstr);
            return userId;
        }

        public static string Encrypt(string txt)
        {
            var now = txt + "_" + DateTime.Now;
            return EncryptionHelper.Encrypt(now);
        }


        public static string SplitAndGetRest(string str, string tosub)
        {
            var i = str.IndexOf(tosub, StringComparison.CurrentCulture);

            var start = i + tosub.Length;
            var length = str.Length - start;
            return str.Substring(start, length);
        }

        public static string GetTelegramChatId(string address)
        {
            return MyGlobal.SplitAndGetRest(address, "t.me/");
        }

      
        public static bool IsUnitTestEnvirement = false;
    }
}