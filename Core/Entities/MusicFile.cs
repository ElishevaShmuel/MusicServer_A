using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MusicFile
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string FileName { get; set; }
        [StringLength(50)]
        public string MimeType { get; set; }
        public long Size { get; set; }
        [StringLength(50)]
        public string FilePath { get; set; }
        public int UserId { get; set; }
        public int Cost { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User User { get; set; }



    }


    public class MusicFileDto
    {
        public  int Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string FilePath { get; set; }
        public int UserId { get; set; }
        public int Cost { get; set; }
    }

}
