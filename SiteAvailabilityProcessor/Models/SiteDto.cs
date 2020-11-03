using System;
using System.Collections.Generic;
using System.Text;

namespace SiteAvailabilityProcessor.Models
{
    public class SiteDto
    {
        public string UserId { get; set; }
        public string Site { get; set; }
        public int Status { get; set; }
    }
}
