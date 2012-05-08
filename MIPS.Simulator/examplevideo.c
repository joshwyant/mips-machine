#include "syscalls.h"
#include "video.h"
#include "math.h"

void main()
{
	// Enable advanced video on the display.
	enable_video();

	clear_screen(0x008000FF);

	int i, j, color;
	for (j = 64; j < 768; j += 128)
	for (i = 64; i < 1024; i += 128)
	{
		if (color == 0)
			color = 0xFF0000;

		draw_circle(
			i, // x 
			j, // y
			48, // radius
			color // color
		);

		color >>= 8;

		refresh_screen();
	}
}