
using System.Collections.Generic;


namespace Compiler
{
     /*
       example:
         new BinOp("+", new UnOp("arg", 0), new UnOp("imm", 10))  ----- >    [ "IM 10", "SW", "AR 0", "AD" ] 
        working on a small processor 
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
    public class AssemblerCodeGenerator
    {

        private static int stackCount;

        public static List<string> generateCode(Ast root)
        {
            stackCount = 0;
            List<string> result  = new List<string>();
            generateAssemblerCode(root,ref result);
            if (result.Count == 0)
            {
                UnOp _root = (UnOp)root;
                result.Add("IM" + _root.n()); 
            }
            return result;
        }
       
        private static void generateAssemblerCode (Ast node, ref List<string> result)
        {
            if (node.op() == null || node.op() == "arg" || node.op() == "imm")
                return ;
            BinOp _currentNode = (BinOp)node;
            Ast left = (Ast)_currentNode.a();
            Ast right = (Ast)_currentNode.b();
            generateAssemblerCode(left,ref result);
            generateAssemblerCode(right, ref result);
            addDataToRegister(ref right, ref result);
            result.Add("SW");
            addDataToRegister(ref left, ref result);
            createCodeForOperation(_currentNode.op()[0], ref result);
            
        }
        private static void addDataToRegister(ref Ast node, ref List<string> result)
        {
            if (node.op() == "arg")
            {
                UnOp node_UnOp = (UnOp)node;
                result.Add("AR" + node_UnOp.n());
            }
            else if (node.op() == "imm")
            {
                UnOp node_UnOp = (UnOp)node;
                result.Add("IM" + node_UnOp.n());
            }
            else if (stackCount > 0)
                result.Add("PO");
        }
        private static void createCodeForOperation(char operation,ref List<string>result)
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

