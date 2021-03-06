﻿using System;
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


            A a;
            B b;

            a = new A();
            b = new B();
            a.Foo();  // output --> "A::Foo()"
            b.Foo();  // output --> "B::Foo()"

            a = new B();
            a.Foo();  // output --> "A::Foo()"


            try
            {
                using (var ctx = new XCodeDbContext())
                {
                    var dbexist = ctx.Database.Exists();

                    if (dbexist)
                    {
                        //生成mysql数据库
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



    class A
    {
        public void Foo() { Console.WriteLine("A::Foo()"); }
    }

    class B : A
    {
        public new void Foo() { Console.WriteLine("B::Foo()"); }
    }

    

}
