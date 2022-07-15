using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    /*
    Tiny Three-Pass Compiler
    https://www.codewars.com/kata/5265b0885fda8eac5900093b
    To build ast used top down recursive descent parser
    do not support error processing, all input should be valid
    */
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("input an expression");
            string input = Console.ReadLine();
            Console.WriteLine("input argument value");
            string str_arguments = Console.ReadLine();
            string [] str_argumentsArray = str_arguments.Split(' ');
            int[] arguments = new int[str_arguments.Length];
            for (int i = 0; i < str_argumentsArray.Length - 1; i++)
            {
                if (char.IsDigit(str_argumentsArray[i][0]))
                arguments[i] = Convert.ToInt32(str_argumentsArray[i]);
            }

            AST_Builder astBuilder = new AST_Builder();
            Ast res1 = astBuilder.buildAST(input); //"[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)"
            string StrNLR_mainAst = "";
            res1.preOrder_NLR(res1, ref StrNLR_mainAst);
            AstReducer reducer = new AstReducer();
            res1 = reducer.reduceAst(res1);


            //need to see ast
            //string StrNLR = "";
            //string StrNRL = "";
            //string StrRNL = "";
            //string StrLNR = "";
            //string StrLevel = "";
            //res1.preOrder_NLR(res1, ref StrNLR);
            //res1.preOrder_NRL(res1, ref StrNRL);
            //res1.preOrder_RNL(res1, ref StrRNL);
            //res1.preOrder_LNR(res1, ref StrLNR);
            //res1.levelOrder(res1, ref StrLevel);

            AssemblerCodeGenerator assemblerCodeGenerator = new AssemblerCodeGenerator();
            List<string> resultAssembler = AssemblerCodeGenerator.generateCode(res1);
            string output = "";
            Console.WriteLine();
            Console.WriteLine("Assembler code:");
            foreach (string str in resultAssembler)
            {
                output += str;
                Console.WriteLine(str);
            }
            Console.WriteLine();
           int result = CompilerSimulator.simulate(resultAssembler, arguments);
           Console.WriteLine("result:" + result);
           Console.ReadLine();
        }
    }
}
