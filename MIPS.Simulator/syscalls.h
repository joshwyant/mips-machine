#ifndef __SYSCALLS_H__
#define __SYSCALLS_H__

#ifndef true
#define true 1
#endif

#ifndef false
#define false 0
#endif


extern void main();
void __start();
void kprintf(const char* format, ...);
void println(char *str);

#endif