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

    CMainWindow *m_MainWindow;
    QFrame *m_MainFrame;
    FrameBase *m_CurrentFrame;

    QStack<FrameBase*> m_FrameHistory;
    QVBoxLayout frameLayout;
public:
    explicit FrameManager(CMainWindow *mainWindow, QObject *parent = nullptr);

    FrameManager(FrameManager &other) = delete;
    void operator=(const FrameManager &) = delete;

    CMainWindow *MainWindow() const;

    void setCurrentFrame(FrameBase *frame);
    FrameBase *CurrentFrame();
    void PreviousFrame();

    static FrameManager *instance()
    {
        return _instnce;
    }
signals:


private:
    static FrameManager *_instnce;

};

