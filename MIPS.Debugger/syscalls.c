#include "syscalls.h"

extern void main();

void __start()
{
	// Call the "main" function.
	main();

	// 'Exit' syscall.
	asm ("li $v0, 10\n\t"
	     "syscall"
		 : : : "v0"
		 );
}

void print(const char *str)
{
	// Print string syscall
	asm ("li $v0, 4\n\t"
		 "la $a0, %0\n\t"
		 "syscall"
		 : : "m" (*str)
	     : "v0", "a0"
		 );
}

int getint()
{
	int result;

	// Get integer syscall
	asm ("li $v0, 5\n\t"
		 "addu %0, $0, $v0\n\t"
		 "syscall"
		 : "=r" (result)
		 : : "v0"
		 );

	return result;
}

void print_char(char c)
{
	char ch[2];
	ch[0] = c;
	ch[1] = '\0';

	print(ch);
}

void println(char *str)
{
	print(str);
	print("\n");
}

void printhexb(char c)
{
    char str[3] = {'0'+((c&0xF0)>>4),'0'+(c&0xF),0};
    if (str[0] > '9') str[0] += 7;
    if (str[1] > '9') str[1] += 7;
    print(str);
}

void printhexw(short w)
{
    printhexb(w>>8);
    printhexb(w);
}

void printhexd(int d)
{
    printhexw(d>>16);
    printhexw(d);
}

void printdec(int x)
{
    unsigned temp = (unsigned)(x < 0 ? -x : x);
    unsigned div;
    unsigned mod;
    char str[11];
    char str2[11];
    char *strptr = str;
    char *strptr2 = str2;
    if (x < 0) print_char('-');
    do
    {
        div = temp/10;
        mod = temp%10;
        temp = div;
        *strptr = '0'+mod;
        strptr++;
    } while (temp != 0);
    *strptr = 0;
    do
    {
        strptr--;
        *strptr2 = *strptr;
        strptr2++;
    } while (strptr != str);
    *strptr2 = 0;
    print(str2);
}

void kprintf(const char* format, ...)
{
    int arg = 0;
	unsigned *value = (unsigned*)(&format) + 1;
    while (*format)
    {
        unsigned val = *value;
        // Skip 3 items on the stack at %ebp: prev %epb, %eip, and $format.
        if (*format == '%')
        {
            switch (*++format)
            {
                case 'd':
                    arg++;
                    printdec(val);
                    break;
                case 's':
                    arg++;
                    print((const char*)val);
                    break;
                case 'c':
                    arg++;
                    print_char((char)val);
                    break;
                case 'b':
                    arg++;
                    print("0x");
                    printhexb(val);
                    break;
                case 'w':
                    arg++;
                    print("0x");
                    printhexw(val);
                    break;
                case 'l':
                    arg++;
                    print("0x");
                    printhexd(val);
                    break;
                case '%':
                    print_char('%');
                    break;
                default:
                    print_char('%');
                    print_char(*format);
                    break;
            }
        }
        else
        {
            print_char(*format);
        }
        format++;
		value++;
    }
}