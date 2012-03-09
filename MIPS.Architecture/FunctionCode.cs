namespace MIPS.Architecture
{
    /// <summary>
    /// Defines function codes for the register Op-Code.
    /// </summary>
    public enum FunctionCode
    {
        /// <summary>
        /// Shift Left Logical
        /// </summary>
        sll = 0x0,
        /// <summary>
        /// Shift Right Logical
        /// </summary>
        srl = 0x2,
        /// <summary>
        /// Shift Right Arithmetic
        /// </summary>
        sra = 0x3,
        /// <summary>
        /// Shift Left Logical Variable
        /// </summary>
        sllv = 0x4,
        /// <summary>
        /// Shift Right Logical Variable
        /// </summary>
        srlv = 0x6,
        /// <summary>
        /// Shift Right Arithmetic Variable
        /// </summary>
        srav = 0x7,
        /// <summary>
        /// Jump Register
        /// </summary>
        jr = 0x8,
        /// <summary>
        /// Jump and Link Register
        /// </summary>
        jalr = 0x9,
        /// <summary>
        /// System Call
        /// </summary>
        syscall = 0x0C,
        /// <summary>
        /// Move From High
        /// </summary>
        mfhi = 0x10,
        /// <summary>
        /// Move to High
        /// </summary>
        mthi = 0x11,
        /// <summary>
        /// Move From Low
        /// </summary>
        mflo = 0x12,
        /// <summary>
        /// Move To Low
        /// </summary>
        mtlo = 0x13,
        /// <summary>
        /// Multiply
        /// </summary>
        mult = 0x18,
        /// <summary>
        /// Multiply Unsigned
        /// </summary>
        multu = 0x19,
        /// <summary>
        /// Divide
        /// </summary>
        div = 0x1A,
        /// <summary>
        /// Divide Unsigned
        /// </summary>
        divu = 0x1B,
        /// <summary>
        /// Add
        /// </summary>
        add = 0x20,
        /// <summary>
        /// Add Unsigned
        /// </summary>
        addu = 0x21,
        /// <summary>
        /// Subtract
        /// </summary>
        sub = 0x22,
        /// <summary>
        /// Subtract Unsigned
        /// </summary>
        subu = 0x23,
        /// <summary>
        /// And
        /// </summary>
        and = 0x24,
        /// <summary>
        /// Or
        /// </summary>
        or = 0x25,
        /// <summary>
        /// Exclusive Or
        /// </summary>
        xor = 0x26,
        /// <summary>
        /// Nor
        /// </summary>
        nor = 0x27,
        /// <summary>
        /// Set on Less Than
        /// </summary>
        slt = 0x2A,
        /// <summary>
        /// Set on Less Than Unsigned
        /// </summary>
        sltu = 0x2B,
    }
}
