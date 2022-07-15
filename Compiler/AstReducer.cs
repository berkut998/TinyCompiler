using System;


namespace Compiler
{
    public class AstReducer
    {
        public Ast reduceAst (Ast root)
        {
            reduce(ref root);
            return root;
        }

        private void reduce (ref Ast node)
        {
            if (node.op() == null || node.op() == "arg" || node.op() == "imm")
                return;
            BinOp currBinOp = (BinOp)node;
            Ast left = currBinOp.a();
            Ast right = currBinOp.b();
            reduce(ref left);
            reduce(ref right);
            if (left.op() == "imm" && right.op() == "imm")
            {
                UnOp left_Unar = (UnOp)left;
                UnOp right_Unar = (UnOp)right;
                int? result = calculate(left_Unar.n(), right_Unar.n(), currBinOp.op()[0]);
                node = new UnOp("imm", (int)result);
                return;
            }
                node = new BinOp(currBinOp.op(), left, right);

            return;
        }

        private int? calculate (int? a, int? b, char operation)
        {
            switch (operation)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    return null;
            }
        }

    }
}
