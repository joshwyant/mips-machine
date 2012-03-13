    # SAMPLE.ASM: This program prints a greeting.

              .data
    #  Define a greeting message.
    Message:  .asciiz   "Hello World!\n"

              .text
    #  Print the greeting message.
              ori        $v0, $0, 4
              lui        $a0, 0x1001
              ori        $a0, $a0, Message
              syscall
    #  Return to the operating system.
              ori        $v0, $0, 10
              syscall