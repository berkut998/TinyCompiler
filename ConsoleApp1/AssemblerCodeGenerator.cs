using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AssemblerCodeGenerator
    {

        private int stackCount;
        List<string> result  = new List<string>();

        public AssemblerCodeGenerator ()
        {
            result = new List<string>();
        }
        /*
       
         new BinOp("+", new UnOp("arg", 0), new UnOp("imm", 10))  ----- >    [ "IM 10", "SW", "AR 0", "AD" ] 

         You are working on a small processor 
        with two registers (R0 and R1),
        a stack, 
        and an array of input arguments.
        The result of a function is expected to be in R0. 
        The processor supports the following instructions:

           "IM n"     // load the constant value n into R0
            "AR n"     // load the n-th input argument into R0
            "SW"       // swap R0 and R1
            "PU"       // push R0 onto the stack
            "PO"       // pop the top value off of the stack into R0
            "AD"       // add R1 to R0 and put the result in R0
            "SU"       // subtract R1 from R0 and put the result in R0
            "MU"       // multiply R0 by R1 and put the result in R0
            "DI"       // divide R0 by R1 and put the result in R0
         
         */
        public List<string> GenerateCode(Ast node)
        {

            /*
			// ADD TO r0
			// SWAP 
			// ADD NEXT TO R0
			// CALCULATE
			// ADD TO STACK
			// check left check right
            */

            if (node.op() == null || node.op() == "arg" || node.op() == "imm")
                return null;// return;
            BinOp _currentNode = (BinOp)node;
            Ast left = (Ast)_currentNode.a();
            Ast right = (Ast)_currentNode.b();

            GenerateCode(left);
            GenerateCode(right);
            if (right.op() == "arg")
            {
                UnOp right_UnOp = (UnOp)right;
                result.Add("AR" + right_UnOp.n());
            }
            else if (right.op() == "imm")
            {
                UnOp right_UnOp = (UnOp)right;
                result.Add("IM" + right_UnOp.n());
            }
            else if (stackCount > 0)
                result.Add("PO");
            result.Add("SW");
            if (left.op() == "arg")
            {
                UnOp left_UnOp = (UnOp)left;
                result.Add("AR" + left_UnOp.n());
            }
            else if (left.op() == "imm")
            {
                UnOp left_UnOp = (UnOp)left;
                result.Add("IM" + left_UnOp.n());
            }
            else if (stackCount > 0)
                result.Add("PO");

          
            GenerateCODE(_currentNode.op()[0], ref result);
            return result;
        }
        private void GenerateCODE(char operation,ref List<string>result)
        {
            switch (operation)
            {
                case '+':
                    result.Add("AD");
                    break;
                case '-':
                    result.Add("SU");
                    break;
                case '*':
                    result.Add("MU");
                    break;
                case '/':
                    result.Add("DI");
                    break;
                default:
                    break;
            }
            result.Add("PU");
            stackCount++;
        }
    }
}

