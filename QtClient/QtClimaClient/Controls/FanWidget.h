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
    explicit FanWidget(const QString &fanKey,const bool &isAnalog, QWidget *parent = nullptr);

    ~FanWidget();
    void setFanState(FanStateEnum_t state);
    void setFanMode(FanMode_t mode);
    FanStateEnum fanState();
    FanMode_t fanMode(){return m_fanMode;}
    QString FanKey();

    void setFanName(const QString &name);
    bool isAnalog(){return m_isAnalog;}
    void setAnalogValue(const float &analogPower);
    float getAnalogValue();

    void setAnalogMax(float max);
    void setAnalogMin(float min);


    void paintEvent(QPaintEvent *event) override;
    void resizeEvent(QResizeEvent *event) override;
signals:
    void FanModeChanged(const QString &fanKey, FanMode_t newMode);
    void FanStateChanged(const QString &fanKey, FanStateEnum_t newState);
    void FanAnalogValueChanged(const QString &fanKey, const float &value);

    void EditBegin(const QString &fanKey);
    void EditAccept(const QString &fanKey);
    void EditCancel(const QString &fanKey);
private slots:
    void onModeLabelClicked();
    void onModeEditorModeChanged(FanModeEnum mode);
    void onModeEditorStateChanged(FanStateEnum_t state);
    void onModeEditorValueChanged(const float &value);
private:
    FanStateEnum_t m_fanState;
    QString m_fanKey;
    QString m_fanName;
    FanModeEnum m_fanMode;
    QMovie *m_fanMovie;
    bool m_isAnalog;
    QLabel *m_fanLabel;
    QLabel *m_analogValueLabel;
    QLabel *m_nameLabel;
    QClickableLabel *m_modeLabel;
    float m_analogValue;

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

    float m_analogMax;
    float m_analogMin;


    void createUI();
    void rebuildUI();
};

#endif // FANWIDGET_H
