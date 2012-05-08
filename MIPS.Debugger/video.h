#ifndef __VIDEO_H__
#define __VIDEO_H__

#define screen_width 1024
#define screen_height 768

void enable_video();

void refresh_screen();

void clear_screen(unsigned color);

void draw_circle(int x, int y, int radius, unsigned color);

int video_syscall(int id);

#endif