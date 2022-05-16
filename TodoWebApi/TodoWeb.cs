namespace TodoWebApi
{
    public class TodoWeb
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Createdby { get; set; } = string.Empty;
        public DateTime Createdat { get; set; } = DateTime.Now;
    }
}
