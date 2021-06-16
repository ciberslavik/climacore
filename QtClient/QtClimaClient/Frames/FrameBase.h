#pragma once

#include <QWidget>

class FrameBase : public QWidget
{
    Q_OBJECT
public:
    explicit FrameBase(QWidget *parent = nullptr);
    virtual QString getFrameName() = 0;
signals:

};

