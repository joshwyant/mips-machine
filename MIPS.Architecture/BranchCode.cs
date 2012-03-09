namespace MIPS.Architecture
{
    public enum BranchCode
    {
        /// <summary>
        /// Branch if Less than Zero
        /// </summary>
        bltz = 0x0,
        /// <summary>
        /// Branch if Greater Than or Equal to Zero
        /// </summary>
        bgez = 0x1,
        /// <summary>
        /// Branch if Less Than Zero, and Link
        /// </summary>
        bltzal = 0x10,
        /// <summary>
        /// Branch if Greater Than or Equal to Zero, and Link
        /// </summary>
        bgezal = 0x11,

    }
}
