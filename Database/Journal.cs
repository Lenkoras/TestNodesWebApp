using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Index(nameof(Text))]
    public class Journal
    {
        public int Id { get; set; }
        public long EventId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
