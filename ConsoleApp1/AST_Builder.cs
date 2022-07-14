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
        private List<string> list_arguments;
        public Ast buildAST (string input)
        {
            list_arguments = new List<string>(); 
            root = new BinOp();
            lexer = new Lexer(input);
            currentToken = lexer.getToken();
            if (currentToken.type == Token.tokenType.error || currentToken.type == Token.tokenType.end)
            {
                return null; //error or empety input
            }
            getListArguments(ref root);
            BinOp rootNode = (BinOp)root;
            return rootNode.a();
        }
        private void getListArguments(ref Ast tree)
        {
            if (currentToken.content == "[")
            {
                currentToken = lexer.getToken();
                while (currentToken.content != "]")
                {

                    if (currentToken.type == Token.tokenType.variable)
                        list_arguments.Add(currentToken.content);
                    else
                        return;
                    currentToken = lexer.getToken();
                }
                currentToken = lexer.getToken();
            }
            add_sub(ref tree);
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
                unarMinus(ref tmpTree);
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
                    addUnOp(ref tree,"imm" , Convert.ToInt32(currentToken.content));
                    currentToken = lexer.getToken();
                    break;
                case Token.tokenType.variable:
                    addUnOp(ref tree, "arg", getArgumentNumber(currentToken.content));//here number of argument
                    currentToken = lexer.getToken();
                    break;
                default:
                    break;
            }
        }
        private int getArgumentNumber (string argumentName)
        {
            for (int i = 0; i < list_arguments.Count; i++)
            {
                if (list_arguments[i] == argumentName)
                    return i;
            }
            return -1;
        }

        private void addUnOp(ref Ast mainNode, string content, int val)
        {
            mainNode = new BinOp (null, new UnOp(content, val), null);
        }

        private  void addBinOp(ref Ast root, Ast node, string content)
        {
            BinOp _root = (BinOp)root;
            BinOp _node = (BinOp)node;

            // if right node == nul when ir is just number(UnOp) 
            if (_node.b() == null && _root.b() == null)
            {
                root = new BinOp(content, _root.a(), _node.a());
            }
            else if (_node.b() == null && _node.a() == null)
            {
                root = new BinOp(content, _node.a(), _root.b());
            }
            else if (_root.b() == null)
            {
                root = new BinOp(content, _root.a(),_node);
            }
            else if (_root.a() == null)
            {
                root = new BinOp(content, _node.b(),_root.b() );
            }

            Ast tmpAst = root;
            root = new BinOp(content, tmpAst,null);
        }

    }
}
