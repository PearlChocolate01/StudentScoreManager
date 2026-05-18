using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentScoreManager
{
    class Program
    {
        static string DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "students_data.txt");
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
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        SearchStudent();
                        break;
                    case "5":
                        ShowAllStudents();
                        break;
                    case "6":
                        SaveToFile();
                        break;
                    case "7":
                        LoadFromFileByCovering();
                        break;
                    case "8":
                        LoadFromFileByAdding();
                        break;
                    case "0":
                        Console.WriteLine("再见！");
                        return;
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
            Console.WriteLine("3. 修改学生信息");
            Console.WriteLine("4. 查询学生");
            Console.WriteLine("5. 显示所有学生（按总分排名）");
            Console.WriteLine("6. 保存数据到文件");
            Console.WriteLine("7. 从文件加载数据（覆盖）");
            Console.WriteLine("8. 从文件加载数据（新增）");
            Console.WriteLine("0. 退出");
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
                Mathe = math,
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
    
        static void UpdateStudent()
        {
            Console.WriteLine("\n--- 修改学生信息 ---");
            Console.WriteLine("请输入要修改的学号：");
            string id = Console.ReadLine();

            Student std = students.Find(s => s.Id == id);
            if(std == null)
            {
                Console.WriteLine("未找到该学生");
                return;
            }

            Console.WriteLine($"当前信息：{std.Name} | 语文：{std.Chinese} | 数学：{std.Mathe} | 英语：{std.English}");
            Console.WriteLine("留空表示不修改");

            Console.WriteLine("新姓名：");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) std.Name = newName;

            Console.WriteLine("新语文成绩：");
            string newChinese = Console.ReadLine();
            if(!string.IsNullOrEmpty(newChinese))
            {
                if (!double.TryParse(newChinese, out double new_chinese))
                {
                    Console.WriteLine("格式错误！");
                    return;
                }
                if (new_chinese < 0 || new_chinese > 100)
                {
                    Console.WriteLine("成绩必须在0-100之间！");
                    return;
                }
                std.Chinese = new_chinese;
            }

            Console.WriteLine("新数学成绩：");
            string newMath = Console.ReadLine();
            if (!string.IsNullOrEmpty(newMath))
            {
                if (!double.TryParse(newMath, out double new_math))
                {
                    Console.WriteLine("格式错误！");
                    return;
                }
                if (new_math < 0 || new_math > 100)
                {
                    Console.WriteLine("成绩必须在0-100之间！");
                    return;
                }
                std.Mathe = new_math;
            }

            Console.WriteLine("新英语成绩：");
            string newEnglish = Console.ReadLine();
            if (!string.IsNullOrEmpty(newEnglish))
            {
                if (!double.TryParse(newEnglish, out double new_english))
                {
                    Console.WriteLine("格式错误！");
                    return;
                }
                if (new_english < 0 || new_english > 100)
                {
                    Console.WriteLine("成绩必须在0-100之间！");
                    return;
                }
                std.English = new_english;
            }

            Console.WriteLine("修改成功！");
        }
    
        static void SearchStudent()
        {
            Console.WriteLine("\n--- 查询学生 ---");
            Console.WriteLine("1. 按学号查询");
            Console.WriteLine("2. 按姓名查询（支持模糊）");
            Console.WriteLine("请选择：");

            string choice = Console.ReadLine();
            if(choice == "1")
            {
                Console.WriteLine("请输入学号：");
                string id = Console.ReadLine();
                Student std = students.Find(s => s.Id == id);
                if(std == null)
                {
                    Console.WriteLine("未找到该学生");
                    return;
                }
                Console.WriteLine($"学号：{std.Id} | 姓名：{std.Name} | 语文：{std.Chinese} | 数学：{std.Mathe} | 英语：{std.English}");
            }
            else if(choice == "2")
            {
                Console.WriteLine("请输入姓名：");
                string name = Console.ReadLine();
                List<Student> stds = students.FindAll(s => s.Name.Contains(name));
                if(stds.Count == 0)
                {
                    Console.WriteLine("未找到学生");
                    return;
                }
                foreach(var std in stds)
                {
                    Console.WriteLine($"学号：{std.Id} | 姓名：{std.Name} | 语文：{std.Chinese} | 数学：{std.Mathe} | 英语：{std.English}");
                }
            }
            else
            {
                Console.WriteLine("无效选择");
            }
        }
    
        static void ShowAllStudents()
        {
            if(students.Count == 0)
            {
                Console.WriteLine("暂无学生数据");
                return;
            }

            var sortedStudents = students.OrderByDescending(s => s.Total).ToList();
            Console.WriteLine("\n--- 学生成绩列表 ---");
            Console.WriteLine("排名\t学号\t姓名\t语文\t数学\t英语\t总分\t均分");
            Console.WriteLine("------------------------------------------------------------");

            for(int i = 0; i < sortedStudents.Count; i++)
            {
                var s = sortedStudents[i];
                Console.WriteLine($"{i + 1}\t{s.Id}\t{s.Name}\t{s.Chinese}\t{s.Mathe}\t{s.English}\t{s.Total}\t{s.Average}");
            }
        }

        static void SaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(
                    System.Text.Unicode.UnicodeRanges.BasicLatin,      // 英文、数字
                    System.Text.Unicode.UnicodeRanges.CjkUnifiedIdeographs  // 常用中文
                )
            };
            string json = JsonSerializer.Serialize(students, options);
            File.WriteAllText(DataPath, json);
            Console.WriteLine("数据已保存到“我的文档/students_data.txt”");
        }

        static void LoadFromFileByCovering()
        {
            if (!File.Exists(DataPath))
            {
                Console.WriteLine("文件不存在");
                return;
            }

            string json = File.ReadAllText(DataPath);
            var stdList = JsonSerializer.Deserialize<List<Student>>(json);
            if (stdList != null || stdList.Count > 0) 
            {
                students = stdList;
                Console.WriteLine($"成功加载{students.Count}条数据");
            }
            else
            {
                Console.WriteLine("数据不存在");
            }
        }

        static void LoadFromFileByAdding()
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
        public double Mathe { get; set; }
        /// <summary>
        /// 总分（只读属性，自动计算）
        /// </summary>
        public double Total => Chinese + English + Mathe;
        /// <summary>
        /// 平均分（只读属性，自动计算）
        /// </summary>
        public double Average => Math.Round(Total / 3, 2);
    }
}
