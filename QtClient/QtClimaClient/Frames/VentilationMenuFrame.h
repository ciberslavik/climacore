#ifndef VENTILATIONMENUFRAME_H
#define VENTILATIONMENUFRAME_H

#include <QWidget>

namespace Ui {
class VentilationMenuFrame;
}

class VentilationMenuFrame : public QWidget
{
    Q_OBJECT

public:
    explicit VentilationMenuFrame(QWidget *parent = nullptr);
    ~VentilationMenuFrame();

private:
    Ui::VentilationMenuFrame *ui;
};

#endif // VENTILATIONMENUFRAME_H
