#include "DefaultFrameFactory.h"
#include <QMetaObject>

DefaultFrameFactory::DefaultFrameFactory()
{

}

SystemStateFrame *DefaultFrameFactory::CreateSystemStateFrame()
{
    return nullptr;
}

MainMenuFrame *DefaultFrameFactory::CreateMainMenuFrame()
{
    return nullptr;
}
