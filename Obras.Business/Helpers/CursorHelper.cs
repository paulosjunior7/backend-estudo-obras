namespace Obras.Business.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class CursorHelper
    {
        public static string ToCursor(int id) => Convert.ToBase64String(BitConverter.GetBytes(id));

        public static string ToCursor(string id) => Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(id));

        public static int FromCursor(string base64) => BitConverter.ToInt32(Convert.FromBase64String(base64), 0);

        public static string FromCursorString(string base64) => BitConverter.ToString(Convert.FromBase64String(base64), 0);

        public static (string firstCursor, string lastCursor) GetFirstAndLastCursor(IEnumerable<string> enumerable)
        {
            if (enumerable?.Any() != true)
            {
                return (null, null);
            }

            var firstCursor = ToCursor(enumerable.First());
            var lastCursor = ToCursor(enumerable.Last());

            return (firstCursor, lastCursor);
        }

        public static (string firstCursor, string lastCursor) GetFirstAndLastCursor(IEnumerable<int> enumerable)
        {
            if (enumerable?.Any() != true)
            {
                return (null, null);
            }

            var firstCursor = ToCursor(enumerable.First());
            var lastCursor = ToCursor(enumerable.Last());

            return (firstCursor, lastCursor);
        }
    }
}
