namespace NotesShokhov.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = "unknown";
        public string Text { get; set; } = string.Empty;
        public DateTime DateCreate { get; set; }

    }
}
