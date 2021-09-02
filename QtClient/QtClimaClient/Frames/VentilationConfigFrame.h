#pragma once

#include "FrameBase.h"

#include <QItemSelectionModel>
#include <QWidget>
#include <Services/FrameManager.h>
#include <Models/FanInfosModel.h>
#include <Network/GenericServices/VentilationService.h>

namespace Ui {
class VentilationConfigFrame;
}

class VentilationConfigFrame : public FrameBase
{
    Q_OBJECT

public:
    explicit VentilationConfigFrame(QWidget *parent = nullptr);
    ~VentilationConfigFrame();
    QString getFrameName() override;

private slots:
    void on_btnSelectGraph_clicked();
    void on_btnReturn_clicked();
    void on_btnEdit_clicked();
    void on_btnAdd_clicked();
    void on_btnDelete_clicked();
    void on_btnCancel_clicked();
    void on_btnDown_clicked();
    void on_btnUp_clicked();

    void onFanInfoListReceived(QList<FanInfo> infos);
private:
    Ui::VentilationConfigFrame *ui;
    FanInfosModel *m_infosModel;
    VentilationService *m_ventService;
    QItemSelectionModel *m_selection;



    // QWidget interface
protected:
    void closeEvent(QCloseEvent *event) override;
    void showEvent(QShowEvent *event) override;
};

