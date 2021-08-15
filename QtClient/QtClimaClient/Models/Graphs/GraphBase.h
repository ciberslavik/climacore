#pragma once

#include <QAbstractTableModel>

class GraphBase : public QAbstractTableModel
{
    Q_OBJECT

public:
    explicit GraphBase(QObject *parent = nullptr);


private:
};

