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
    void on_btnReturn_clicked();
    void on_btnEdit_clicked();

    void on_btnDown_clicked();
    void on_btnUp_clicked();


    void onFanInfoListReceived(QMap<QString, FanInfo> infos);
    void on_btnAccept_clicked();

    void onTimeout();
private:
    Ui::VentilationConfigFrame *ui;
    QMap<QString, FanInfo> m_infos;
    FanInfosModel *m_infosModel;
    VentilationService *m_ventService;
    QItemSelectionModel *m_selection;
    bool m_dataChanged;
    bool m_running;

    void selectRow(int index);
    void updateStatus(const QList<FanInfo> &infos);
    void updateInfo(const QList<FanInfo> &infos);



    enum EditorState
    {
        Initialize,
        Running,
        UpdateData
    };

    EditorState m_currentState;
    // QWidget interface
protected:
    void closeEvent(QCloseEvent *event)override;
    void showEvent(QShowEvent *event)override;

};

