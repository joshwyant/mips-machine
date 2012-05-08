using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace MIPS.Architecture
{
    public class CPU
    {
        #region Members
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
        /// Thread synchronization for CPU traps.
        /// </summary>
        private ManualResetEvent TrapSync = new ManualResetEvent(false);

        public ManualResetEvent SyscallSync = new ManualResetEvent(false);

        /// <summary>
        /// Occurs when a breakpoint is hit.
        /// </summary>
        public event EventHandler BreakpointHit;

        /// <summary>
        /// Gets or sets whether the CPU should run in single-step mode
        /// </summary>
        public bool SingleStep { get; set; }

        /// <summary>
        /// Occurs when the CPU makes a step in single-step mode.
        /// </summary>
        public event EventHandler CPUStep;

        /// <summary>
        /// Occurs when the CPU is paused using Pause().
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Exit Syscall
        /// </summary>
        public event EventHandler ExitSyscall;

        /// <summary>
        /// yscall
        /// </summary>
        public event EventHandler Syscall;

        // If true, pause execution at the next opportunity.
        bool pause = false;

        bool stop = false;

        public List<IntPtr> BreakPoints = new List<IntPtr>();

        public long Elapsed;
        #endregion

        #region Helper Methods
        /// <summary>
        /// Helper method to raise an event on the correct thread
        /// </summary>
        private void RaiseEventOnUIThread(Delegate e, params object[] args)
        {
            if (e == null)
                return;

            foreach (Delegate d in e.GetInvocationList())
            {
                ISynchronizeInvoke syncObj = d.Target as ISynchronizeInvoke;
                if (syncObj == null)
                {
                    d.DynamicInvoke(args);
                }
                else
                {
                    syncObj.EndInvoke(syncObj.BeginInvoke(d, args));
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new CPU. This constructor is only called inside the Machine constructor.
        /// </summary>
        internal CPU(Machine machine)
        {
            Machine = machine;

            // Create the thread for running the CPU
            WorkerThread = new Thread(Run);

            // Allocate memory for the 32 general purpose registers
            unsafe
            {
                RF = (uint*)System.Runtime.InteropServices.Marshal.AllocHGlobal(32 * 4);
            }

            Initialize();
        }

        unsafe void Initialize()
        {
            // Initialize each of the registers to zero.
            for (int i = 0; i < 32; i++)
                RF[i] = 0;

            // Initialize the stack pointer
            RF[(int)Register.sp] = 0x7fffeffc;
        }
        #endregion

        #region Control Methods
        // Runs on the worker thread
        private void Run()
        {
            isRunning = true;
            while (!stop)
            {
                Step();
            }
            isRunning = false;

            stop = false;
        }

        /// <summary>
        /// Starts the CPU running.
        /// </summary>
        public void Start()
        {
            if (WorkerThread.ThreadState == ThreadState.Stopped)
            {
                WorkerThread = new Thread(Run);
            }

            if (WorkerThread.IsAlive)
            {
                throw new InvalidOperationException("CPU is already running.");
            }

            // Sets the trap sync, ready to break
            TrapSync.Set();
            pause = false;
            WorkerThread.Start();
        }

        /// <summary>
        /// Stops the CPU.
        /// </summary>
        public void Stop()
        {
            if (WorkerThread.IsAlive)
            {
                stop = true;

                // TODO: I think this is the right way to do it.
                if (Thread.CurrentThread != WorkerThread)
                {
                    // TODO: I'm not sure if this is a good idea, but it gets the thread to stop.
                    TrapSync.Set();
                    SyscallSync.Set();

                    WorkerThread.Join();
                }
            }

            BreakPoints.Clear();
        }

        /// <summary>
        /// Pauses CPU execution.
        /// </summary>
        public void Pause()
        {
            if (!WorkerThread.IsAlive)
                throw new InvalidOperationException("CPU is not running.");

            pause = true;
        }

        /// <summary>
        /// Resumes execution of the CPU.
        /// </summary>
        public void Resume()
        {
            if (!WorkerThread.IsAlive)
                throw new InvalidOperationException("CPU is not running.");

            pause = false;
            SingleStep = false;
            TrapSync.Set();
        }

        /// <summary>
        /// Steps to the next instruction.
        /// </summary>
        public void NextStep()
        {
            if (!WorkerThread.IsAlive)
                throw new InvalidOperationException("CPU is not running.");

            SingleStep = true;
            pause = false;

            // Signal the CPU to execute the next instruction
            TrapSync.Set();
        }
        #endregion

        #region Execution Methods
        private unsafe void Step()
        {
            // Fetch the next instruction
            var pc = unchecked((uint)PC);
            IR = new Instruction(Machine.Memory.GetWord(pc));


            // Raise events
            if (pause)
            {
                RaiseEventOnUIThread(Paused, this, null);
            }
            if (BreakPoints.Contains((IntPtr)pc))
            {
                RaiseEventOnUIThread(BreakpointHit, this, null);
            }
            if (SingleStep)
            {
                RaiseEventOnUIThread(CPUStep, this, null);
            }

            // Pause CPU execution
            if (pause || BreakPoints.Contains((IntPtr)pc) || SingleStep)
            {
                // System.Diagnostics.Debugger.Break();
                // Suspend this thread and wait for the CPU to be resumed
                TrapSync.Reset();
                TrapSync.WaitOne();
                pause = false;
            }

            if (IR.OpCode == OpCode.Register)
            {
                ExecuteRegisterInstruction();
            }
            else
            {
                ExecuteImmediateInstruction();
            }

            // Increment the program counter
            PC++;

            Elapsed++;
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
                    PC = (uint*)RF[IR.Rs];
                    break;
                // Jump and Link Register
                case FunctionCode.jalr:
                    RF[IR.Rd] = (uint)(PC + 1);
                    PC = (uint*)RF[IR.Rs];
                    break;
                // System Call
                case FunctionCode.syscall:
                    SyscallSync.Reset();
                    RaiseEventOnUIThread(Syscall, this, null);
                    SyscallSync.WaitOne();
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
                    if ((int)RF[IR.Rs] < (int)RF[IR.Rt])
                        RF[IR.Rd] = 1;
                    else
                        RF[IR.Rd] = 0;
                    break;
                // Set on Less Than Unsigned
                case FunctionCode.sltu:
                    if (RF[IR.Rs] < RF[IR.Rt])
                        RF[IR.Rd] = 1;
                    else
                        RF[IR.Rd] = 0;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private unsafe void ExecuteImmediateInstruction()
        {
            switch (IR.OpCode)
            {
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
                            if ((int)RF[IR.Rs] >= 0)
                            {
                                RF[(int)Register.ra] = (uint)(PC + 1);
                                PC += unchecked((int)IR.SignExtendedImmediate);
                            }
                            break;
                        // Branch if Less than Zero
                        case BranchCode.bltz:
                            if ((int)RF[IR.Rs] < 0)
                                PC += unchecked((int)IR.SignExtendedImmediate);
                            break;
                        // Branch if Less than Zero, and Link
                        case BranchCode.bltzal:
                            if ((int)RF[IR.Rs] < 0)
                            {
                                RF[(int)Register.ra] = (uint)(PC + 1);
                                PC += unchecked((int)IR.SignExtendedImmediate);
                            }
                            break;
                        default:
                            // TODO: Invalid expression instruction?
                            throw new NotImplementedException();
                    }
                    break;
                // The Jump instruction
                case OpCode.j:
                    PC = (uint*)(IR.Target << 2) - 1;
                    break;
                // The Jump and Link (call procedure) instruction
                case OpCode.jal:
                    RF[(int)Register.ra] = (uint)(PC + 1);
                    PC = (uint*)(IR.Target << 2) - 1;
                    break;
                // Branch if Equal
                case OpCode.beq:
                    if (RF[IR.Rs] == RF[IR.Rt])
                        PC += unchecked(IR.SignExtendedImmediate);
                    break;
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
                    if ((int)RF[IR.Rs] > 0)
                        PC += unchecked(IR.SignExtendedImmediate);
                    break;
                // Add Immediate
                case OpCode.addi:
                    // TODO: Overflow exception
                    RF[IR.Rt] = RF[IR.Rs] + (IR.SignExtendedImmediate);
                    break;
                // Add Immediate Unsigned
                case OpCode.addiu:
                    RF[IR.Rt] = RF[IR.Rs] + (IR.SignExtendedImmediate);
                    break;
                // Set on Less Than Immediate
                case OpCode.slti:
                    if ((int)RF[IR.Rs] < (int)IR.SignExtendedImmediate)
                        RF[IR.Rt] = 1;
                    else
                        RF[IR.Rt] = 0;
                    break;
                // Set on Less Than Immediate Unsigned
                case OpCode.sltiu:
                    if (RF[IR.Rs] < IR.Immediate)
                        RF[IR.Rt] = 1;
                    else
                        RF[IR.Rt] = 0;
                    break;
                // And Immediate
                case OpCode.andi:
                    RF[IR.Rt] = RF[IR.Rs] & IR.Immediate;
                    break;
                // Or Immediate
                case OpCode.ori:
                    RF[IR.Rt] = RF[IR.Rs] | IR.Immediate;
                    break;
                // Xor Immediate
                case OpCode.xori:
                    RF[IR.Rt] = RF[IR.Rs] ^ IR.Immediate;
                    break;
                // Load Upper Immediate
                case OpCode.lui:
                    RF[IR.Rt] = (uint)IR.Immediate << 16;
                    break;
                // Misc. System Instructions
                case OpCode.System:
                    throw new NotImplementedException();
                // Load Byte
                case OpCode.lb:
                    RF[IR.Rt] = (uint)(int)(sbyte)Machine.Memory.GetByte(RF[IR.Rs] + IR.SignExtendedImmediate);
                    break;
                // Load Halfword
                case OpCode.lh:
                    RF[IR.Rt] = (uint)(int)(short)Machine.Memory.GetShort(RF[IR.Rs] + IR.SignExtendedImmediate);
                    break;
                // Load Word Left
                case OpCode.lwl:
                    throw new NotImplementedException();
                // Load Word
                case OpCode.lw:
                    RF[IR.Rt] = Machine.Memory.GetWord(RF[IR.Rs] + IR.SignExtendedImmediate);
                    break;
                // Load Byte Unsigned
                case OpCode.lbu:
                    RF[IR.Rt] = Machine.Memory.GetByte(RF[IR.Rs] + IR.SignExtendedImmediate);
                    break;
                // Load Halfword Unsigned
                case OpCode.lhu:
                    RF[IR.Rt] = Machine.Memory.GetShort(RF[IR.Rs] + IR.SignExtendedImmediate);
                    break;
                // Load Word Right
                case OpCode.lwr:
                    throw new NotImplementedException();
                // Store Byte
                case OpCode.sb:
                    Machine.Memory.SetByte(RF[IR.Rs] + IR.SignExtendedImmediate, (byte)RF[IR.Rt]);
                    break;
                // Store Halfword
                case OpCode.sh:
                    Machine.Memory.SetShort(RF[IR.Rs] + IR.SignExtendedImmediate, (ushort)RF[IR.Rt]);
                    break;
                // Store Word Left
                case OpCode.swl:
                    throw new NotImplementedException();
                // Store Word
                case OpCode.sw:
                    Machine.Memory.SetWord(RF[IR.Rs] + IR.SignExtendedImmediate, RF[IR.Rt]);
                    break;
                // Store Word Right
                case OpCode.swr:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        bool isRunning = false;

        public bool IsRunning { get { return isRunning; } }

        public bool IsPaused { get { return pause; } }
    }
}
