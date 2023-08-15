using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolCoreApi.Entities
{
    public class StudentFeeMonth
    {
        public int month { get; set; }
        public int year { get; set; }
        public int isGenerated { get; set; }
    }
}