#include "MainWindow.h"
#include "ui_MainWindow.h"

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


void CMainWindow::on_pushButton_clicked()
{

}

