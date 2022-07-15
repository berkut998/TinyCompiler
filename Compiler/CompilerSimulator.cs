using System;
using System.Collections.Generic;

namespace Compiler
{
    public  class CompilerSimulator
    {
        public static int simulate(List<string> asm, int[] argv)
        {
            int r0 = 0;
            int r1 = 0;
            List<int> stack = new List<int>();
            foreach (string ins in asm)
            {
                string code = ins.Substring(0, 2);
                switch (code)
                {
                    case "IM": r0 = Int32.Parse(ins.Substring(2).Trim()); break;
                    case "AR": r0 = argv[Int32.Parse(ins.Substring(2).Trim())]; break;
                    case "SW": int tmp = r0; r0 = r1; r1 = tmp; break;
                    case "PU": stack.Add(r0); break;
                    case "PO": r0 = stack[stack.Count - 1]; stack.RemoveAt(stack.Count - 1); break;
                    case "AD": r0 += r1; break;
                    case "SU": r0 -= r1; break;
                    case "MU": r0 *= r1; break;
                    case "DI": r0 /= r1; break;
                }
            }
            return r0;
        }
    }
}
