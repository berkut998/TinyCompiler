using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Compiler;
namespace CompilerTests
{
    [TestClass]
    public class AstReducerTester
    {

        private bool check(Ast tree, string Expecteed)
        {
            string actual = "";
            tree.preOrder_NLR(tree, ref actual);
            actual = actual.Remove(actual.Length - 1, 1);
            if (actual == Expecteed)
                return true;
            return false;
        }

        [TestMethod]
        public void testReduceAst ()
        {
            AST_Builder astBuilder = new AST_Builder();
            AstReducer astReducer = new AstReducer();


            Ast astResult = astBuilder.buildAST(" [ a b ] a*a + b*b");
            astResult = astReducer.reduceAst(astResult);
            Assert.IsTrue(check(astResult, "+,*,arg,arg,*,arg,arg"));


            astResult = astBuilder.buildAST("  [ first second ] (first + second) / 2");
            astResult = astReducer.reduceAst(astResult);
            Assert.IsTrue(check(astResult, "/,+,arg,arg,imm"));


            astResult = astBuilder.buildAST("[ x ] x + 2*5");
            astResult = astReducer.reduceAst(astResult);
            Assert.IsTrue(check(astResult, "+,arg,imm"));

            astResult = astBuilder.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            astResult = astReducer.reduceAst(astResult);
            Assert.IsTrue(check(astResult, "/,-,+,*,imm,arg,*,imm,arg,*,imm,arg,imm"));


            astResult = astBuilder.buildAST(" [] 5 + 5");
            astResult = astReducer.reduceAst(astResult);
            Assert.IsTrue(check(astResult, "imm"));


            astResult = astBuilder.buildAST(" [z] z - z");
            astResult = astReducer.reduceAst(astResult);
            Assert.IsTrue(check(astResult, "-,arg,arg"));
        }
    }
}
