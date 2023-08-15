using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class SwapSlotCreation
    {
        public int timeSlotDetailID1 { get; set; }
        public int timeSlotDetailID2 { get; set; }
        public string spType { get; set; }
    }
}