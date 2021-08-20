#include "SelectGraphFrame.h"
#include "ui_SelectGraphFrame.h"

#include <ApplicationWorker.h>

SelectGraphFrame::SelectGraphFrame(QList<GraphInfo> infos, QWidget *parent) :
    FrameBase(parent),
    ui(new Ui::SelectGraphFrame)
{
    ui->setupUi(this);
    setTitle("Выбор графика");


    m_graphInfosModel = new SelectGraphModel(infos);

    ui->listView->setModel(m_graphInfosModel);
}

SelectGraphFrame::~SelectGraphFrame()
{
    delete ui;
}

QString SelectGraphFrame::getFrameName()
{
    return "SelectGraphFrame";
}

void SelectGraphFrame::TempInfosResponse(QList<GraphInfo> *infos)
{

}
