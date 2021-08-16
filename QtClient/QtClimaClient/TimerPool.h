#pragma once

#include <QObject>
#include <QTimer>
#include <QMap>

class TimerPool : public QObject
{
    Q_OBJECT
public:
    explicit TimerPool(QObject *parent = nullptr);

    static TimerPool* instance()
    {
        if(_instance == nullptr)
            _instance = new TimerPool();

        return _instance;
    }

    QTimer* getUpdateTimer();
signals:

private:
    static TimerPool *_instance;
    QMap<QString,QTimer*> _timers;
};

