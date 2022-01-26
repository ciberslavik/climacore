#pragma once

#include <QDialog>

#include <Models/Timers/LightTimerItem.h>

namespace Ui {
    class EditTimerDialog;
}

class EditTimerDialog : public QDialog
{
    Q_OBJECT

public:
    explicit EditTimerDialog(QWidget *parent = nullptr);
    ~EditTimerDialog();
    void setTimerInfo(const LightTimerItem &info);
    void setTitle(const QString &title);
    QDateTime onTime() const;
    QDateTime offTime() const;
private slots:
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

private:
    Ui::EditTimerDialog *ui;
    QDateTime m_onTime;
    QDateTime m_offTime;
};

