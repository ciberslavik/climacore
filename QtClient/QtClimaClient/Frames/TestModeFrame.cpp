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

    ProfileInfo testProfile1;
    testProfile1.Key= "Key0";
    testProfile1.Name = "Тестовый профиль 1";

    ProfileInfo testProfile2;
    testProfile2.Key= "Key1";
    testProfile2.Name = "Тестовый профиль 2";

    ProfileInfo testProfile3;
    testProfile3.Key= "Key2";
    testProfile3.Name = "Тестовый профиль 3";

    testData.append(testProfile1);
    testData.append(testProfile2);
    testData.append(testProfile3);
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

}

void TestModeFrame::showEvent(QShowEvent *event)
{

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

