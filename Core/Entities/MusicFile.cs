using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MusicFile
    {

        public int Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string FilePath { get; set; }
        public string UserId { get; set; }
        public int Cost { get; set; }

    }
}
