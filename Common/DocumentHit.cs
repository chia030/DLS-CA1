using System;
using System.Collections.Generic;
using Common.BE;

namespace Common
{
    public class DocumentHit
    {
        public BEDocument Document { get; set; }

        public int NoOfHits { get; set; }

        public List<string> Missing { get; set; }
    }
}