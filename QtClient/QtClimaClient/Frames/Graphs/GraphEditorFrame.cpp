#include "GraphEditorFrame.h"
#include "ui_GraphEditorFrame.h"

GraphEditorFrame::GraphEditorFrame(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::GraphEditorFrame)
{
    ui->setupUi(this);
}

GraphEditorFrame::~GraphEditorFrame()
{
    delete ui;
}
