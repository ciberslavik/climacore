#ifndef MESSAGEDIALOG_H
#define MESSAGEDIALOG_H

#include <QDialog>

namespace Ui {
class MessageDialog;
}





class MessageDialog : public QDialog
{
    Q_OBJECT

public:
    enum DialogType
    {
        OkDialog,
        OkCancelDialog,
        YesNoDialog
    };


    explicit MessageDialog(QWidget *parent = nullptr);
    explicit MessageDialog(const QString &title="",const QString &message="", const DialogType dialogType = DialogType::OkDialog, QWidget *parent = nullptr);
    ~MessageDialog();
    void setTitle(const QString &title);
    void setMessage(const QString &message);
    void setDialogType(const DialogType &dialogType);
private slots:
    void on_btnOK_clicked();

    void on_btnCancel_clicked();

private:
    Ui::MessageDialog *ui;
};

#endif // MESSAGEDIALOG_H
