#pragma once

#include <QWidget>
#include "Models/Timers/LightTimerProfile.h"
#include <QMouseEvent>

class TimerPresenter : public QWidget
{
    Q_OBJECT
public:
    explicit TimerPresenter(QWidget *parent = nullptr);

    void setTimerProfile(const LightTimerProfile &profile);

    void paintEvent(QPaintEvent *event) override;
    void resizeEvent(QResizeEvent *event) override;
signals:

private:
    LightTimerProfile m_profile;
    QRect m_borderRect;
    QRect m_graphRect;

    void drawLeftBar(QPainter *painter);
    void drawTopBar(QPainter *painter);
    void drawTimer(QPainter *painter, const LightTimerDay &timers, const QRect &rect);
    void calcSize();
    const double MinsPerDay;
    double m_tickHeight;
    double m_columnWidth;
    //geometry

    int _leftLegendSize;
    int _topLegendSize;

};

