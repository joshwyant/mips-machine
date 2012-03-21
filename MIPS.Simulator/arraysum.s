#	Main Program to allow user to input an array,
#	Then call the PENO function which sums to positive and negative values in the array,
#	Then displays the positive and negative sums
#
		.data
Prompt1:	.asciiz	"\n Enter Next Array Value. Terminate with -9999 "
Prompt2:	.asciiz "\n The Sum of the Positive Values is "
Prompt3:	.asciiz "\n The Sum of the Negative Values is "
X:		.word	0:100
		
		.text
#	Input the values for the array X
		la	$t1,X
		li	$t2,100
		li	$t3,-9999
		li	$t4,0

InputLoop:	li	$v0,4
		la	$a0,Prompt1
		syscall
		li	$v0,5
		syscall
		beq	$v0,$t3,ExitLoop	# Check for terminating value
		sw	$v0,0($t1)
		addi	$t1,$t1,4
		addi	$t4,$t4,1
		bne	$t4,$t2,InputLoop	# Repeat until the array is full

ExitLoop:
		addiu	$sp,$sp,-16		# add 4 words to the top of the stack
		la	$t5,X
		sw	$t5,0($sp)		# Address of array goes first on the stack
		sw	$t4,4($sp)		# size of the arrary goes 2nd on the stack
		sw	$0,8($sp)		# Positive sums go 3rd on the stack
		sw	$0,12($sp)		# Negative sums go 4th on the stack
		jal	PENO

		li	$v0,4			# Print the sum of the positive values
		la	$a0,Prompt2
		syscall
		lw	$a0,8($sp)
		li	$v0,1
		syscall

		li	$v0,4			# Print the sum of the negative values
		la	$a0,Prompt3
		syscall
		lw	$a0,12($sp)
		li	$v0,1
		syscall
		
		li	$v0,10
		syscall		
						
PENO:	
		lw	$t6,0($sp)
		lw	$t7,4($sp)
		li	$v0, 0
		li	$v1, 0
LOOP:	
		lw	$t0, 0($t6)		# Get next number from the array
		bltz	$t0, NEG	
		add	$v0, $v0, $t0	# Add to positive sum
		b	CHK
NEG:	
		add	$v1, $v1, $t0	# Add to negative sum
CHK:	
		addi	$t6,$t6,4
		addi	$t7, $t7, -1
		bgtz	$t7, LOOP
		sw	$v0,8($sp)		# Put Positive sum on the stack
		sw	$v1,12($sp)		# put Negative sum on the stack
		jr	$ra