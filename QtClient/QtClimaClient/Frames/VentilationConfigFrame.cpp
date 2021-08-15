#include "SelectGraphFrame.h"
#include "VentilationConfigFrame.h"
#include "ui_VentilationConfigFrame.h"

VentilationConfigFrame::VentilationConfigFrame(QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::VentilationConfigFrame)
{
    ui->setupUi(this);
    setTitle("Настройка вентиляции");
}

VentilationConfigFrame::~VentilationConfigFrame()
{
    delete ui;
}

QString VentilationConfigFrame::getFrameName()
{
    return "VentilatilationConfigFrame";
}

void VentilationConfigFrame::on_btnSelectGraph_clicked()
{
   // SelectGraphFrame *selectFrame = new SelectGraphFrame();
    //FrameManager::instance()->setCurrentFrame(selectFrame);
}

