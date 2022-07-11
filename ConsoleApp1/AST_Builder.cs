using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    //The first pass will be the method pass1 which takes a string representing a function in the
    //    original programming language and will return a(JSON) object that represents that
    //    Abstract Syntax Tree.The Abstract Syntax Tree must use the following representations:




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
    public class AST_Builder
    {
        private Ast root;
        private Lexer lexer;
        private Token currentToken;
        private BinOp tmpBinOp;

        public Ast buildAST (string input)
        {
            tmpBinOp = new BinOp();
            root = new BinOp();
            lexer = new Lexer(input);
            currentToken = lexer.getToken();
            if (currentToken.type == Token.tokenType.error || currentToken.type == Token.tokenType.end)
            {
                return null; //error or empety input
            }
            add_sub(ref root);
            root = root.leftNode;
            return root;
        }

        private void add_sub (ref Ast tree)
        {

            mul_div(ref tree);
            char oper =' ';
            while ((oper = currentToken.content[0])  == '+' || oper  == '-')
            {
                Ast tmpTree = new BinOp();
                currentToken = lexer.getToken();
                mul_div(ref tmpTree);
                switch (oper)
                {
                    case '+':
    ///                     result = result + tmp_result;
                        addBinOp(ref tree, tmpTree, "+");
                        break;
                    case '-':
                        addBinOp(ref tree, tmpTree, "-");
                        break;
                }
            }
        }
        private void mul_div (ref Ast tree)
        {

            char oper = ' ';
            unarMinus(ref tree);
            while ( (oper = currentToken.content[0]) == '*' || oper == '/')
            {
                Ast tmpTree = new BinOp();
                currentToken = lexer.getToken();
                mul_div(ref tmpTree);
                switch (oper)
                {
                    case '*':
                        addBinOp(ref tree, tmpTree, "*");
                        break;
                    case '/':
                        addBinOp(ref tree, tmpTree, "/");
                        break;
                }

            }

        }
      

        private void unarMinus (ref Ast tree)
        {
            char oper = '0';
            if (currentToken.type == Token.tokenType.delimetr && (currentToken.content == "+" || currentToken.content == "-"))
            {
                oper = currentToken.content[0];
                currentToken = lexer.getToken();
            }
            paretheses(ref tree);
            if (oper == '-')
            {
                //result = -result;
            }
        }
        private void paretheses(ref Ast tree)
        {
            if (currentToken.content[0] == '(')
            {
                currentToken = lexer.getToken();
                add_sub(ref tree);
                if (currentToken.content[0] != ')')
                    ;//error
                currentToken = lexer.getToken();
            }
            else
               atom(ref tree);
        }
        private void atom (ref Ast tree)
        {
            switch (currentToken.type)
            {
                case Token.tokenType.number:
                    addUnOp(ref tree, currentToken.content);
                    currentToken = lexer.getToken();
                    break;
                case Token.tokenType.variable:
                    addUnOp(ref tree, currentToken.content);
                    currentToken = lexer.getToken();
                    break;
                default:
                    break;
            }
        }

        //maybe do not work
        //not work need tmp ast
        private void setUnarOp (string content)
        {
            if (root.leftNode == null)
                root.leftNode = new UnOp(content);
            else if (root.rightNode == null)
                root.rightNode = new UnOp(content);
        }

        private void addUnOp(ref Ast mainNode,string content)
        {
             if (mainNode.leftNode == null)
                mainNode.leftNode = new UnOp(content);
            else //(mainNode.rightNode == null)
                mainNode.rightNode = new UnOp(content);
            //else
            //{
            //    Ast tmpAst = mainNode;
            //    mainNode = new BinOp();
            //    mainNode.rightNode = tmpAst;
            //    mainNode.leftNode = new UnOp(content);
            //}
        }

        private  void addBinOp(ref Ast root, Ast node, string content)
        {
            

            root.content = content;
            //если правый узел в тмп результе = нулю то это просто число и переменная
            if (node.rightNode == null && root.rightNode == null)
            {
                root.rightNode = node.leftNode;
            }
            else if (node.rightNode == null && node.leftNode == null)
            {
                root.leftNode = node.leftNode;
            }
            else if (root.rightNode == null)
            {
                root.rightNode = node;
            }
            else if (root.leftNode == null)
            {
                root.leftNode = node.rightNode;
            }

                Ast tmpAst = root;
                root = new BinOp();
                //root.rightNode = node;
                root.leftNode = tmpAst;
        }

    }
}
