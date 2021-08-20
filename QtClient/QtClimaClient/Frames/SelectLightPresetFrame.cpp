#include "SelectLightPresetFrame.h"
#include "ui_SelectLightPresetFrame.h"

SelectLightPresetFrame::SelectLightPresetFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::SelectLightPresetFrame)
{
    ui->setupUi(this);
}

SelectLightPresetFrame::~SelectLightPresetFrame()
{
    delete ui;
}
