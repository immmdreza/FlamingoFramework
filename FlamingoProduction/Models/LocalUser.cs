using System.ComponentModel.DataAnnotations;

namespace FlamingoProduction.Models
{
    public class LocalUser
    {
        [Key]
        public int LocalId { get; set; }

        public long TelegramId { get; set; }
    }
}
