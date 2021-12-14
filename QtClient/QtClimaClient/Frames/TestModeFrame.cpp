#include "TestModeFrame.h"
#include "ui_TestModeFrame.h"

#include <Models/Graphs/ProfileInfo.h>

#include <Frames/Graphs/SelectProfileFrame.h>
#include <Services/FrameManager.h>

TestModeFrame::TestModeFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::TestModeFrame)
{
    ui->setupUi(this);
}

TestModeFrame::~TestModeFrame()
{
    delete ui;
}

void TestModeFrame::on_btnSelectProfileTest_clicked()
{

}

void TestModeFrame::closeEvent(QCloseEvent *event)
{
    Q_UNUSED(event)
}

void TestModeFrame::showEvent(QShowEvent *event)
{
    Q_UNUSED(event)
}


void TestModeFrame::on_btnSelectGraphTest_clicked()
{
    SelectProfileFrame *graphFrame = new SelectProfileFrame(ProfileType::Temperature);

    FrameManager::instance()->setCurrentFrame(graphFrame);
}


void TestModeFrame::on_pushButton_clicked()
{
    SelectProfileFrame *graphFrame = new SelectProfileFrame(ProfileType::Ventilation);

    FrameManager::instance()->setCurrentFrame(graphFrame);
}

