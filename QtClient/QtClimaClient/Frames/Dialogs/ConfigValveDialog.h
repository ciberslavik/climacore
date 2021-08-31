#pragma once

#include <QDialog>

namespace Ui {
class ConfigValveDialog;
}

class ConfigValveDialog : public QDialog
{
    Q_OBJECT

public:
    explicit ConfigValveDialog(QWidget *parent = nullptr);
    ~ConfigValveDialog();

private:
    Ui::ConfigValveDialog *ui;
};

