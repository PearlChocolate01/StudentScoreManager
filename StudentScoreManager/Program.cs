using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentScoreManager
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        DeleteStudent();
                        break;
                    default:
                        Console.WriteLine("输入无效，请重新选择");
                        break;
                }

                Console.WriteLine("\n按任意键继续……");
                Console.ReadKey(true);//读一个键，用于停顿，防止循环后瞬间清屏看不见本次操作结果；true表示隐藏当前输入的键值
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("========== 学生成绩管理系统 ==========");
            Console.WriteLine("1. 添加学生");
            Console.WriteLine("2. 删除学生");
            Console.WriteLine("======================================");
            Console.WriteLine("请选择操作：");
        }

        static void AddStudent() 
        {
            Console.WriteLine("\n--- 添加学生 ---");
            Console.WriteLine("学号：");
            string id = Console.ReadLine();

            // 检查学号是否已存在
            if (students.Exists(s => s.Id == id))
            {
                Console.WriteLine("学号已存在！");
                return;
            }

            Console.WriteLine("姓名：");
            string name = Console.ReadLine();

            Console.WriteLine("语文成绩：");
            if(!double.TryParse(Console.ReadLine(), out double chinese))
            {
                Console.WriteLine("语文成绩格式错误！");
                return;
            }
            if(chinese < 0 || chinese > 100)
            {
                Console.WriteLine("语文成绩必须在 0-100 之间！");
                return;
            }

            Console.WriteLine("数学成绩：");
            if (!double.TryParse(Console.ReadLine(), out double math))
            {
                Console.WriteLine("数学成绩格式错误！");
                return;
            }
            if (math < 0 || math > 100)
            {
                Console.WriteLine("数学成绩必须在 0-100 之间！");
                return;
            }

            Console.WriteLine("英语成绩：");
            if (!double.TryParse(Console.ReadLine(), out double english))
            {
                Console.WriteLine("英语成绩格式错误！");
                return;
            }
            if (english < 0 || english > 100)
            {
                Console.WriteLine("英语成绩必须在 0-100 之间！");
                return;
            }

            Student std = new Student()
            {
                Id = id,
                Name = name,
                Chinese = chinese,
                Math = math,
                English = english
            };
            students.Add(std);
            Console.WriteLine($"成功添加学生【{id} {name}】！");
        }

        static void DeleteStudent()
        {
            Console.WriteLine("\n--- 删除学生 ---");
            Console.WriteLine("请输入要删除的学号：");
            string id = Console.ReadLine();

            Student std = students.Find(s => s.Id == id);
            if(std == null)
            {
                Console.WriteLine("未找到该学生");
                return;
            }

            Console.WriteLine($"确认删除【{id} {std.Name}】？(y/n)");
            if(Console.ReadLine() == "y")
            {
                students.Remove(std);
                Console.WriteLine("删除成功！");
            }
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
        /// <summary>
        /// 语文成绩
        /// </summary>
        public double Chinese { get; set; }
        /// <summary>
        /// 英语成绩
        /// </summary>
        public double English { get; set; }
        /// <summary>
        /// 数学成绩
        /// </summary>
        public double Math { get; set; }
        /// <summary>
        /// 总分（只读属性，自动计算）
        /// </summary>
        public double Total => Chinese + English + Math;
        /// <summary>
        /// 平均分（只读属性，自动计算）
        /// </summary>
        public double Average => Total / 3;
    }
}
