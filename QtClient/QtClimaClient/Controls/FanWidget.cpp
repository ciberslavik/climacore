#include "FanWidget.h"

#include <QPainter>
#include <QPicture>

FanWidget::FanWidget(QWidget *parent) : QWidget(parent)
{
    m_fanMovie = new QMovie(":/Images/Fan.gif");
    m_fanLabel = new QLabel(this);
    m_modeLabel = new QClickableLabel(this);
    m_modeEditor = new FanModeSwitch();
    m_modeEditor->setVisible(false);

    //label->setText("Test");
    m_fanLabel->setMovie(m_fanMovie);
    //m_fanMovie->start();
    m_manualPixmap = QPixmap(":Images/Industry-Manual-icon.png");
    m_autoPixmap = QPixmap(":Images/Industry-Automatic-icon.png");
    m_discardPixmap = QPixmap(":Images/cancel-icon.png");
    m_alertPixmap=QPixmap(":Images/alert-icon.png").scaled(32, 32);

    m_fanMode = FanMode::Auto;
    m_fanState = FanStateEnum::Disabled;

    connect(m_modeLabel, &QClickableLabel::labelClicked, this, &FanWidget::onModeLabelClicked);
    connect(m_modeEditor, &FanModeSwitch::acceptMode, this, &FanWidget::onModeEditorAccept);
    rebuildUI();
}

void FanWidget::setFanState(FanStateEnum state)
{
    if(m_fanState != state)
    {
        m_fanState = state;
        rebuildUI();
        update();
    }
}

void FanWidget::setMode(FanMode mode)
{
    if(m_fanMode != mode)
    {
        m_fanMode = mode;

        rebuildUI();
        update();
    }
}

FanStateEnum FanWidget::fanState()
{
    return m_fanState;
}

void FanWidget::paintEvent(QPaintEvent *event)
{


    QPainter painter(this);


    painter.setPen(m_borderPen);
    painter.drawRoundRect(m_borderRect, 10, 10);

    painter.setPen(m_statePen);
    painter.setBrush(m_stateBrush);
    painter.drawEllipse(m_stateRect);

}

void FanWidget::resizeEvent(QResizeEvent *event)
{
    m_borderRect.setX(0);
    m_borderRect.setY(0);
    m_borderRect.setWidth(event->size().width()-1);
    m_borderRect.setHeight(event->size().height()-1);

    m_stateRect.setX(m_borderRect.width() - 30);
    m_stateRect.setY(10);
    m_stateRect.setWidth(20);
    m_stateRect.setHeight(20);

    m_modeRect.setX(3);
    m_modeRect.setY(3);
    m_modeRect.setWidth(32);
    m_modeRect.setHeight(32);
    m_modeLabel->setGeometry(m_modeRect);

    QRect r;
    r.setX(5);
    r.setY(40);
    r.setWidth(event->size().width()-10);
    r.setHeight(event->size().height()-42);
    m_fanLabel->setGeometry(r);
    m_fanMovie->setScaledSize(r.size());
}

void FanWidget::onModeLabelClicked()
{
    //m_modeEditor->setVisible(true);

    m_modeEditor->setFanMode(m_fanMode);
    m_modeEditor->exec();
}

void FanWidget::onModeEditorAccept(FanMode mode)
{
    if(m_fanMode != mode)
    {
        setMode(mode);
    }
    m_modeEditor->setVisible(false);
    m_modeEditor->close();
}

void FanWidget::onModeEditorCancel()
{

}

void FanWidget::rebuildUI()
{
    switch(m_fanMode)
    {
        case FanMode::Auto:
            m_modeLabel->setPixmap(m_autoPixmap);
        break;
        case FanMode::Manual:
            m_modeLabel->setPixmap(m_manualPixmap);
        break;
        case FanMode::Disabled:
            m_modeLabel->setPixmap(m_discardPixmap);
        break;
    }

    if(m_fanMode != FanMode::Disabled)
    {
        switch (m_fanState)
        {
            case FanStateEnum::Enabled:
                m_stateBrush = QBrush(Qt::green);
                m_fanMovie->start();
            break;
            case FanStateEnum::Disabled:
                m_stateBrush = QBrush(Qt::GlobalColor::darkGreen);
                m_fanMovie->stop();
            break;
            case FanStateEnum::Alarm:
                m_stateBrush = QBrush(Qt::red);
                m_modeLabel->setPixmap(m_alertPixmap);
                m_fanMovie->stop();
            break;
        }
    }
    else
    {
        m_stateBrush = QBrush(Qt::gray);
    }
}
