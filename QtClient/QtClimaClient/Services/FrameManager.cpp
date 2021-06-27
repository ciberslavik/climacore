#include "FrameManager.h"

FrameManager::FrameManager(CMainWindow *mainWindow, QObject *parent) : QObject(parent)
{
    m_MainWindow = mainWindow;
    m_MainFrame = m_MainWindow->getMainFrame();
}



CMainWindow *FrameManager::MainWindow() const
{
    return m_MainWindow;
}

void FrameManager::setCurrentFrame(const FrameName &frameName)
{
    FrameBase *frame;

    switch (frameName) {
        case FrameName::SystemState:
        break;
    case FrameName::MainMenu:
        break;
    }
}

void FrameManager::PreviousFrame()
{

}


