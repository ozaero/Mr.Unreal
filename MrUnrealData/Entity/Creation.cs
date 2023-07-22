using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrUnrealData.Entity
{
    public class Creation : BaseEntity
    {
        public string CreationName { get; set; }
        public string ArtistName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
    }
}
