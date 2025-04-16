namespace PageCounter.Data // eller valfritt namespace
{
    public class UserInputParams
    {
        public bool IsLoc { get; set; }
        public bool UseEndOfMonth { get; set; }
        public DateTime DtEndDate { get; set; }
        public int BookLength { get; set; }
    }
}
