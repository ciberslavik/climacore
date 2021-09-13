#pragma once
#include <QObject>
#include <Services/QSerializer.h>
#include <Models/SchedulerInfo.h>

class SchedulerInfoResponse:public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE;
public:
    SchedulerInfoResponse(){}
    virtual ~SchedulerInfoResponse(){}

    QS_OBJECT(SchedulerInfo, Info)
};
