#pragma once

#include <QDialog>

namespace Ui {
class EditFanDialog;
}

class EditFanDialog : public QDialog
{
    Q_OBJECT

public:
    explicit EditFanDialog(QWidget *parent = nullptr);
    ~EditFanDialog();

private slots:
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

private:
    Ui::EditFanDialog *ui;
};

