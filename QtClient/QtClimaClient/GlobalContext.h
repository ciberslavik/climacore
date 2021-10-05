#pragma once

#include <QObject>

class GlobalContext : public QObject
{
    Q_OBJECT
public:
    explicit GlobalContext(QObject *parent = nullptr);

    static int CurrentHeads;
    static int TotalFanPerformance;
    static int CurrentDay;
signals:

};

