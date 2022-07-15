
using System.Collections.Generic;

namespace Compiler
{
    /*
    next methods is useless and need only for debugging:
    preOrder_NLR
    preOrder_NRL
    preOrder_RNL
    preOrder_LNR
    levelOrder
     */
    public abstract class Ast
    {
        protected Ast leftNode;
        protected Ast rightNode;
        protected string content;

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
        public void preOrder_NRL(Ast root, ref string result)
        {
            if (root == null)
                return;
            result += root.content + ",";
            preOrder_NRL(root.rightNode, ref result);
            preOrder_NRL(root.leftNode, ref result);

        }
        public void preOrder_RNL(Ast root, ref string result)
        {
            if (root == null)
                return;

            preOrder_RNL(root.rightNode, ref result);
            result += root.content + ",";
            preOrder_RNL(root.leftNode, ref result);

        }
        public void preOrder_LNR(Ast root, ref string result)
        {
            if (root == null)
                return;

            preOrder_LNR(root.leftNode, ref result);
            result += root.content + ",";
            preOrder_LNR(root.rightNode, ref result);

        }


        public void levelOrder (Ast root,ref string result)
        {
            Queue<Ast> queue = new Queue<Ast>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                Ast currNode = queue.Dequeue();
                result +=  currNode.content + ",";
                if (currNode.rightNode != null)
                    queue.Enqueue(currNode.rightNode);
                if (currNode.leftNode != null)
                    queue.Enqueue(currNode.leftNode);

            }
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
        private int val_n;
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
}
