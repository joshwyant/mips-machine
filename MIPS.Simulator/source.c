#include "syscalls.h"

void main()
{
	int i = 0;
	int j = 0;
	for (i = 0; i < 10; i++)
		j += i;

	println("Hello, world!");

	if (false)
		println("True!!");
	else
		println("False!!");
}

