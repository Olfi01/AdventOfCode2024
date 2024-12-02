using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019
{
    public class IntcodeComputerService
    {
        /// <summary>
        /// Executes an opcode on the present memory
        /// </summary>
        /// <param name="parameters">Parameters for the opcode, if any</param>
        /// <returns>True if the opcode terminates execution</returns>
        public delegate bool ProcessOpcode(params int[] parameters);
        public class Opcode(int argumentCount, ProcessOpcode execute) 
        {
            public int ArgumentCount { get; } = argumentCount;
            public ProcessOpcode Execute { get; } = execute;
        }
        private readonly Dictionary<int, Opcode> opcodes = [];

        public IntcodeComputerService() 
        {
            PopulateOpcodes();
        }

        private void PopulateOpcodes()
        {
            opcodes.Add(1, new Opcode(3, args =>
            {
                memory[args[2]] = memory[args[0]] + memory[args[1]];
                return false;
            }));
            opcodes.Add(2, new Opcode(3, args =>
            {
                memory[args[2]] = memory[args[0]] * memory[args[1]];
                return false;
            }));
            opcodes.Add(99, new Opcode(0, args =>
            {
                return true;
            }));
        }

        private readonly List<int> memory = [];
        public int ExecuteProgram(IEnumerable<int> program, int readAt = 0)
        {
            memory.Clear();
            memory.AddRange(program);
            for (int i = 0; i < memory.Count; i++)
            {
                var op = opcodes[memory[i]];
                int[] parameters = [.. memory[(i + 1)..(i + 1 + op.ArgumentCount)]];
                i += op.ArgumentCount;
                if (op.Execute(parameters)) break;
            }
            return memory[readAt];
        }
    }
}
