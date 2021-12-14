#include "TimerPresenter.h"

#include <QPainter>
#include <QStaticText>

TimerPresenter::TimerPresenter(QWidget *parent) : QWidget(parent),
    MinsPerDay(24 * 60)
{
    _leftLegendSize = 55;
    _topLegendSize = 20;
}

void TimerPresenter::setTimerProfile(const LightTimerProfile &profile)
{
    m_profile = profile;

//    qSort(m_timers.Timers.begin(),m_timers.Timers.end(),
//          [](const TimerInfo &a, const TimerInfo &b) -> bool { return a.OnTime < b.OnTime; });
    update();
}

void TimerPresenter::paintEvent(QPaintEvent *event)
{
    Q_UNUSED(event);

    QPainter painter(this);

    painter.drawRect(m_borderRect);
    painter.drawRect(m_graphRect);
    drawLeftBar(&painter);
    drawTopBar(&painter);


}

void TimerPresenter::resizeEvent(QResizeEvent *event)
{

    m_borderRect.setX(0);
    m_borderRect.setY(0);
    m_borderRect.setWidth(event->size().width() - 1);
    m_borderRect.setHeight(event->size().height() - 1);

    m_graphRect.setX(_leftLegendSize);
    m_graphRect.setY(_topLegendSize);
    m_graphRect.setWidth(event->size().width() - (_leftLegendSize +3));
    m_graphRect.setHeight(event->size().height() - (_topLegendSize +3));

    m_tickHeight = m_graphRect.height() / MinsPerDay;

    if(m_profile.Days.count()>0)
        m_columnWidth = m_graphRect.width() / m_profile.Days.count();
    else
        m_columnWidth = 10;

    update();
}

void TimerPresenter::drawLeftBar(QPainter *painter)
{
    for(int i = 0; i < MinsPerDay; i++)
    {
        double coord = m_graphRect.y() + (i * m_tickHeight);
        if(i % 10 == 0)
        {
            painter->drawLine(m_graphRect.x() - 5, coord, m_graphRect.x(), coord);
        }
        if(i % 60 == 0)
        {
            int hour = i / 60;
            QString lbl;
            if(hour < 10)
            {
                lbl = "0" + QString::number(hour) + ":00";
            }
            else
            {
                lbl = QString::number(hour) + ":00";
            }

            QStaticText txt(lbl);
            txt.setTextWidth(8);
            painter->drawLine(m_graphRect.x() - 10, coord, m_graphRect.x(), coord);

            coord = coord - (txt.size().height() / 2);
            painter->drawStaticText(2,coord, txt);
        }
    }
}

void TimerPresenter::drawTopBar(QPainter *painter)
{

    double columnLeft = m_graphRect.x();

    foreach (const LightTimerDay &tList, m_profile.Days)
    {
        QStaticText txt(QString::number(tList.DayNumber));
        painter->drawStaticText(columnLeft +(m_columnWidth/2), 2, txt);

        QRect timerRect(columnLeft,m_graphRect.top(),m_columnWidth,m_graphRect.height());

        painter->drawRect(timerRect);
        drawTimer(painter, tList, timerRect);



        columnLeft = columnLeft + m_columnWidth;
    }
}

void TimerPresenter::drawTimer(QPainter *painter, const LightTimerDay &timers, const QRect &rect)
{

        qDebug() << rect;
        foreach(const LightTimerItem &timer, timers.Timers)
        {
            double onMinute = ((timer.OnTime.time().msecsSinceStartOfDay() / 1000) / 60);
            double offMinute = ((timer.OffTime.time().msecsSinceStartOfDay() / 1000) / 60);
            double workTime = offMinute - onMinute;

            double y = rect.top() + m_tickHeight * onMinute;
            double h = m_tickHeight * workTime;

            QRect tRect(rect.left(), y, rect.width(), h);

            qDebug() << tRect;
            QBrush brush = painter->brush();
            painter->setBrush(Qt::green);
            painter->drawRect(tRect);
            painter->setBrush(brush);

        }
}
