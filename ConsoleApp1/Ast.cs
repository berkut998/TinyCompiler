using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{


       /*
       // Each node is of type 'Ast' and has the following methods:
       // Ast has method 'op()' returning 'String'
       // BinOp has methods 'a()' and 'b()', both return 'Ast'
       // UnOp has method 'n()' returning 'int'
       new BinOp('+', a, b)       // add subtree a to subtree b
       new BinOp('-', a, b)       // subtract subtree b from subtree a
       new BinOp('*', a, b)       // multiply subtree a by subtree b
       new BinOp('/', a, b)       // divide subtree a from subtree b
       new UnOp('arg', n)         // reference to n-th argument, n integer
       new UnOp('imm', n)         // immediate value n, n integer
      */
    public abstract class Ast
    {
        public Ast leftNode;
        public Ast rightNode;
        public string content;

        public Ast()
        { }
        public Ast (string content)
        {
            this.content = content;
        }
        public string op ()
        {
            return content;
        }
        public void preOrder_NLR(Ast root, ref string result)
        {
            if (root == null)
                return;
            result += root.content + ",";
            preOrder_NLR(root.leftNode, ref result);
            preOrder_NLR(root.rightNode, ref result);
        }
    }

    public class BinOp : Ast
    {
        public BinOp()
        {
        }
        public BinOp(string content)
        {
            this.content = content;
        }
        public BinOp(string content,Ast leftNode, Ast rightNode)
        {
            this.content = content;
            this.leftNode = leftNode;
            this.rightNode = rightNode;
        }
        public Ast a()
        {
            return leftNode;
        }
        public Ast b()
        {
            return rightNode;
        }
    }
    public class UnOp:Ast
    {
        public int val_n;
        public UnOp(string content)
        {
            this.content = content;
        }
        public UnOp (string content,int val_n)
        {
            this.content = content;
            this.val_n = val_n;
        }
        public int n ()
        {
            return val_n;
        }
    }
    //

}
