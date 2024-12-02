using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
        public delegate Task<int> ProcessOpcode(params (int value, int mode)[] parameters);
        public class Opcode(string name,  int argumentCount, ProcessOpcode execute) 
        {
            public string Name { get; } = name;
            public int ArgumentCount { get; } = argumentCount;
            public ProcessOpcode Execute { get; } = execute;

            public override string ToString()
            {
                return $"Op: {Name}";
            }
        }
        private readonly Dictionary<int, Opcode> opcodes = [];

        public IntcodeComputerService() 
        {
            PopulateOpcodes();
        }

        public Channel<int> InputStream { get; } = Channel.CreateUnbounded<int>();
        public Channel<int> OutputStream { get; } = Channel.CreateUnbounded<int>();

        private void PopulateOpcodes()
        {
            opcodes.Add(1, new Opcode("add", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                memory[args[2].value] = p0 + p1;
                return Task.FromResult(-1);
            }));
            opcodes.Add(2, new Opcode("multiply", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                memory[args[2].value] = p0 * p1;
                return Task.FromResult(-1);
            }));
            opcodes.Add(99, new Opcode("quit", 0, args =>
            {
                return Task.FromResult(-2);
            }));
            opcodes.Add(3, new Opcode("input", 1, async args =>
            {
                var input = await InputStream.Reader.ReadAsync();
                memory[args[0].value] = input;
                return -1;
            }));
            opcodes.Add(4, new Opcode("output", 1, async args =>
            {
                var p0 = EvaluateParameter(args[0]);
                await OutputStream.Writer.WriteAsync(p0);
                return -1;
            }));
            opcodes.Add(5, new Opcode("jump-if-true", 2, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                if (p0 != 0) return Task.FromResult(p1);
                else return Task.FromResult(-1);
            }));
            opcodes.Add(6, new Opcode("jump-if-false", 2, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                if (p0 == 0) return Task.FromResult(p1);
                else return Task.FromResult(-1);
            }));
            opcodes.Add(7, new Opcode("less-than", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                memory[args[2].value] = p0 < p1 ? 1 : 0;
                return Task.FromResult(-1);
            }));
            opcodes.Add(8, new Opcode("equals", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                memory[args[2].value] = p0 == p1 ? 1 : 0;
                return Task.FromResult(-1);
            }));
        }

        private int EvaluateParameter((int value, int mode) param)
        {
            return param.mode switch
            {
                0 => memory[param.value],
                1 => param.value,
                _ => throw new Exception()
            };
        }

        private readonly List<int> memory = [];
        public async Task<int> ExecuteProgram(IEnumerable<int> program, int readAt = 0)
        {
            memory.Clear();
            memory.AddRange(program);
            for (int i = 0; i < memory.Count; i++)
            {
                var current = memory[i];
                var opcode = current % 100;
                var op = opcodes[opcode];
                List<(int value, int mode)> parameters = [];
                for (int j = 1; j <= op.ArgumentCount; j++)
                {
                    int mode = (current / (int)Math.Pow(10, 1 + j)) % 10;
                    parameters.Add((memory[i + j], mode));
                }
                i += op.ArgumentCount;
                int result = await op.Execute([.. parameters]);
                if (result == -2) break;
                else if (result >= 0) i = result - 1;   // - 1 to compensate for next round of for loop
            }
            return memory[readAt];
        }
    }
}
