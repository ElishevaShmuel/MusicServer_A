using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UploadViewModel
    {

        public User User { get; set; }
        public MusicFile File { get; set; }
        public int CostFile { get; set; }
        public Currency Currency { get; set; }

    }
}
