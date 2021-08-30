#pragma once

#include <QWidget>

namespace Ui {
class ProductionFrame;
}

class ProductionFrame : public QWidget
{
    Q_OBJECT

public:
    explicit ProductionFrame(QWidget *parent = nullptr);
    ~ProductionFrame();

private:
    Ui::ProductionFrame *ui;
};

