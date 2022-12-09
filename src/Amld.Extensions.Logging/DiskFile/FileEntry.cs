namespace Amld.Extensions.Logging.DiskFile
{
    public class FileEntry
    {
        public string FileName { get;set; }
        public string Msg { get; set; }
        public FileEntry(string msg)
        {
            FileName =DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".log";
            Msg = msg;
        }
    }
}
