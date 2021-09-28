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

void FanModeSwitch::setTitle(const QString &title)
{
    ui->lblTitle->setText(title);
}

void FanModeSwitch::setFanMode(FanModeEnum mode)
{
    if(m_fanMode != mode)
    {
        m_fanMode = mode;
        switch (m_fanMode) {
        case FanModeEnum::Auto:
            ui->btnAuto->setChecked(true);
            break;
        case FanModeEnum::Manual:
            ui->btnManual->setChecked(true);
            break;
        case FanModeEnum::Disabled:
            ui->btnDisable->setChecked(true);
            break;
        }
    }
}

FanModeEnum FanModeSwitch::fanMode()
{
    return m_fanMode;
}

void FanModeSwitch::setFanState(FanStateEnum_t state)
{
    if(m_fanState != state)
    {
        m_fanState = state;
        switch (m_fanState) {
        case FanStateEnum::Running:
            ui->btnOn->setChecked(true);
            break;
        case FanStateEnum::Stopped:
            ui->btnOff->setChecked(true);
            break;
        case FanStateEnum::Alarm:
            break;
        }
    }
}

void FanModeSwitch::on_btnAccept_clicked()
{
    accept();
}

void FanModeSwitch::on_btnCancel_clicked()
{
    reject();
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


void FanModeSwitch::on_btnOn_clicked()
{
    if(m_fanState == FanStateEnum::Stopped)
    {
        m_fanState = FanStateEnum::Running;
        emit fanStateChanged(m_fanState);
    }
}

void FanModeSwitch::on_btnOff_clicked()
{
    if(m_fanState == FanStateEnum::Running)
    {
        m_fanState = FanStateEnum::Stopped;
        emit fanStateChanged(m_fanState);
    }
}

void FanModeSwitch::on_btnAuto_toggled(bool checked)
{
    if(checked)
    {
        m_fanMode = FanModeEnum::Auto;
        ui->grpState->setVisible(false);
        QSize mSize = size();
        mSize.setWidth(188);
        setGeometry(QRect(pos(),mSize));
        emit fanModeChanged(m_fanMode);
    }
}


void FanModeSwitch::on_btnManual_toggled(bool checked)
{
    if(checked)
    {
        m_fanMode = FanModeEnum::Manual;
        ui->grpState->setVisible(true);
        QSize mSize = size();
        mSize.setWidth(298);
        setGeometry(QRect(pos(),mSize));
        emit fanModeChanged(m_fanMode);
    }
}


void FanModeSwitch::on_btnDisable_toggled(bool checked)
{
    if(checked)
    {
        m_fanMode = FanModeEnum::Disabled;
        ui->grpState->setVisible(false);
        QSize mSize = size();
        mSize.setWidth(188);
        setGeometry(QRect(pos(),mSize));
        emit fanModeChanged(m_fanMode);
    }
}

