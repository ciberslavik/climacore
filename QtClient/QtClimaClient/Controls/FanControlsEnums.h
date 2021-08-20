#ifndef FANCONTROLSENUMS_H
#define FANCONTROLSENUMS_H
typedef enum class FanState
{
    Enabled,
    Disabled,
    Alarm
}FanState_t;

typedef enum class FanMode
{
    Auto,
    Manual,
    Disabled
}FanMode_t;

#endif // FANCONTROLSENUMS_H
