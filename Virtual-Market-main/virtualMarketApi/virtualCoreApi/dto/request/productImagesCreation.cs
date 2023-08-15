using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtualCoreApi.Entities
{
    public class ProductImagesCreation
    {
        public int productImageID { get; set; }
        public string applicationEDocPath { get; set; }
        public string applicationEdocExtenstion { get; set; }
        public string applicationEDoc { get; set; }
    }
}