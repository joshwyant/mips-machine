using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MIPS.Architecture
{
    public class CPU
    {
        /// <summary>
        /// Gets the worker thread the CPU is running on.
        /// </summary>
        public Thread WorkerThread { get; protected set; }

        /// <summary>
        /// Gets the Register File
        /// </summary>
        public unsafe uint* RF;

        /// <summary>
        /// The Program Counter; gets or sets the address of the current instruction.
        /// </summary>
        public unsafe uint* PC;

        /// <summary>
        /// Gets or sets the current instruction
        /// </summary>
        public Instruction IR;

        /// <summary>
        /// Gets or sets the high word of an arithmetic operation result.
        /// </summary>
        public uint High;

        /// <summary>
        /// Gets or sets the low word of an arithmetic operation result.
        /// </summary>
        public uint Low;

        /// <summary>
        /// Gets or sets the machine this CPU is a part of.
        /// </summary>
        public Machine Machine;

        /// <summary>
        /// Occurs when a breakpoint is hit.
        /// </summary>
        public event EventHandler BreakpointHit;

        /// <summary>
        /// Gets or sets whether the CPU is executing in single-step mode.
        /// </summary>
        public bool SingleStep { get; set; }

        /// <summary>
        /// Occurs when the CPU makes a step in single-step mode.
        /// </summary>
        public event EventHandler CPUStep;

        /// <summary>
        /// Exit Syscall
        /// </summary>
        public event EventHandler ExitSyscall;

        // If true, pause execution at the next opportunity.
        bool _break = false;

        public List<IntPtr> BreakPoints = new List<IntPtr>();

        /// <summary>
        /// Creates a new CPU. This constructor is only called inside the Machine constructor.
        /// </summary>
        internal CPU(Machine machine)
        {
            WorkerThread = new Thread(Run);
            unsafe
            {
                RF = (uint*)System.Runtime.InteropServices.Marshal.AllocHGlobal(32 * 4);
                for (int i = 0; i < 32; i++)
                    RF[i] = 0;
            }
            Machine = machine;
        }

        // Runs on the worker thread
        internal void Run()
        {
            while (true)
            {
                Step();
            }
        }

        /// <summary>
        /// Starts the CPU running
        /// </summary>
        public void Start()
        {
            WorkerThread.Start();
        }

        /// <summary>
        /// Pauses CPU execution.
        /// </summary>
        public void Break()
        {
            _break = true;
        }

        /// <summary>
        /// Resumes execution of the CPU.
        /// </summary>
        public void Resume()
        {
            _break = false;
            WorkerThread.Resume();
        }

        private unsafe void Step()
        {
            // Fetch the next instruction
            int pc = unchecked((int)PC++);
            IR = new Instruction(Machine.Memory[pc >> 2]);

            // Single-step mode
            if (SingleStep)
            {
                // TODO: Invoke this in correct thread
                if (CPUStep != null)
                    CPUStep(this, null);
                // Suspend execution of this thread
                // System.Diagnostics.Debugger.Break();
                // TODO: Use better method
                WorkerThread.Suspend();
            }

            // Trigger breakpoints
            if (_break || BreakPoints.Contains((IntPtr)pc))
            {
                // TODO: Invoke this in correct thread
                if (BreakpointHit != null)
                    BreakpointHit(this, null);
                // Suspend execution of this thread
                // System.Diagnostics.Debugger.Break();
                // Todo: Use better method
                WorkerThread.Suspend();
            }

            if (IR.OpCode == OpCode.Register)
            {
                ExecuteRegisterInstruction();
            }
            else if (IR.OpCode == OpCode.j || IR.OpCode == OpCode.jal)
            {
                throw new NotImplementedException();
            }
            else
            {
                ExecuteImmediateInstruction();
            }
        }

        private unsafe void ExecuteRegisterInstruction()
        {
            // Register format instructions have Op-Code "0" and a Function Code.
            switch (IR.FunctionCode)
            {
                // Shift Left Logical
                case FunctionCode.sll:
                    RF[IR.Rd] = RF[IR.Rt] << IR.sa;
                    break;
                // Shift Right Logical
                case FunctionCode.srl:
                    RF[IR.Rd] = RF[IR.Rt] >> IR.sa;
                    break;
                // Shift Right Arithmetic
                case FunctionCode.sra:
                    RF[IR.Rd] = unchecked((uint)((int)RF[IR.Rt] >> IR.sa));
                    break;
                // Shift Left Logical Variable
                case FunctionCode.sllv:
                    RF[IR.Rd] = RF[IR.Rt] << (int)RF[IR.Rs];
                    break;
                // Shift Right Logical Variable
                case FunctionCode.srlv:
                    RF[IR.Rd] = RF[IR.Rt] >> (int)RF[IR.Rs];
                    break;
                // Shift Right Arithmetic Variable
                case FunctionCode.srav:
                    RF[IR.Rd] = unchecked((uint)((int)RF[IR.Rt] >> (int)RF[IR.Rs]));
                    break;
                // Jump Register
                case FunctionCode.jr:
                    throw new NotImplementedException();
                // Jump and Link Register
                case FunctionCode.jalr:
                    throw new NotImplementedException();
                // System Call
                case FunctionCode.syscall:
                    // TODO: For now, all syscalls are simulated here.
                    Syscall();
                    break;
                // Move From High
                case FunctionCode.mfhi:
                    RF[IR.Rd] = High;
                    break;
                // Move to High
                case FunctionCode.mthi:
                    High = RF[IR.Rs];
                    break;
                // Move From Low
                case FunctionCode.mflo:
                    RF[IR.Rd] = Low;
                    break;
                // Move To Low
                case FunctionCode.mtlo:
                    Low = RF[IR.Rs];
                    break;
                // Multiply
                case FunctionCode.mult:
                    {
                        long result = (long)RF[IR.Rs] * (long)RF[IR.Rt];
                        Low = (uint)result;
                        High = (uint)(result >> 32);
                        break;
                    }
                // Multiply Unsigned
                case FunctionCode.multu:
                    {
                        ulong result = (ulong)RF[IR.Rs] * (ulong)RF[IR.Rt];
                        Low = (uint)result;
                        High = (uint)(result >> 32);
                        break;
                    }
                // Divide
                case FunctionCode.div:
                    {
                        long result;
                        Low = (uint)Math.DivRem((long)unchecked((uint)RF[IR.Rs]), unchecked((int)(long)RF[IR.Rs]), out result);
                        High = (uint)result;
                        break;
                    }
                // Divide Unsigned
                case FunctionCode.divu:
                    {
                        long result;
                        Low = (uint)Math.DivRem((long)RF[IR.Rs], (long)RF[IR.Rs], out result);
                        High = (uint)result;
                        break;
                    }
                // Add
                case FunctionCode.add:
                    RF[IR.Rd] = unchecked((uint)((int)RF[IR.Rs] + (int)RF[IR.Rt]));
                    // TODO: Generate exception in case of overflow.
                    break;
                // Add Unsigned
                case FunctionCode.addu:
                    RF[IR.Rd] = RF[IR.Rs] + RF[IR.Rt];
                    break;
                // Subtract
                case FunctionCode.sub:
                    RF[IR.Rd] = unchecked((uint)((int)RF[IR.Rs] - (int)RF[IR.Rt]));
                    break;
                // Subtract Unsigned
                case FunctionCode.subu:
                    RF[IR.Rd] = RF[IR.Rs] - RF[IR.Rt];
                    break;
                // And
                case FunctionCode.and:
                    RF[IR.Rd] = RF[IR.Rs] & RF[IR.Rt];
                    break;
                // Or
                case FunctionCode.or:
                    RF[IR.Rd] = RF[IR.Rs] | RF[IR.Rt];
                    break;
                // Exclusive Or
                case FunctionCode.xor:
                    RF[IR.Rd] = RF[IR.Rs] ^ RF[IR.Rt];
                    break;
                // Nor
                case FunctionCode.nor:
                    RF[IR.Rd] = ~(RF[IR.Rs] | RF[IR.Rt]);
                    break;
                // Set on Less Than
                case FunctionCode.slt:
                    throw new NotImplementedException();
                // Set on Less Than Unsigned
                case FunctionCode.sltu:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        private unsafe void ExecuteImmediateInstruction()
        {
            switch (IR.OpCode)
            {
                // The default Op-Code that deals with registers and is defined by a function code.
                case OpCode.Register:
                    throw new NotImplementedException();
                // Special branches with a branch code.
                case OpCode.Branch:
                    switch (IR.BranchCode)
                    {
                        // Branch if Greater Than or Equal to Zero
                        case BranchCode.bgez:
                            if ((int)RF[IR.Rs] >= 0)
                                PC += unchecked((int)IR.SignExtendedImmediate);
                            break;
                        // Branch if Greater Than or Equal to Zero, and Link
                        case BranchCode.bgezal:
                            throw new NotImplementedException();
                        // Branch if Less than Zero
                        case BranchCode.bltz:
                            throw new NotImplementedException();
                        // Branch if Less than Zero, and Link
                        case BranchCode.bltzal:
                            throw new NotImplementedException();
                        default:
                            // TODO: Invalid expression instruction?
                            throw new NotImplementedException();
                    }
                    break;
                // The Jump instruction
                case OpCode.j:
                    throw new NotImplementedException();
                // The Jump and Link (call procedure) instruction
                case OpCode.jal:
                    throw new NotImplementedException();
                // Branch if Equal
                case OpCode.beq:
                    throw new NotImplementedException();
                // Branch if Not Equal
                case OpCode.bne:
                    if (RF[IR.Rs] != RF[IR.Rt])
                        PC += unchecked(IR.SignExtendedImmediate);
                    break;
                // Branch if Less Than or Equal to Zero
                case OpCode.blez:
                    if ((int)RF[IR.Rs] <= 0)
                        PC += unchecked(IR.SignExtendedImmediate);
                    break;
                // Branch if Greater than Zero
                case OpCode.bgtz:
                    throw new NotImplementedException();
                // Add Immediate
                case OpCode.addi:
                    // TODO: Overflow exception
                    RF[IR.Rt] = RF[IR.Rs] + IR.SignExtendedImmediate;
                    break;
                // Add Immediate Unsigned
                case OpCode.addiu:
                    RF[IR.Rt] = RF[IR.Rs] + IR.SignExtendedImmediate;
                    break;
                // Set on Less Than Immediate
                case OpCode.slti:
                    throw new NotImplementedException();
                // Set on Less Than Immediate Unsigned
                case OpCode.sltiu:
                    throw new NotImplementedException();
                // And Immediate
                case OpCode.andi:
                    throw new NotImplementedException();
                // Or Immediate
                case OpCode.ori:
                    RF[IR.Rt] = RF[IR.Rs] | IR.Immediate;
                    break;
                // Xor Immediate
                case OpCode.xori:
                    throw new NotImplementedException();
                // Load Upper Immediate
                case OpCode.lui:
                    RF[IR.Rt] = (uint)IR.Immediate << 16;
                    break;
                // Misc. System Instructions
                case OpCode.System:
                    throw new NotImplementedException();
                // Load Byte
                case OpCode.lb:
                    throw new NotImplementedException();
                // Load Halfword
                case OpCode.lh:
                    throw new NotImplementedException();
                // Load Word Left
                case OpCode.lwl:
                    throw new NotImplementedException();
                // Load Word
                case OpCode.lw:
                    throw new NotImplementedException();
                // Load Byte Unsigned
                case OpCode.lbu:
                    throw new NotImplementedException();
                // Load Halfword Unsigned
                case OpCode.lhu:
                    throw new NotImplementedException();
                // Load Word Right
                case OpCode.lwr:
                    throw new NotImplementedException();
                // Store Byte
                case OpCode.sb:
                    throw new NotImplementedException();
                // Store Halfword
                case OpCode.sh:
                    throw new NotImplementedException();
                // Store Word Left
                case OpCode.swl:
                    throw new NotImplementedException();
                // Store Word
                case OpCode.sw:
                    throw new NotImplementedException();
                // Store Word Right
                case OpCode.swr:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        private unsafe void Syscall()
        {
            switch (RF[(int)Register.v0])
            {
                // Print Integer, a0 = number to be printed
                case 1:
                    Console.Write((int)RF[(int)Register.a0]);
                    break;
                // Print Float, a0 = number to be printed
                case 2:
                    throw new NotImplementedException();
                // Print Double, a0 = number to be printed
                case 3:
                    throw new NotImplementedException();
                // Print String, a0 = address of string in memory
                case 4:
                    int addr = (int)RF[(int)Register.a0];
                    int offset = addr % 4;
                    addr >>= 2;
                    while (true)
                    {
                        uint word = Machine.Memory[addr];
                        byte b = 0;
                        while (offset < 4)
                        {
                            b = (byte)(word >> (offset * 8));
                            if (b == 0)
                                break;
                            if (b == 10)
                                Console.WriteLine();
                            else
                                Console.Write((char)b);
                            offset++;
                        }
                        offset = 0;
                        if (b == 0)
                            break;
                        addr++;
                    }
                    break;
                // Read Integer, Number returned in v0
                case 5:
                    RF[(int)Register.v0] = (uint)int.Parse(Console.ReadLine());
                    break;
                // Read Float, Number returned in f0
                case 6:
                    throw new NotImplementedException();
                // Read Double, Number returned in f0
                case 7:
                    throw new NotImplementedException();
                // Read String, a0 = address of input buffer in memory, a1 = length of buffer (n)
                case 8:
                    throw new NotImplementedException();
                // Sbrk, a0 = amount, address returned in v0
                case 9:
                    throw new NotImplementedException();
                // Exit
                case 10:
                    if (ExitSyscall != null)
                        ExitSyscall(this, null);
                    // TODO: Halt
                    break;
            }
        }
    }
}
