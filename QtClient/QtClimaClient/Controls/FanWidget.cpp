#include "FanWidget.h"

#include <QPainter>
#include <QPicture>

FanWidget::FanWidget(QWidget *parent) : QWidget(parent)
{
    m_fanMode = FanModeEnum::Auto;
    m_fanState = FanStateEnum::Stopped;
    createUI();
    rebuildUI();
}

FanWidget::FanWidget(const QString &fanKey, const bool &isAnalog, QWidget *parent) : QWidget(parent)
{
    m_isAnalog = isAnalog;
    m_fanKey = fanKey;

    createUI();
    rebuildUI();
}

FanWidget::~FanWidget()
{
    disconnect(m_modeLabel, &QClickableLabel::labelClicked, this, &FanWidget::onModeLabelClicked);
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

void FanWidget::setFanMode(FanModeEnum mode)
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

void FanWidget::setAnalogValue(const float &analogPower)
{
    if(m_isAnalog)
    {
        m_analogValue = analogPower;
        m_analogValueLabel->setText(QString::number(m_analogValue, 'f', 1));
    }
}

float FanWidget::getAnalogValue()
{
    return m_analogValue;
}


void FanWidget::paintEvent(QPaintEvent *event)
{
    Q_UNUSED(event)

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

    if(m_isAnalog)
    {
        QRect anLblRect;
        anLblRect.setX(m_modeRect.x()+m_modeRect.width()+2);
        anLblRect.setY(3);
        anLblRect.setWidth(38);
        anLblRect.setHeight(15);
        m_analogValueLabel->setGeometry(anLblRect);
    }

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
    if(!m_isAnalog)
    {
        FanModeSwitch modeEditor(this);
        connect(&modeEditor, &FanModeSwitch::fanModeChanged, this, &FanWidget::onModeEditorModeChanged);
        connect(&modeEditor, &FanModeSwitch::fanStateChanged, this, &FanWidget::onModeEditorStateChanged);

        FanModeEnum oldMode = m_fanMode;
        FanStateEnum oldState = m_fanState;

        modeEditor.setFanMode(m_fanMode);
        modeEditor.setFanState(m_fanState);
        modeEditor.setParent((QWidget*)parent());

        if(modeEditor.exec() == QDialog::Rejected)
        {
            setFanMode(oldMode);
            emit FanModeChanged(m_fanKey, oldMode);
            setFanState(oldState);
            emit FanStateChanged(m_fanKey, oldState);
            emit EditCancel(m_fanKey);
        }
        else
        {
            emit EditAccept(m_fanKey);
        }

        disconnect(&modeEditor, &FanModeSwitch::fanModeChanged, this, &FanWidget::onModeEditorModeChanged);
        disconnect(&modeEditor, &FanModeSwitch::fanStateChanged, this, &FanWidget::onModeEditorStateChanged);
    }
}


void FanWidget::onModeEditorModeChanged(FanModeEnum mode)
{
    setFanMode(mode);
    emit FanModeChanged(m_fanKey, mode);
}

void FanWidget::onModeEditorStateChanged(FanStateEnum_t state)
{
    setFanState(state);
    emit FanStateChanged(m_fanKey, state);
}

void FanWidget::createUI()
{
    m_fanMovie = new QMovie(":/Images/Fan.gif");
    m_fanLabel = new QLabel(this);
    m_modeLabel = new QClickableLabel(this);

    if(m_isAnalog)
    {
        m_analogValueLabel = new QLabel(this);
        m_analogValueLabel->setText("100");
    }

    //label->setText("Test");
    m_fanLabel->setMovie(m_fanMovie);
    m_fanMovie->start();
    m_manualPixmap = QPixmap(":Images/Industry-Manual-icon.png");
    m_autoPixmap = QPixmap(":Images/Industry-Automatic-icon.png");
    m_discardPixmap = QPixmap(":Images/cancel-icon.png");
    m_alertPixmap=QPixmap(":Images/alert-icon.png").scaled(32, 32);


    connect(m_modeLabel, &QClickableLabel::labelClicked, this, &FanWidget::onModeLabelClicked);
}

void FanWidget::rebuildUI()
{
    switch(m_fanMode)
    {
    case FanModeEnum::Auto:
        m_modeLabel->setPixmap(m_autoPixmap);
        break;
    case FanModeEnum::Manual:
        m_modeLabel->setPixmap(m_manualPixmap);
        break;
    case FanModeEnum::Disabled:
        m_modeLabel->setPixmap(m_discardPixmap);
        break;
    }

    if(m_fanMode != FanModeEnum::Disabled)
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

    if(m_isAnalog)
        m_analogValueLabel->setText(QString::number(m_analogValue, 'f', 1));
}
