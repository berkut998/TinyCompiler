using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConsoleApp1;
namespace CompilerTests
{
    [TestClass]
    public class AstBuilderTest
    {

        private bool check(Ast tree, string Expecteed)
        {
            string actual = "";
            tree.preOrder_NLR(tree, ref actual);
            actual = actual.Remove(actual.Length - 1,1);
            if (actual == Expecteed)
                return true;
            return false;
        }

        [TestMethod]
        public void SimpleCase()
        {
            AST_Builder astBuider = new AST_Builder();

            Ast result1 = astBuider.buildAST("a + b");
            Assert.IsTrue(check (result1,"+,a,b"));

            result1 = astBuider.buildAST("a - b");
            Assert.IsTrue(check(result1, "-,a,b"));

            result1 = astBuider.buildAST("a * b");
            Assert.IsTrue(check(result1, "*,a,b"));

            result1 = astBuider.buildAST("a / b");
            Assert.IsTrue(check(result1, "/,a,b"));
        }

        [TestMethod]
        public void mixCase_add_sub_div_mul()
        {
            AST_Builder astBuider = new AST_Builder();

            Ast result1 = astBuider.buildAST("a + b * c + d");
            //Assert.IsTrue(check(result1, "+,d,+,a,*,b,c"));

            result1 = astBuider.buildAST("(2 - a) / d");
            Assert.IsTrue(check(result1, "/,d,-,2,a"));

            result1 = astBuider.buildAST("(1+3) * (a + x)");
            Assert.IsTrue(check(result1, "*,+,1,3,+,a,x"));
        }

        [TestMethod]
        public void codeWarsTests()
        {
            AST_Builder astBuider = new AST_Builder();

            Ast result1 = astBuider.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            Ast t1 = new BinOp("/", new BinOp("-", new BinOp("+", new BinOp("*", new BinOp("*", new UnOp("imm", 2), new UnOp("imm", 3)), new UnOp("arg", 0)), new BinOp("*", new UnOp("imm", 5), new UnOp("arg", 1))), new BinOp("*", new UnOp("imm", 3), new UnOp("arg", 2))), new BinOp("+", new BinOp("+", new UnOp("imm", 1), new UnOp("imm", 3)), new BinOp("*", new UnOp("imm", 2), new UnOp("imm", 2))));
            Assert.AreEqual(result1, t1);

            result1 = astBuider.buildAST("(2 - a) / d");
            Assert.IsTrue(check(result1, "/,d,-,2,a"));

            result1 = astBuider.buildAST("(1+3) * (a + x)");
            Assert.IsTrue(check(result1, "*,+,1,3,+,a,x"));
        }
        // string prog = 
        // Console.WriteLine("Testing: "+prog);
        // Ast p1 = compiler.pass1(prog);
    }
}
