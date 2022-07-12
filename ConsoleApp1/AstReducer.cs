using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AstReducer
    {
        public Ast reduceAst (Ast root)
        {
            return null;
        }

        //TODO need good Name
        private int? calculate_1 (Ast node, ref int? a, ref int? b)
        {
            int? local_a = null;
            int? local_b = null;
            if (node.content == null)
                return null;
            calculate_1(node.leftNode,ref local_a,ref local_b);
            calculate_1(node.leftNode,ref local_a,ref local_b);

            //set vvalue a,b
            if (node.content == "imm" && local_a == null)
            {
                Type asd = node.GetType();
            }
            // if value was setted calculate
            calculate(local_a, local_b,'+');
            return null;
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
