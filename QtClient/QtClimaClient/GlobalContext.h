#pragma once

#include <QObject>

class GlobalContext : public QObject
{
    Q_OBJECT
public:
    explicit GlobalContext(QObject *parent = nullptr);

signals:

};

