#include "syscalls.h"

int fibonacci(int n);

void main()
{
	int count, i;

	kprintf("Fibonacci numbers!\nHow many? ");

	count = getint();

	while (count != 0)
	{
		kprintf("\nOk, printing "); kprintf("%d", count); kprintf(" fibonacci numbers:\n");
	
		for (i = 1; i <= count; i++)
		{
			kprintf("%d ", fibonacci(i));
		}

		kprintf("\n\nHow many? ");
		count = getint();
	}

	kprintf("\nHasta luego!");
}

int fibonacci(int n)
{
	if (n == 1 || n == 2)
	    return 1;

	return fibonacci(n - 1) + fibonacci(n - 2);
}
