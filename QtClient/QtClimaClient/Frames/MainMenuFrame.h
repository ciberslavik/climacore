#pragma once

#include "FrameBase.h"

#include <QWidget>

namespace Ui {
class MainMenuFrame;
}

class MainMenuFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit MainMenuFrame(QWidget *parent = nullptr);
    ~MainMenuFrame();
    QString getFrameName() override
    {
        return "MainMenuFrame";
    }
private slots:
    void on_btnVentilationOverview_clicked();



    void on_btnReturn_clicked();

    void on_btnTemperature_clicked();


    void on_btnLightConfig_clicked();

    void on_btnProduction_clicked();

    void on_btnEditProfiles_clicked();

    void on_btnShowHystoryMenu_clicked();

    void on_btnHeaterConfig_clicked();

private:
    Ui::MainMenuFrame *ui;
};

