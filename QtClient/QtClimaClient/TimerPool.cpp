#include "TimerPool.h"

TimerPool* TimerPool::_instance = nullptr;

TimerPool::TimerPool(QObject *parent) : QObject(parent)
{

}

QTimer *TimerPool::getUpdateTimer()
{
    if(!_timers.contains("UpdateUI"))
    {
        QTimer *updateTimer = new QTimer();
        updateTimer->setInterval(2000);
        updateTimer->start();
        _timers.insert("UpdateUI", updateTimer);
    }

    return _timers["UpdateUI"];
}
