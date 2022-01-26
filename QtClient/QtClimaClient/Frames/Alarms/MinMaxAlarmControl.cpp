#include "MinMaxAlarmControl.h"
#include "ui_MinMaxAlarmControl.h"

#include <QPainter>
#include <QResizeEvent>

MinMaxAlarmControl::MinMaxAlarmControl(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::MinMaxAlarmControl)
{
    ui->setupUi(this);
}

MinMaxAlarmControl::~MinMaxAlarmControl()
{
    delete ui;
}


void MinMaxAlarmControl::paintEvent(QPaintEvent *event)
{
    Q_UNUSED(event)

    QPainter painter;

    painter.drawRect(m_borderRect);
}

void MinMaxAlarmControl::resizeEvent(QResizeEvent *event)
{
    m_borderRect.setX(0);
    m_borderRect.setY(0);
    m_borderRect.setWidth(event->size().width() -1);
    m_borderRect.setHeight(event->size().height() - 1);
}
