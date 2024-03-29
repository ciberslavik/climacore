#ifndef INPUTDIGITDIALOG_H
#define INPUTDIGITDIALOG_H

#include <QDialog>
#include <QLineEdit>

namespace Ui {
class InputDigitDialog;
}

class InputDigitDialog : public QDialog
{
    Q_OBJECT

public:
    explicit InputDigitDialog(QLineEdit *edit, QWidget *parent = nullptr);
    void setTitle(const QString &title);
    ~InputDigitDialog();
private slots:
    void onDigitButtonPressed();
    void onDotButtonPressed();
    void onDeleteButtonPressed();
    void onOkButtonPressed();
    void onCancelButtonPressed();

    void on_pushButton_clicked();

private:
    Ui::InputDigitDialog *ui;
    QLineEdit *_editText;
    bool _first_key;
};

#endif // INPUTDIGITDIALOG_H
