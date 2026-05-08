using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentScoreManager
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Student
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        public double Chinese { get; set; }
        public double English { get; set; }
        public double Math { get; set; }
    }
}
