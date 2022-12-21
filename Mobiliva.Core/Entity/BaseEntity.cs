using System;
using static Mobiliva.Core.Enums.Enums;

namespace Mobiliva.Core.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public RecordStatus RecordStatus { get; set; }
    }
}

