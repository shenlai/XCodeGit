using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain;
using XCode.Repositories.EF;

namespace XCode.ConsoleApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("111");

            try
            {
                using (var ctx = new XCodeDbContext())
                {
                    var dbexist = ctx.Database.Exists();

                    if (dbexist)
                    {
                        //创建成功
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            Console.Read();

        }
    }
}
