using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Compiler;
namespace CompilerTests
{
    [TestClass]
    public class AstBuilderTester
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
            Assert.IsTrue(check (result1,"+,arg,arg"));

            result1 = astBuider.buildAST("a - b");
            Assert.IsTrue(check(result1, "-,arg,arg"));

            result1 = astBuider.buildAST("a * b");
            Assert.IsTrue(check(result1, "*,arg,arg"));

            result1 = astBuider.buildAST("a / b");
            Assert.IsTrue(check(result1, "/,arg,arg"));
        }

        [TestMethod]
        public void mixCase_add_sub_div_mul()
        {
            AST_Builder astBuider = new AST_Builder();

            Ast result1 = astBuider.buildAST("a + b * c + d");
            Assert.IsTrue(check(result1, "+,+,arg,*,arg,arg,arg"));

            result1 = astBuider.buildAST("(2 - a) / d");
            Assert.IsTrue(check(result1, "/,-,imm,arg,arg"));

            result1 = astBuider.buildAST("(1+3) * (a + x)");
            Assert.IsTrue(check(result1, "*,+,imm,imm,+,arg,arg"));
        }

        [TestMethod]
        public void codeWarsTests()
        {
            AST_Builder astBuider = new AST_Builder();

            Ast result1 = astBuider.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            Assert.IsTrue (check (result1, "/,-,+,*,*,imm,imm,arg,*,imm,arg,*,imm,arg,+,+,imm,imm,*,imm,imm"));

            result1 = astBuider.buildAST("(2 - a) / d");
            Assert.IsTrue(check(result1, "/,-,imm,arg,arg"));

            result1 = astBuider.buildAST("(1+3) * (a + x)");
            Assert.IsTrue(check(result1, "*,+,imm,imm,+,arg,arg"));
        }

    }
}
