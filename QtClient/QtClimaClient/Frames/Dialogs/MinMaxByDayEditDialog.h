#pragma once

#include <QDialog>
#include <Frames/Dialogs/inputdigitdialog.h>
#include <Services/FrameManager.h>
#include <Models/Graphs/MinMaxByDayPoint.h>

namespace Ui {
class MinMaxByDayEditDialog;
}

class MinMaxByDayEditDialog : public QDialog
{
    Q_OBJECT

public:
    explicit MinMaxByDayEditDialog(QWidget *parent = nullptr);
    ~MinMaxByDayEditDialog();

      void setValue(MinMaxByDayPoint *value);
      MinMaxByDayPoint *getValue();

      void setTitle(const QString &title);
      void setMaxValueName(const QString &maxValueName);
      void setMinValueName(const QString &minValueName);

private slots:
    void on_btnAccept_clicked();
    void on_txtEdit_clicked();
    void on_btnCancel_clicked();

private:
    Ui::MinMaxByDayEditDialog *ui;

    MinMaxByDayPoint *m_value;
};

