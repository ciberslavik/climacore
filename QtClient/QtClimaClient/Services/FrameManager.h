#pragma once

#include <MainWindow.h>
#include <QObject>

#include <Frames/FrameBase.h>

class FrameManager : public QObject
{
    Q_OBJECT
    Q_PROPERTY(CMainWindow* MainWindow READ MainWindow WRITE setMainWindow)


    CMainWindow *m_MainWindow;

public:
    explicit FrameManager(CMainWindow *mainWindow, QObject *parent = nullptr);



    CMainWindow *MainWindow() const;
    void setMainWindow(CMainWindow *newMainWindow);
    void RegisterFrame(FrameBase *frame);

    void setCurrentFrame(const QString &frameName);
    void PreviousFrame();
signals:

private:


};

