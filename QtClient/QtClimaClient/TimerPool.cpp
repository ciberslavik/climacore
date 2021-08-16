#include "TimerPool.h"

TimerPool* TimerPool::_instance = nullptr;

TimerPool::TimerPool(QObject *parent) : QObject(parent)
{

}

QTimer *TimerPool::getUpdateTimer()
{
    if(!_timers.contains("UpdateUI"))
        _timers.insert("UpdateUI", new QTimer());

    return _timers["UpdateUI"];
}
