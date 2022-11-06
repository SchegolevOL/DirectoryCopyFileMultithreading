namespace DirectoryCopyFileMultithreading.Lib
{
    public class MyFile
    {
        public static void FileCreate(string path)
        {
            using(FileStream fs = new FileStream(path,FileMode.OpenOrCreate))
            {

            }
        }
    }
}