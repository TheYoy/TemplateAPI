using System;
using System.Collections.Generic;
namespace APITemplate.DependencyInjecyions.Microservices.Tlog.Response
{
    public class TlogPlaceResponseV1 {
        public int LID { get; set; }
        public string LNAME { get; set; }
        public string LDETAIL { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }
        public string MNICKNAME { get; set; }
        public string MNAME { get; set; }
        public string MLASTNAME { get; set; }
    }

}