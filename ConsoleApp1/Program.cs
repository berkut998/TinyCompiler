using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
       
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
            Ast ast = binOp1.a();
            ast = unOp1;
            //addToAST(ref root, binOp1);
            //addToAST(ref root, binOp2);
            //addToAST(ref root, binOp3);
            AST_Builder astBuilder = new AST_Builder();
            Ast res1 = astBuilder.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            BinOp tmpRigthNodeRoot11 = (BinOp)res1;
            BinOp tmpRigthNodeRoot = (BinOp)res1.GetType().GetMethod("a").Invoke(res1, null);
            string StrNLR_mainAst = "";
            res1.preOrder_NLR(res1, ref StrNLR_mainAst);
            AstReducer reducer = new AstReducer();
            res1 = reducer.reduceAst(res1);

            //tmpRigthNodeRoot = node.GetType().GetMethod("a").Invoke(node, null);
            string StrNLR = "";
            string StrNRL = "";
            string StrRNL = "";
            string StrLNR = "";
            string StrLevel = "";
            res1.preOrder_NLR(res1, ref StrNLR);
            res1.preOrder_NRL(res1, ref StrNRL);
            res1.preOrder_RNL(res1, ref StrRNL);
            res1.preOrder_LNR(res1, ref StrLNR);
            res1.levelOrder(res1, ref StrLevel);
            // ("/",  ("-",  ("+",  ("*",  ("*",  ("", 2),  ("imm", 3)),  ("arg", 0)),  ("*",  ("imm", 5),  ("arg", 1))),  ("*",  ("imm", 3),  ("arg", 2))),  ("+",  ("+",  ("imm", 1),  ("imm", 3)),  ("*",  ("imm", 2),  ("imm", 2))));
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

        //private static void addToAST(ref Ast root, Ast node)
        //{
        //    if (root.rightNode != null && root.leftNode != null)
        //    {
        //        Ast tmpAst = root;
        //        root = new BinOp();
        //        root.rightNode = node;
        //        root.leftNode = tmpAst;
        //    }
        //    else if (root.rightNode == null)
        //    {
        //        root.rightNode = node;

        //    }
        //    else if (root.leftNode == null)
        //    {

        //        root.leftNode = node;
        //    }
        //}


    }
}
