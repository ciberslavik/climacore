#include "TempProfileEditorFrame.h"
#include "ui_TempProfileEditorFrame.h"

TempProfileEditorFrame::TempProfileEditorFrame(ValueByDayProfile *profile, QWidget *parent) :
    QWidget(parent),
    ui(new Ui::GraphEditorFrame)
{
    ui->setupUi(this);
    m_profile = profile;
}

TempProfileEditorFrame::~TempProfileEditorFrame()
{
    delete ui;
}
