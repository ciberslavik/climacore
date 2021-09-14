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
#include <Models/FanState.h>

class FanWidget : public QWidget
{
    Q_OBJECT
public:

    explicit FanWidget(QWidget *parent = nullptr);
    explicit FanWidget(const QString &fanKey, QWidget *parent = nullptr);

    ~FanWidget();
    void setFanState(FanStateEnum_t state);
    void setFanMode(FanMode_t mode);
    FanStateEnum fanState();
    FanMode_t fanMode(){return m_fanMode;}
    QString FanKey();
    void setIsAnalog(bool isanalog);
    bool isAnalog(){return m_isAnalog;}

    void paintEvent(QPaintEvent *event) override;
    void resizeEvent(QResizeEvent *event) override;
signals:
    void FanModeChanged(const QString &fanKey, FanMode_t newMode);
    void FanStateChanged(const QString &fanKey, FanStateEnum_t newState);
    void EditBegin(const QString &fanKey);
    void EditAccept(const QString &fanKey);
    void EditCancel(const QString &fanKey);
private slots:
    void onModeLabelClicked();
    void onModeEditorAccept();
    void onModeEditorCancel();
    void onModeEditorModeChanged(FanMode mode);
    void onModeEditorStateChanged(FanStateEnum_t state);
private:
    FanStateEnum_t m_fanState;
    QString m_fanKey;
    FanMode m_fanMode;
    QMovie *m_fanMovie;
    bool m_isAnalog;
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
    void createUI();
    void rebuildUI();
};

#endif // FANWIDGET_H
