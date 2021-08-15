#pragma once

#include <Services/QSerializer.h>
#include <QObject>
#include <QDateTime>

class GraphInfo : public QSerializer
{
    Q_GADGET
    QS_SERIALIZABLE
public:
    GraphInfo(){}
    virtual ~GraphInfo(){}
    QS_FIELD(QString, Key)
    QS_FIELD(QString, Name)
    QS_FIELD(QString, Description)
    QS_FIELD(QDateTime, CreationTime)
    QS_FIELD(QDateTime, ModifiedTime)
};

