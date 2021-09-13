#pragma once

#include <QDialog>

namespace Ui {
class StartProductionDialog;
}

class StartProductionDialog : public QDialog
{
    Q_OBJECT

public:
    explicit StartProductionDialog(QWidget *parent = nullptr);
    ~StartProductionDialog();

    QDateTime getStartDate();
    QDateTime getPlendingDate();
    int getHeadsCount();
private slots:
    void onTxtClicked();
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

private:
    Ui::StartProductionDialog *ui;
};

