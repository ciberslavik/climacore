#include "FrameManager.h"

FrameManager *FrameManager::_instnce = nullptr;

FrameManager::FrameManager()
    : QObject(nullptr),
      m_CurrentFrame(nullptr),
      m_isInitialized(false)
{

}

void FrameManager::Initialize(CMainWindow *mainWindow, QObject *parent)
{
    m_MainWindow = mainWindow;
    m_MainFrame = m_MainWindow->getMainFrame();
    frameLayout = new QVBoxLayout(m_MainFrame);
    frameLayout->setContentsMargins(0,0,0,0);

    setParent(parent);
    m_isInitialized = true;
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
    if(!m_isInitialized)
        return;

    if(m_CurrentFrame != nullptr)
    {
        m_CurrentFrame->close();
        frameLayout->removeWidget(m_CurrentFrame);
        m_FrameHistory.push(m_CurrentFrame);
    }

    setFrame(frame);
}


void FrameManager::PreviousFrame()
{
    if(!m_isInitialized)
        return;

    if(m_CurrentFrame != nullptr)
    {
        frameLayout->removeWidget(m_CurrentFrame);
        m_CurrentFrame->close();

        delete m_CurrentFrame;

        FrameBase *prevFrame = m_FrameHistory.pop();
        setFrame(prevFrame);
    }
}

void FrameManager::setFrame(FrameBase *frame)
{
    m_CurrentFrame = frame;

    frameLayout->addWidget(m_CurrentFrame);
    m_MainWindow->setFrameTitle(m_CurrentFrame->Title());
    m_CurrentFrame->setParent(m_MainFrame);
    m_MainFrame->setLayout(frameLayout);

    m_CurrentFrame->show();
}


