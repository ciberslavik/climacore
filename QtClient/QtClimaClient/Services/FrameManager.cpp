#include "FrameManager.h"

FrameManager *FrameManager::_instnce = nullptr;

FrameManager::FrameManager(CMainWindow *mainWindow, QObject *parent) : QObject(parent)
{
    m_MainWindow = mainWindow;
    m_MainFrame = m_MainWindow->getMainFrame();
    frameLayout = new QVBoxLayout(m_MainFrame);
    frameLayout->setContentsMargins(0,0,0,0);
    _instnce = this;
}

FrameManager::~FrameManager()
{
    //delete frameLayout;
}

CMainWindow *FrameManager::MainWindow() const
{
    return m_MainWindow;
}

void FrameManager::setCurrentFrame(FrameBase *frame)
{
    if(m_CurrentFrame == nullptr)
    {
        m_CurrentFrame = frame;
    }

    if(m_CurrentFrame != frame)
    {
        frameLayout->removeWidget(m_CurrentFrame);
        m_FrameHistory.push(m_CurrentFrame);
        m_CurrentFrame = frame;
    }


    frameLayout->addWidget(m_CurrentFrame);
    m_MainWindow->setFrameTitle(m_CurrentFrame->Title());
    m_CurrentFrame->setParent(m_MainFrame);
    m_MainFrame->setLayout(frameLayout);
    m_CurrentFrame->show();
}


void FrameManager::PreviousFrame()
{

}


