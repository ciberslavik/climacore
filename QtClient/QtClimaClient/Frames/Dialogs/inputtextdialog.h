#ifndef INPUTTEXTDIALOG_H
#define INPUTTEXTDIALOG_H

#include <QDialog>
#include <QLineEdit>
#include <QLabel>

#define NEXT_ROW_MARKER 0
namespace Ui {
class InputTextDialog;
}



class InputTextDialog : public QDialog
{
    Q_OBJECT

public:
    explicit InputTextDialog(QWidget *parent = nullptr);
    ~InputTextDialog();

    QString getText();
    void setText(QString text);
    void setTitle(const QString &title);
private slots:
    void buttonClicked(int key);
private:
    Ui::InputTextDialog *ui;

    void createKeyboard();

    QLineEdit *textbox;
    QLabel *lblTitle;
};

#endif // INPUTTEXTDIALOG_H
