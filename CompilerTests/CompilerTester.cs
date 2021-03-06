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
    public class CompilerTester
    {
        [TestMethod]
        public void CustomTest ()
        {
            AST_Builder astBuilder = new AST_Builder();
            AstReducer astReducer = new AstReducer();


            Ast astResult = astBuilder.buildAST(" [ a b ] a*a + b*b");
            astResult = astReducer.reduceAst(astResult);
            List<string> assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            int result = CompilerSimulator.simulate(assemblerCode,new int[] {2,3});
            Assert.AreEqual(13,result);

            astResult = astBuilder.buildAST("  [ first second ] (first + second) / 2");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] { 5, 15 });
            Assert.AreEqual(10, result);

            astResult = astBuilder.buildAST("[ x ] x + 2*5");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] {1 });
            Assert.AreEqual(11, result);

            astResult = astBuilder.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] { 4, 0, 0 });
            Assert.AreEqual(3, result);

            astResult = astBuilder.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] { 4, 8, 0 });
            Assert.AreEqual(8, result);


            astResult = astBuilder.buildAST("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] { 4, 8, 16 });
            Assert.AreEqual(2, result);

            astResult = astBuilder.buildAST(" [] 5 + 5");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] { 2, 3 });
            Assert.AreEqual(10, result);


            astResult = astBuilder.buildAST(" [z] z - z");
            astResult = astReducer.reduceAst(astResult);
            assemblerCode = AssemblerCodeGenerator.generateCode(astResult);
            result = CompilerSimulator.simulate(assemblerCode, new int[] { 2});
            Assert.AreEqual(0, result);
        }

    }
}
