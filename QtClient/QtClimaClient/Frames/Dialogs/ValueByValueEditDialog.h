#pragma once

#include <QDialog>

namespace Ui {
class ValueByValueEditDialog;
}

class ValueByValueEditDialog : public QDialog
{
    Q_OBJECT

public:
    explicit ValueByValueEditDialog(QWidget *parent = nullptr);
    ~ValueByValueEditDialog();

    void setTitle(const QString &title);
    void setValueXTitle(const QString &title);
    void setValueYTitle(const QString &title);
    void setValueXSuffix(const QString &suffix);
    void setValueYSuffix(const QString &suffix);

    void setValueX(float valuex);
    float getValueX();

    void setValueY(float valuey);
    float getValueY();
private slots:
    void on_btnAccept_clicked();

    void on_btnCancel_clicked();

    void onTxtClicked();
private:
    Ui::ValueByValueEditDialog *ui;
};

