#include "FanWidget.h"

#include <QPainter>
#include <QPicture>

FanWidget::FanWidget(QWidget *parent) : QWidget(parent)
{
    m_fanMode = FanMode::Auto;
    m_fanState = FanStateEnum::Stopped;
    createUI();
    rebuildUI();
}

FanWidget::FanWidget(const QString &fanKey, QWidget *parent)
{
    m_fanKey = fanKey;

    createUI();
    rebuildUI();
}

FanWidget::~FanWidget()
{
    disconnect(m_modeLabel, &QClickableLabel::labelClicked, this, &FanWidget::onModeLabelClicked);
    disconnect(m_modeEditor, &FanModeSwitch::acceptMode, this, &FanWidget::onModeEditorAccept);
    disconnect(m_modeEditor, &FanModeSwitch::cancelEdit, this, &FanWidget::onModeEditorCancel);
    disconnect(m_modeEditor, &FanModeSwitch::fanModeChanged, this, &FanWidget::onModeEditorModeChanged);
    disconnect(m_modeEditor, &FanModeSwitch::fanStateChanged, this, &FanWidget::onModeEditorStateChanged);

}

void FanWidget::setFanState(FanStateEnum_t state)
{
    if(m_fanState != state)
    {
        m_fanState = state;
        rebuildUI();
        update();
    }
}

void FanWidget::setFanMode(FanMode mode)
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

QString FanWidget::FanKey()
{
    return m_fanKey;
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
    emit EditBegin(m_fanKey);
    m_modeEditor->setFanMode(m_fanMode);
    m_modeEditor->setFanState(m_fanState);
    m_modeEditor->setParent((QWidget*)parent());
    m_modeEditor->exec();

}

void FanWidget::onModeEditorAccept()
{
    setFanMode(m_modeEditor->fanMode());
    setFanState(m_modeEditor->fanState());
    m_modeEditor->setVisible(false);
    m_modeEditor->close();
    emit EditAccept(m_fanKey);
}

void FanWidget::onModeEditorCancel()
{
    m_modeEditor->setVisible(false);
    m_modeEditor->close();
    emit EditCancel(m_fanKey);
}

void FanWidget::onModeEditorModeChanged(FanMode mode)
{
    setFanMode(m_modeEditor->fanMode());
    emit FanModeChanged(m_fanKey, mode);
}

void FanWidget::onModeEditorStateChanged(FanStateEnum_t state)
{
    setFanState(m_modeEditor->fanState());
    emit FanStateChanged(m_fanKey, state);
}

void FanWidget::createUI()
{
    m_fanMovie = new QMovie(":/Images/Fan.gif");
    m_fanLabel = new QLabel(this);
    m_modeLabel = new QClickableLabel(this);
    m_modeEditor = new FanModeSwitch();
    m_modeEditor->setVisible(false);

    //label->setText("Test");
    m_fanLabel->setMovie(m_fanMovie);
    m_fanMovie->start();
    m_manualPixmap = QPixmap(":Images/Industry-Manual-icon.png");
    m_autoPixmap = QPixmap(":Images/Industry-Automatic-icon.png");
    m_discardPixmap = QPixmap(":Images/cancel-icon.png");
    m_alertPixmap=QPixmap(":Images/alert-icon.png").scaled(32, 32);


    connect(m_modeLabel, &QClickableLabel::labelClicked, this, &FanWidget::onModeLabelClicked);
    connect(m_modeEditor, &FanModeSwitch::acceptMode, this, &FanWidget::onModeEditorAccept);
    connect(m_modeEditor, &FanModeSwitch::cancelEdit, this, &FanWidget::onModeEditorCancel);
    connect(m_modeEditor, &FanModeSwitch::fanModeChanged, this, &FanWidget::onModeEditorModeChanged);
    connect(m_modeEditor, &FanModeSwitch::fanStateChanged, this, &FanWidget::onModeEditorStateChanged);
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
            case FanStateEnum::Running:
                m_stateBrush = QBrush(Qt::green);
                m_fanMovie->start();
            break;
            case FanStateEnum::Stopped:
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
