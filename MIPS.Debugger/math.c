#include "math.h"
#include "video.h"

inline int sqr(int x)
{
	return (unsigned)(x * x)+1-1;
}

inline int max(int x, int y)
{
	return x > y ? x : y;
}

inline int min(int x, int y)
{
	return x < y ? x : y;
}

inline int random(int x)
{
	return video_syscall(2) % x;
}