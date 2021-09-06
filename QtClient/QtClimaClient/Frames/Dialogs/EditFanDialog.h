#pragma once

#include <QDialog>
#include <Network/GenericServices/DeviceProviderService.h>
#include "Models/FanInfo.h"

namespace Ui {
class EditFanDialog;
}

class EditFanDialog : public QDialog
{
    Q_OBJECT

public:
    explicit EditFanDialog(QWidget *parent = nullptr);
    ~EditFanDialog();
    FanInfo *getInfo();
    void setInfo(FanInfo *info);
private slots:
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();
    void onTxtDigitClicked();
    void onTxtPerfOrCont();

    void onTxtTextClicked();
    void onRelayListReceived(QList<RelayInfo> relayList);
private:
    Ui::EditFanDialog *ui;
    FanInfo *m_fanInfo;
    DeviceProviderService *m_devProvider;
};

