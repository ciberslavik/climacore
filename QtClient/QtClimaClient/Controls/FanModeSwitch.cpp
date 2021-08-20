#include "FanModeSwitch.h"
#include "ui_FanModeSwitch.h"

#include <QPainter>

FanModeSwitch::FanModeSwitch(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::FanModeSwitch)
{
    ui->setupUi(this);
}

FanModeSwitch::~FanModeSwitch()
{
    delete ui;
}

void FanModeSwitch::setFanMode(FanMode mode)
{
    if(m_mode != mode)
    {
        m_mode = mode;
        checkButton();
    }
}

FanMode FanModeSwitch::fanMode()
{
    return m_mode;
}

void FanModeSwitch::on_btnDisable_clicked()
{
    if(m_mode!=FanMode::Disabled)
    {
        m_mode = FanMode::Disabled;
        checkButton();
    }
}

void FanModeSwitch::on_btnManual_clicked()
{
    if(m_mode != FanMode::Manual)
    {
        m_mode = FanMode::Manual;
        checkButton();
    }
}

void FanModeSwitch::on_btnAuto_clicked()
{
    if(m_mode != FanMode::Auto)
    {
        m_mode = FanMode::Auto;
        checkButton();
    }
}

void FanModeSwitch::on_btnAccept_clicked()
{
    emit acceptMode(m_mode);
}

void FanModeSwitch::on_btnCancel_clicked()
{
    emit cancelEdit();
}

void FanModeSwitch::paintEvent(QPaintEvent *event)
{
    QWidget::paintEvent(event);
    QRect borderRect(QPoint(0, 0), QSize(this->size().width() - 2, this->size().height() - 2));
    QPen borderPen(Qt::black);
    QBrush backgrountBrush(Qt::gray);


    QPainter pa(this);

    pa.setPen(borderPen);
    pa.drawRect(borderRect);
    pa.fillRect(borderRect, backgrountBrush);
}

void FanModeSwitch::checkButton()
{
    switch (m_mode) {
        case FanMode::Auto:
            ui->btnAuto->setChecked(true);
        break;
        case FanMode::Manual:
            ui->btnManual->setChecked(true);
            if(m_fanState)
            {
                ui->btnOn->setChecked(true);
            }
            else
            {
                ui->btnOff->setChecked(true);
            }
        break;
        case FanMode::Disabled:
            ui->btnDisable->setChecked(true);
        break;
    }
    if((m_mode == FanMode::Auto) || (m_mode == FanMode::Disabled))
    {
        ui->grpState->setVisible(false);
        QSize mSize = size();
        mSize.setWidth(188);
        setGeometry(QRect(pos(),mSize));
    }
    else if(m_mode == FanMode::Manual)
    {
        ui->grpState->setVisible(true);
        QSize mSize = size();
        mSize.setWidth(298);
        setGeometry(QRect(pos(),mSize));
    }
}

void FanModeSwitch::on_btnOn_clicked()
{
    if(!m_fanState)
    {
        m_fanState = true;
        emit fanStateChanged(m_fanState);
    }
}

void FanModeSwitch::on_btnOff_clicked()
{
    if(m_fanState)
    {
        m_fanState = false;
        emit fanStateChanged(m_fanState);
    }
}
