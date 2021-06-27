#pragma once

#include "FrameBase.h"
#include "MainMenuFrame.h"
#include "SystemStateFrame.h"
#include <Services/FrameName.h>
#include <QObject>

class IFrameFactory
{
public:
    virtual SystemStateFrame *CreateSystemStateFrame() = 0;
    virtual MainMenuFrame *CreateMainMenuFrame() = 0;
};
Q_DECLARE_INTERFACE(IFrameFactory,"Frame factory /1.0")
