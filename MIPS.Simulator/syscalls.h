#ifndef __SYSCALLS_H__
#define __SYSCALLS_H__

#ifndef true
#define true 1
#endif

#ifndef false
#define false 0
#endif

void kprintf(const char* format, ...);
void println(char *str);
int getint();
void printdec(int);
int kstrcmp(const char* a, const char* b);

#endif