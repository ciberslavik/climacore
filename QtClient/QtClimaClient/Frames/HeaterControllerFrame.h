#pragma once

#include "FrameBase.h"

#include <QWidget>

#include <Network/GenericServices/HeaterControllerService.h>

namespace Ui {
    class HeaterControllerFrame;
}

class HeaterControllerFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit HeaterControllerFrame(QWidget *parent = nullptr);
    ~HeaterControllerFrame();

private:
    Ui::HeaterControllerFrame *ui;
    HeaterControllerService *m_heaterService;
    bool m_heat1modify;
    bool m_heat2modify;

    QList<HeaterParams> m_heaters;

    QMetaObject::Connection timerConnection;
    // FrameBase interface
public:
    virtual QString getFrameName() override {return "HeaterControllerFrame";}
private slots:
    void on_btnReturn_clicked();
    void onHeaterStateListReceived(float setpoint, QList<HeaterState> heaterStates);
    void onHeaterParamsListReceived(QList<HeaterParams> heaterParams);
    void onHeaterUpdated(HeaterState state);
    void onTxtCorrection1Clicked();
    void onTxtCorrection2Clicked();
    void onTxtDeltaOn1Clicked();
    void onTxtDeltaOff1Clicked();
    void onTxtDeltaOn2Clicked();
    void onTxtDeltaOff2Clicked();

    void on_btnApply_clicked();

    void updateTimeout();
    // QWidget interface
protected:
    virtual void closeEvent(QCloseEvent *event) override;
    virtual void showEvent(QShowEvent *event) override;
};

