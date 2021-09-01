#pragma once

#include <QDialog>

namespace Ui {
class EditFanDialog;
}

class EditFanDialog : public QDialog
{
    Q_OBJECT

public:
    explicit EditFanDialog(QWidget *parent = nullptr);
    ~EditFanDialog();

private:
    Ui::EditFanDialog *ui;
};

