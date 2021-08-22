#pragma once

#include <MainWindow.h>
#include <QLayout>
#include <QVBoxLayout>
#include <QObject>
#include <QStack>
#include <QWidget>

#include <Frames/FrameBase.h>
#include "FrameName.h"

class FrameManager : public QObject
{
    Q_OBJECT

public:

    virtual ~FrameManager();
    FrameManager(FrameManager &other) = delete;
    void operator=(const FrameManager &) = delete;

    void Initialize(CMainWindow *mainWindow, QObject *parent = nullptr);
    CMainWindow *MainWindow() const;

    void setCurrentFrame(FrameBase *frame);
    FrameBase *CurrentFrame();
    void PreviousFrame();

    static FrameManager *instance()
    {
        if(_instnce == nullptr)
            _instnce = new FrameManager();

        return _instnce;
    }
signals:

protected:
    explicit FrameManager();
private:
    static FrameManager *_instnce;

    CMainWindow *m_MainWindow;
    QFrame *m_MainFrame;
    FrameBase *m_CurrentFrame;

    QStack<FrameBase*> m_FrameHistory;
    QVBoxLayout *frameLayout;
    bool m_isInitialized;
    void setFrame(FrameBase *frame);
};

