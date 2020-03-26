using System.Collections;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace test
{
#nullable enable
    class Program
    {
       static async Task WriteLine()
        {
            using var stream = new FileStream("name.xml", FileMode.Open, FileAccess.ReadWrite);
            byte[] bytes = Encoding.UTF8.GetBytes("<b>hello.</b>");
            System.Console.WriteLine(stream.Length);
            await stream.WriteAsync(bytes, 0, bytes.Length);
            System.Console.WriteLine(stream.Length);
            await stream.FlushAsync();
            System.Console.WriteLine(stream.Length);
            stream.Write(bytes, 0, bytes.Length);
            System.Console.WriteLine(stream.Length);
            
        }
        static void Main(string[] args)
        {
             WriteLine().Start();

            System.Console.WriteLine(args.Length);
            //             const string _connectionString = @"data source=F:/hands/下载/新建文件夹/YSTileTagProd/YSTileTagProd/bin/Debug/Database/Data.db";
            //             SQLite SQLite = new SQLite(_connectionString);
            //             DataTable? tryGet = default;
            //             SQLite.GetAndOperateSQLite(table =>
            //                              {
            //                                  tryGet = table;
            //                                  System.Console.WriteLine(table.Rows.Count > 0 ? table.Rows[0][0] : "没有数据");
            //                              }, "select * from config");
            //             int result = (int)SQLite.ExecuteCommandThenDisposed(x => x.ExecuteNonQuery(), "delete from config");
            //             System.Console.WriteLine($"选择到了{result}");
            //             System.Console.WriteLine(tryGet?.Rows.Count > 0 ? tryGet.Rows[0][0] : "没有数据");
            // System.Console.WriteLine(DateTime.Now.ToString("DD"));
            #region XMLExplian

            //             Type widgetType = typeof(Widget);

            //             //Gets every HelpAttribute defined for the Widget type
            //             object[] widgetClassAttributes = widgetType.GetCustomAttributes(typeof(HelpAttribute), false);

            //             if (widgetClassAttributes.Length > 0)
            //             {
            //                 HelpAttribute attr = (HelpAttribute)widgetClassAttributes[0];
            //                 Console.WriteLine($"Widget class help URL : {attr.Url} - Related topic : {attr.Topic}");
            //             }

            //             System.Reflection.MethodInfo displayMethod = widgetType.GetMethod(nameof(Widget.Display));

            //             //Gets every HelpAttribute defined for the Widget.Display method
            //             object[] displayMethodAttributes = displayMethod.GetCustomAttributes(typeof(HelpAttribute), false);

            //             if (displayMethodAttributes.Length > 0)
            //             {
            //                 HelpAttribute attr = (HelpAttribute)displayMethodAttributes[0];
            //                 Console.WriteLine($"Display method help URL : {attr.Url} - Related topic : {attr.Topic}");
            //             }

            // object[,] ArrayList = { { "123", 1, false }, { "456", true, 5 }, { 7, "789", "last" } };
            // object[] list = { ",,", 1, false };

            //             var query = from q in list
            //                         where q is string
            //                         select q;
            //             query = from q in ArrayList.Cast<object>()
            //                     where q is int a && a > 2
            //                     select q;
            //             var search = ArrayList.Cast<object>().Where(query => query is int);

            //             (string a, int i, bool b, string w) = ("hello", 2, true, "world");
            //             System.Console.WriteLine($"{a},{b},{i},{w}");
            //             foreach (var item in search)
            //             {
            //                 System.Console.WriteLine(item);
            //             }

            //             Console.WriteLine("Hello World!");
            //             Name name = new Name();
            //             Action Litter = () =>
            //            {
            //                var datatable = new DataTable();
            //                datatable.Columns.Add("one", typeof(string));
            //                datatable.Rows.Add("Hello Friends");
            //                System.Console.WriteLine(datatable.Rows[0][0]);
            //            };
            //             name.Test(Litter);
            //             string param = "notnull：";
            //             string isnull = param ?? "notnull";
            //             System.Console.WriteLine(isnull);
            // #pragma warning disable CS8601 // 可能的 null 引用赋值。
            //             System.Console.WriteLine(name.ReturnNull(ref param) + "one");
            // #pragma warning restore CS8601 // 可能的 null 引用赋值。
            // #nullable restore
            //             param = null;
            // #pragma warning disable CS8601 // 可能的 null 引用赋值。
            //             System.Console.WriteLine(name.ReturnNull(ref param) + "two");
            // #pragma warning restore CS8601 // 可能的 null 引用赋值。
            //             isnull = param ?? "null";
            //             System.Console.WriteLine(isnull);

            //             Console.ReadLine();
            //         }

            #endregion
            // Create a data source.       
            string[] words = { "apples", "blueberries", "oranges", "bananas", "apricots" };
            // Create the query.      
            // var wordGroups1 =
            //  from w in words
            //  group w by w[w.Length - 1] into fruitGroup
            //  where fruitGroup.Count() >= 2
            //  select new
            //  {
            //      FirstLetter = fruitGroup.Key,
            //      WordsCount = fruitGroup.Count(),
            //      stringName = fruitGroup
            //  };
            // // Execute the query. Note that we only iterate over the groups,         // not the items in each group       
            // foreach (var item in wordGroups1)
            // {
            //     Console.WriteLine(" {0} has {1} elements.name is ", item.FirstLetter, item.WordsCount);
            //     foreach (var name in item.stringName)
            //     {
            //         System.Console.WriteLine($"{name} \t");
            //     }
            // }
            // // Keep the console window open in debug mode   
            // Console.WriteLine("Press any key to exit.");

            Name n = new Name("sm");
            System.Console.WriteLine(n.name);
            // var a = Json.JsonParser.FromJson("{\"a\":\"呵呵\"}");
            // System.Console.WriteLine(a["a"]);
        }
        public class Name
        {
            /// <summary>
            /// 
            /// </summary>
            string n;
            public string name => n;
            public Name(string _name) => n = _name;
            public void Test([NotNullWhen(true)] Action test) => test();
            public string ReturnNull([NotNullWhen(false)] ref string param) => param ?? "null：" + "there is disnullable";

        }
        public class HelpAttribute : Attribute
        {
            string url;
            /// <summary>
            /// 只读属性
            /// </summary>
            public string Url => url;
            public string urls { get => url; set => url = value; }
            string? topic;
            public HelpAttribute(string url)
            {
                this.url = url;
            }


            public string? Topic
            {
                get => topic;
                set => topic = value;
            }
        }
        [Help("https://docs.microsoft.com/dotnet/csharp/tour-of-csharp/attributes", Topic = "Hello World")]
        public class Widget
        {
            [Help("https://docs.microsoft.com/dotnet/csharp/tour-of-csharp/attributes",
            Topic = "Display")]
            public void Display(string text) { }
        }

    }
}
