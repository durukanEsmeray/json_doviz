using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doviz.Entities
{
    public class ParaBirimi
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Tanim { get; set; }
        public decimal UyariLimit { get; set; }

    }
}
