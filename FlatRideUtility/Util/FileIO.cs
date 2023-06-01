using System.IO;

namespace FlatRideUtility.Util
{
    public static class FileIO
    {
        public static string FilePath
        {
            get
            {
                return FilePaths.getFolderPath("FileIO_Log.txt");
            }
        }

        public static void Clear()
        {
            File.WriteAllText(FilePath, "");
        }

        public static void WriteAll(string @string)
        {
            File.WriteAllText(FilePath, @string);
        }
        public static void Write(string @string)
        {
            File.AppendAllText(FilePath, @string + '\n');
        }
        public static void WriteException(string @string)
        {
            Write("----- Exception -----");
            Write(@string);
            Write("-------- End --------");
        }
    }
}
