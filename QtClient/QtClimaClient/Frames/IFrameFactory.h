#pragma once

#include "FrameBase.h"

#include <QObject>

class IFrameFactory
{
public:
    virtual FrameBase *CreateFrame(const QString &frameName) = 0;
};
Q_DECLARE_INTERFACE(IFrameFactory,"Frame factory /1.0")
