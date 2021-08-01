#include "MainWindow.h"
#include "ui_MainWindow.h"

#include <QTime>

CMainWindow::CMainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

CMainWindow::~CMainWindow()
{
    delete ui;
}

void CMainWindow::setFrameTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

QFrame *CMainWindow::getMainFrame()
{
    return ui->mainFrame;
}

void CMainWindow::updateData()
{
    QString clockStr = QTime::currentTime().toString("hh:mm");
    clockStr += " " + QDate::currentDate().toString("dd.MM.yyyy");
    ui->lblClock->setText(clockStr);
}


void CMainWindow::on_pushButton_clicked()
{

}

