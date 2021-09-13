#pragma once

#include <QDialog>
#include <QDateTime>
namespace Ui {
class StartPreparingDialog;
}

class StartPreparingDialog : public QDialog
{
    Q_OBJECT

public:
    explicit StartPreparingDialog(QWidget *parent = nullptr);
    ~StartPreparingDialog();

    float getTemperature();
    QDateTime getStartDate();
private slots:
    void onTxtClicked();
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

private:
    Ui::StartPreparingDialog *ui;
};

