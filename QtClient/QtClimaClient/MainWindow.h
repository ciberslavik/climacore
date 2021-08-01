#pragma once

#include <QFrame>
#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class CMainWindow : public QMainWindow
{
    Q_OBJECT

public:
    CMainWindow(QWidget *parent = nullptr);
    ~CMainWindow();

    void setFrameTitle(const QString &title);
    void setStatus(const QString &status);

    QFrame *getMainFrame();
public slots:
    void updateData();
private slots:
    void on_pushButton_clicked();

private:
    Ui::MainWindow *ui;

};
