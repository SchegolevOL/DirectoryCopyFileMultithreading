string sourceDir = @"c:\temp";
string backupDir = @"c:\temp\dir";

for (int i = 0; i < 10; i++)
{
    string path = $@"{sourceDir}\Test{i}.txt";
    FileInfo fi = new FileInfo(path);

    if (!fi.Exists)
    {
        using (StreamWriter sw = fi.CreateText())
        {
            sw.WriteLine($"Test{i}");
        }
    }
}

if (!Directory.Exists(backupDir))
{
    DirectoryInfo di = Directory.CreateDirectory(backupDir);
}
string[] fileList = Directory.GetFiles(sourceDir, "*.txt");
int begin = 0;
int end = 0;
int numberOfTasks = 10;



for (int i = 0; i < numberOfTasks; i++)
{

    if (i == numberOfTasks - 1)
    {
        end = fileList.Length;
    }
    else
    {
        end += fileList.Length / numberOfTasks;
    }
    var task = new Task(() => MyCopy(fileList, begin, end, sourceDir, backupDir));
    task.Start();
    await task;
    begin = end;

}
Console.ReadKey();


/*-----------------------------------------------------------------------------------------*/
void MyCopy(string[] fileList, int begin, int end, string sourceDir, string backupDir)
{

    for (int i = begin; i < end; i++)
    {
        string fName = fileList[i].Substring(sourceDir.Length + 1);
        try
        {
            File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
            Console.WriteLine($"File {fileList[i]} copying to dir");
        }
        catch (IOException copyError)
        {
            Console.WriteLine(copyError.Message);
        }
    }
}







//foreach (string f in fileList)
//{
//    string fName = f.Substring(sourceDir.Length + 1);
//    try
//    {
//        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
//    }
//    catch (IOException copyError)
//    {
//        Console.WriteLine(copyError.Message);
//    }
//}
