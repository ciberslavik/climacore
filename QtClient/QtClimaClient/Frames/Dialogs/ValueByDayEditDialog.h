#pragma once

#include <QDialog>
#include <Frames/Dialogs/inputdigitdialog.h>
#include <Services/FrameManager.h>
#include <Models/Graphs/ValueByDayPoint.h>

namespace Ui {
class ValueByDayEditDialog;
}

class ValueByDayEditDialog : public QDialog
{
    Q_OBJECT

public:
    explicit ValueByDayEditDialog(QWidget *parent = nullptr);
    ~ValueByDayEditDialog();

    void setValue(ValueByDayPoint *value);
    ValueByDayPoint getValue();

    void setTitle(const QString &title);
    void setValueName(const QString &valueName);
private slots:
    void on_btnAccept_clicked();
    void on_txtEdit_clicked();
    void on_btnCancel_clicked();

private:
    Ui::ValueByDayEditDialog *ui;

    ValueByDayPoint *m_value;
};

