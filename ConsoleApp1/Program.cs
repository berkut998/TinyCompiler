using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        //TODO tree build right associative (can be error) maybe better if will be left associative
        //a*b+c*d -> +,*,c,d,*,a,b,   should be a*b+c*d -> +,*,a,b,*,c,d,
        static void Main(string[] args)
        {
            Token currentToken;
            Lexer lexer = new Lexer("[ a b ] a*b + c*d");
            do
            {

                currentToken = lexer.getToken();
            } while (currentToken.type != Token.tokenType.end && currentToken.type != Token.tokenType.error);
            Ast root = new BinOp();
            BinOp binOp1 = new BinOp("*");
            UnOp unOp1 = new UnOp("a");
            UnOp unOp2 = new UnOp("b");
            BinOp binOp2 = new BinOp("/");
            UnOp unOp3 = new UnOp("c");
            UnOp unOp4 = new UnOp("d");
            BinOp binOp3 = new BinOp("+");

            //addToAST(ref root, binOp1);
            //addToAST(ref root, binOp2);
            //addToAST(ref root, binOp3);
            AST_Builder astBuilder = new AST_Builder();
            Ast res1 = astBuilder.buildAST("( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            string res1Str = "";
            res1.preOrder_NLR(res1, ref res1Str);
            // ("/",  ("-",  ("+",  ("*",  ("*",  ("imm", 2),  ("imm", 3)),  ("arg", 0)),  ("*",  ("imm", 5),  ("arg", 1))),  ("*",  ("imm", 3),  ("arg", 2))),  ("+",  ("+",  ("imm", 1),  ("imm", 3)),  ("*",  ("imm", 2),  ("imm", 2))));
            astBuilder.buildAST("a+b+c+d");
            //test1 = test1.add("+", test1);
            //test1 = test1.add("-", test1);
            //test1 = test1.add("+", test1);
            //test1   =test1.add("7", test1);
            //test1= test1.add("8", test1);
            //test1 =test1.add("10", test1);
            //test1 =test1.add("11", test1);
            //test1.leftNode = new BinOp();

            //"+,+,a,*,b,c,d,"
            // --> +
            // +       a
            //b *      a  b
        }

        private static void addToAST(ref Ast root, Ast node)
        {
            if (root.rightNode != null && root.leftNode != null)
            {
                Ast tmpAst = root;
                root = new BinOp();
                root.rightNode = node;
                root.leftNode = tmpAst;
            }
            else if (root.rightNode == null)
            {
                root.rightNode = node;

            }
            else if (root.leftNode == null)
            {

                root.leftNode = node;
            }
        }


    }
}
