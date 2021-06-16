#include "FrameManager.h"

FrameManager::FrameManager(CMainWindow *mainWindow, QObject *parent) : QObject(parent)
{
    m_MainWindow = mainWindow;
}



CMainWindow *FrameManager::MainWindow() const
{
    return m_MainWindow;
}

void FrameManager::setMainWindow(CMainWindow *newMainWindow)
{
    m_MainWindow = newMainWindow;
}

void FrameManager::RegisterFrame(FrameBase *frame)
{

}

void FrameManager::setCurrentFrame(const QString &frameName)
{

}

void FrameManager::PreviousFrame()
{

}


