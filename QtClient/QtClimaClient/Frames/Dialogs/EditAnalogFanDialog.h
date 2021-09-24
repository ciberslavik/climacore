#pragma once

#include <QDialog>

namespace Ui {
    class EditAnalogFanDialog;
}

class EditAnalogFanDialog : public QDialog
{
    Q_OBJECT

public:
    explicit EditAnalogFanDialog(QWidget *parent = nullptr);
    ~EditAnalogFanDialog();

private slots:
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();
    void onTxtClicked();

private:
    Ui::EditAnalogFanDialog *ui;
};

