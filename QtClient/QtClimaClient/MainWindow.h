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

    void setTitle(const QString &title);
    void setStatus(const QString &status);

    QFrame *getMainFrame();

private slots:
    void on_pushButton_clicked();

private:
    Ui::MainWindow *ui;
};
