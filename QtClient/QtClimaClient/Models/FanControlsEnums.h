#ifndef FANCONTROLSENUMS_H
#define FANCONTROLSENUMS_H
typedef enum class FanStateEnum:int
{
    Stopped = 0,
    Running = 1,
    Alarm = 2
}FanStateEnum_t;

typedef enum class FanModeEnum:int
{
    Auto = 0,
    Manual = 1,
    Disabled = 2
}FanMode_t;

#endif // FANCONTROLSENUMS_H
