#pragma once

#include <QDialog>
#include <QDateTime>

#include <Controls/qclickablelineedit.h>
#include <Frames/Dialogs/inputdigitdialog.h>
#include <Services/FrameManager.h>


namespace Ui {
class LivestockOperationDialog;
}

class LivestockOperationDialog : public QDialog
{
    Q_OBJECT

public:
    explicit LivestockOperationDialog(QWidget *parent = nullptr);
    ~LivestockOperationDialog();
    int Heads()const {return m_heads;}
    void setHeads(int heads);

    QDateTime OperationDate()const{return m_opDate;}
    void setOperationDate(QDateTime opDate);

    void setTitle(const QString &title);
private slots:
    void on_btnAccept_clicked();
    void on_txtHeads_clicked();

    void on_txtDay_clicked();
    void on_txtMonth_clicked();
    void on_txtYear_clicked();

    void on_btnReject_clicked();


private:
    Ui::LivestockOperationDialog *ui;
    int m_heads;
    QDateTime m_opDate;

    void showDigitDialog(QLineEdit *txt, const QString &title);
};

