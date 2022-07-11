using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /*
     
     function   ::= '[' arg-list ']' expression

    arg-list   ::= /* nothing */
    /*             | variable arg-list

    expression ::= term
                 | expression '+' term
                 | expression '-' term

    term       ::= factor
                 | term '*' factor
                 | term '/' factor

    factor     ::= number
                 | variable
                 | '(' expression ')'
     */

    public struct Token
    { 
        public enum tokenType
        {
            argument,
            variable,
            delimetr,
            number,
            error,
            end
        }
        public string content;
        public tokenType type;
        public Token (string content,tokenType type)
        {
            this.content = content;
            this.type = type;
        }
    }

    public class Lexer
    {
        private int ptrCurrentElemnt;
        private string input;
        public Lexer (string input)
        {
            this.input = input;
        }

        public Token getToken ()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (input.Length - 1 < ptrCurrentElemnt || char.IsWhiteSpace(input[ptrCurrentElemnt]))
            {
                if (input.Length - 1 < ptrCurrentElemnt)
                    return new Token(" ", Token.tokenType.end);
                ptrCurrentElemnt++;
            }
            if (isOperation(input[ptrCurrentElemnt]))
            {
                ptrCurrentElemnt++;
                return new Token(input[ptrCurrentElemnt-1].ToString(), Token.tokenType.delimetr);
            }
            if (char.IsDigit(input[ptrCurrentElemnt]))
            {
                while (input.Length > ptrCurrentElemnt && char.IsDigit(input[ptrCurrentElemnt]))
                {
                    stringBuilder.Append(input[ptrCurrentElemnt]);
                    ptrCurrentElemnt++;
                }
                return new Token(stringBuilder.ToString(), Token.tokenType.number);
            }
            if (char.IsLetter(input[ptrCurrentElemnt]))
            {
                while (input.Length  > ptrCurrentElemnt && char.IsLetter(input[ptrCurrentElemnt]))
                {
                    stringBuilder.Append(input[ptrCurrentElemnt]);
                    ptrCurrentElemnt++;
                }
                return new Token(stringBuilder.ToString(), Token.tokenType.variable);
            }
            return new Token(input[ptrCurrentElemnt].ToString(), Token.tokenType.error);
        }

        private bool isOperation(char symbol)
        {
            switch (symbol)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                case '(':
                    return true;
                case ')':
                    return true;
                case '[':
                    return true;
                case ']':
                    return true;
                default:
                    return false;
            }
        }
    }
}
