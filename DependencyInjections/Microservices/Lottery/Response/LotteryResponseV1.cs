using System;
using System.Collections.Generic;
namespace APITemplate.DependencyInjecyions.Microservices.Lottery
{
    public class LotteryResponseV1 
    {
        public string code { get; set; }
        public string drawdate { get; set; }
        public List<Result> result { get; set; }
    }
    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
        public int reword { get; set; }
        public int amount { get; set; }
        public object number { get; set; }
    }
}