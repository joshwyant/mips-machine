#include "video.h"
#include "syscalls.h"
#include "math.h"

int video_syscall(int id);

//unsigned* videomem = (unsigned*)0x80000000;

void enable_video()
{
	video_syscall(0);
}

void refresh_screen()
{
	video_syscall(1);
}

int video_syscall(int id)
{
	int result;

	// Video syscall
	asm ("li $v0, 100\n\t"
		 "addu $a0, %1, $0\n\t"
		 "syscall\n\t"
		 "addu %0, $0, $v0"
		 : "=r" (result)
		 : "r" (id)
	     : "v0", "a0"
		 );

	return result;
}

void clear_screen(unsigned color)
{
	unsigned* videomem = (unsigned*)0x80000000;

	int i;

	for (i = 0; i < screen_width*screen_height; i++)
		videomem[i] = color;
}

void draw_circle(int x, int y, int radius, unsigned color)
{
	unsigned* videomem = (unsigned*)0x80000000;

	int X, Y;
	int start_x = max(0, x - radius);
	int start_y = max(0, y - radius);
	int end_x = min(screen_width, start_x + radius * 2);
	int end_y = min(screen_height, start_y + radius * 2);
	int r2 = sqr(radius);

	for (X = start_x; X < end_x; X++)
		for (Y = start_y; Y < end_y; Y++)
			if ((sqr(X - x) + sqr(Y - y)) < r2)
				videomem[X+Y*1024] = color;
}