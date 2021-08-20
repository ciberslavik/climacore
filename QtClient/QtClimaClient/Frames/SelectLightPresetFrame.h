#pragma once

#include <QWidget>

namespace Ui {
    class SelectLightPresetFrame;
}

class SelectLightPresetFrame : public QWidget
{
    Q_OBJECT

public:
    explicit SelectLightPresetFrame(QWidget *parent = nullptr);
    ~SelectLightPresetFrame();

private:
    Ui::SelectLightPresetFrame *ui;
};

