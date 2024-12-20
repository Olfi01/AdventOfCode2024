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
        public delegate Task<long> ProcessOpcode(params (long value, int mode)[] parameters);
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

        public Channel<long> InputStream { get; } = Channel.CreateUnbounded<long>();
        public Channel<long> OutputStream { get; } = Channel.CreateUnbounded<long>();

        private long relativeBase = 0;

        private void PopulateOpcodes()
        {
            opcodes.Add(1, new Opcode("add", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                StoreValue(p0 + p1, args[2]);
                return Task.FromResult(-1L);
            }));
            opcodes.Add(2, new Opcode("multiply", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                StoreValue(p0 * p1, args[2]);
                return Task.FromResult(-1L);
            }));
            opcodes.Add(99, new Opcode("quit", 0, args =>
            {
                return Task.FromResult(-2L);
            }));
            opcodes.Add(3, new Opcode("input", 1, async args =>
            {
                var input = await InputStream.Reader.ReadAsync();
                StoreValue(input, args[0]);
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
                else return Task.FromResult(-1L);
            }));
            opcodes.Add(6, new Opcode("jump-if-false", 2, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                if (p0 == 0) return Task.FromResult(p1);
                else return Task.FromResult(-1L);
            }));
            opcodes.Add(7, new Opcode("less-than", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                StoreValue(p0 < p1 ? 1 : 0, args[2]);
                return Task.FromResult(-1L);
            }));
            opcodes.Add(8, new Opcode("equals", 3, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                var p1 = EvaluateParameter(args[1]);
                StoreValue(p0 == p1 ? 1 : 0, args[2]);
                return Task.FromResult(-1L);
            }));
            opcodes.Add(9, new Opcode("adjust-relative-base", 1, args =>
            {
                var p0 = EvaluateParameter(args[0]);
                relativeBase += p0;
                return Task.FromResult(-1L);
            }));
        }

        private long EvaluateParameter((long value, int mode) param)
        {
            return param.mode switch
            {
                0 => memory.GetValueOrDefault(param.value),
                1 => param.value,
                2 => memory.GetValueOrDefault(relativeBase + param.value),
                _ => throw new Exception()
            };
        }

        private void StoreValue(long value, (long value, int mode) param)
        {
            switch (param.mode)
            {
                case 0: 
                    memory[param.value] = value;
                    break;
                case 2: 
                    memory[relativeBase + param.value] = value;
                    break;
                default:
                    throw new Exception();
            };
        }

        private readonly Dictionary<long, long> memory = [];
        public async Task<long> ExecuteProgram(IEnumerable<long> program, long readAt = 0)
        {
            memory.Clear();
            var enumerator = program.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                memory[i] = enumerator.Current;
            }
            for (long i = 0; i < memory.Count; i++)
            {
                var current = memory[i];
                var opcode = (int)(current % 100);
                var op = opcodes[opcode];
                List<(long value, int mode)> parameters = [];
                for (int j = 1; j <= op.ArgumentCount; j++)
                {
                    int mode = (int)((current / (int)Math.Pow(10, 1 + j)) % 10);
                    parameters.Add((memory[i + j], mode));
                }
                i += op.ArgumentCount;
                long result = await op.Execute([.. parameters]);
                if (result == -2) break;
                else if (result >= 0) i = result - 1;   // - 1 to compensate for next round of for loop
            }
            return memory[readAt];
        }
    }
}
