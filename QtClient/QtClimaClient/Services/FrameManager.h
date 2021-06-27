#pragma once

#include <MainWindow.h>
#include <QObject>
#include <QStack>

#include <Frames/FrameBase.h>
#include "FrameName.h"

class FrameManager : public QObject
{
    Q_OBJECT

    CMainWindow *m_MainWindow;
    QFrame *m_MainFrame;
    FrameBase *m_CurrentFrame;

    QStack<FrameBase*> *m_FrameHistory;
public:

    CMainWindow *MainWindow() const;

    void setCurrentFrame(const FrameName &frame);
    void PreviousFrame();
signals:

protected:
    explicit FrameManager(CMainWindow *mainWindow, QObject *parent = nullptr);
private:
    static FrameManager *_instnce;

};

