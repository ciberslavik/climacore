#ifndef FANWIDGET_H
#define FANWIDGET_H

#include "QClickableLabel.h"
#include "Models/FanControlsEnums.h"
#include "FanModeSwitch.h"
#include <QLabel>
#include <QMouseEvent>
#include <QMovie>
#include <QPen>
#include <QPoint>
#include <QWidget>
#include <QApplication>

class FanWidget : public QWidget
{
    Q_OBJECT
public:

    explicit FanWidget(QWidget *parent = nullptr);
    void setFanState(FanStateEnum_t state);
    void setMode(FanMode_t mode);
    FanStateEnum fanState();
    void paintEvent(QPaintEvent *event) override;
    void resizeEvent(QResizeEvent *event) override;
signals:
private slots:
    void onModeLabelClicked();
    void onModeEditorAccept(FanMode mode);
    void onModeEditorCancel();
private:
    FanStateEnum m_fanState;
    FanMode m_fanMode;
    QMovie *m_fanMovie;

    QLabel *m_fanLabel;
    QClickableLabel *m_modeLabel;
    //Rects
    QRect m_borderRect;
    QRect m_stateRect;
    QRect m_modeRect;
    //Pixmaps
    QPixmap m_manualPixmap;
    QPixmap m_autoPixmap;
    QPixmap m_discardPixmap;
    QPixmap m_alertPixmap;

    //Brushes
    QBrush m_borderBrush;
    QBrush m_stateBrush;

    QPen m_borderPen;
    QPen m_statePen;
    FanModeSwitch *m_modeEditor;
    void rebuildUI();
};

#endif // FANWIDGET_H
