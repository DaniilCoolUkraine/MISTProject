using MistProject.Requests.Response;

namespace MistProject.General
{
    public static class Extensions
    {
        public static byte[] ToByte(this string me)
        {
            return System.Text.Encoding.UTF8.GetBytes(me);
        }

        public static string GetText(this IResponseData rawData)
        {
            return System.Text.Encoding.UTF8.GetString(rawData.Data);
        }
    }
}