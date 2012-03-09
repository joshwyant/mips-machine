namespace MIPS.Architecture
{
    public enum OpCode
    {
        /// <summary>
        /// The default Op-Code that deals with registers and is defined by a function code.
        /// </summary>
        Register = 0x0,
        /// <summary>
        /// Special branches with a branch code.
        /// </summary>
        Branch = 0x1,
        /// <summary>
        /// The Jump instruction
        /// </summary>
        j = 0x2,
        /// <summary>
        /// The Jump and Link (call procedure) instruction
        /// </summary>
        jal = 0x3,
        /// <summary>
        /// Branch if Equal
        /// </summary>
        beq = 0x4,
        /// <summary>
        /// Branch if Not Equal
        /// </summary>
        bne = 0x5,
        /// <summary>
        /// Branch if Less Than or Equal to Zero
        /// </summary>
        blez = 0x6,
        /// <summary>
        /// Branch if Greater than Zero
        /// </summary>
        bgtz = 0x7,
        /// <summary>
        /// Add Immediate
        /// </summary>
        addi = 0x8,
        /// <summary>
        /// Add Immediate Unsigned
        /// </summary>
        addiu = 0x9,
        /// <summary>
        /// Set on Less Than Immediate
        /// </summary>
        slti = 0xA,
        /// <summary>
        /// Set on Less Than Immediate Unsigned
        /// </summary>
        sltiu = 0xB,
        /// <summary>
        /// And Immediate
        /// </summary>
        andi = 0xC,
        /// <summary>
        /// Or Immediate
        /// </summary>
        ori = 0xD,
        /// <summary>
        /// Xor Immediate
        /// </summary>
        xori = 0xE,
        /// <summary>
        /// Load Upper Immediate
        /// </summary>
        lui = 0xF,
        /// <summary>
        /// Misc. System Instructions
        /// </summary>
        System = 0x10,
        /// <summary>
        /// Load Byte
        /// </summary>
        lb = 0x20,
        /// <summary>
        /// Load Halfword
        /// </summary>
        lh = 0x21,
        /// <summary>
        /// Load Word Left
        /// </summary>
        lwl = 0x22,
        /// <summary>
        /// Load Word
        /// </summary>
        lw = 0x23,
        /// <summary>
        /// Load Byte Unsigned
        /// </summary>
        lbu = 0x24,
        /// <summary>
        /// Load Halfword Unsigned
        /// </summary>
        lhu = 0x25,
        /// <summary>
        /// Load Word Right
        /// </summary>
        lwr = 0x26,
        /// <summary>
        /// Store Byte
        /// </summary>
        sb = 0x28,
        /// <summary>
        /// Store Halfword
        /// </summary>
        sh = 0x29,
        /// <summary>
        /// Store Word Left
        /// </summary>
        swl = 0x2A,
        /// <summary>
        /// Store Word
        /// </summary>
        sw = 0x2B,
        /// <summary>
        /// Store Word Right
        /// </summary>
        swr = 0x2E,
    }
}
